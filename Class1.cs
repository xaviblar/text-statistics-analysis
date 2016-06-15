//Librerias Utilizadas
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace IIPrograArqui
{
    class Metodos
    {
        //Se declaran los temporizadores, los cuales mediran el tiempo de cada operacion realizada, tiempo que se guardara en variables tipo long
        static Stopwatch temporizadorTotal, temporizador1, temporizador2, temporizador3;
        static long tiempoTotal, tiempo1, tiempo2, tiempo3;

        //Busca la palabra Mas larga del archivo y la imprime, recibe la ruta del archivo y un bool que funciona como
        //flag para indicar los pasos a seguir dependiendo si es un archivo web o local
        public static void PalabraMasLarga(string ruta, bool Web)
        {
            if (Web == false)
            {
                StreamReader archivo = new StreamReader(ruta);
                string palabraMasLarga = " ";
                string palabra;
                Regex R = new Regex(" +");
                palabra = archivo.ReadLine();
                while (palabra != null)
                {
                    string[] Palabras = R.Split(palabra);
                    foreach (String W in Palabras)
                    {
                        if (W.Length >= palabraMasLarga.Length)
                        {
                            palabraMasLarga = W;
                        }
                    }
                    palabra = archivo.ReadLine();
                }
                Console.WriteLine("La palabra mas larga del archivo es " + palabraMasLarga + ", Con " + palabraMasLarga.Length + " caracteres");
            }
            else
            {
                var archivo = new System.Net.WebClient().DownloadString(ruta);
                string palabraMasLarga = " ";
                Regex R = new Regex(" +");
                string[] Palabras = R.Split(archivo);
                foreach (String W in Palabras)
                {
                    if (W.Length >= palabraMasLarga.Length)
                    {
                        palabraMasLarga = W;
                    }
                }
                Console.WriteLine("La palabra mas larga del archivo es " + palabraMasLarga + ", Con " + palabraMasLarga.Length + " caracteres");
            }

        }

        //Funcione que recibe una palabra y la ruta del archivo a trabajar, y busca e imprime la cantidad de veces que esta palabra aparece
        //en el archivo, ademas recibe un bool que funciona como flag para determinar los pasos a seguir dependiendo si se trabaja con un archivo web
        //o local/
        public static void NumeroDeVeces(string Palabra, string ruta, bool Web)
        {

            if (Web == false)
            {
                StreamReader archivo = new StreamReader(ruta);
                string TodoArchivo;
                TodoArchivo = archivo.ReadLine();
                int veces = 0;
                Regex R = new Regex(" +");
                while (TodoArchivo != null)
                {
                    string[] Palabras = R.Split(TodoArchivo);
                    foreach (String W in Palabras)
                    {
                        if (W == Palabra)
                        {
                            veces++;
                        }
                    }
                    TodoArchivo = archivo.ReadLine();
                }
                Console.WriteLine("La palabra " + Palabra + " aparece " + veces + " veces en el archivo");
            }

            else
            {
                var archivo = new System.Net.WebClient().DownloadString(ruta);
                int veces = 0;
                Regex R = new Regex(" +");
                string[] Palabras = R.Split(archivo);
                foreach (String W in Palabras)
                {
                    if (W == Palabra)
                    {
                        veces++;
                    }
                }
                Console.WriteLine("La palabra " + Palabra + " aparece " + veces + " veces en el archivo");
            }


        }

        //Funcion que inserta todas las distintas palabras que aparecen en el archivo a trabajar en un diccionario con la cantidad de veces que 
        //aparecen, luego ordena ese diccionario de manera descendente e imprime la cantidad de palabras solicitadad, ademas recibe un bool que 
        //funciona como flag para determinar los pasos a seguir dependiendo si se trabaja con un archivo web o local.
        public static void PalabrasComunes(int Numero, string ruta, bool Web)
        {
            if (Web == false)
            {
                Dictionary<string, int> Diccionario = new Dictionary<string, int>();
                StreamReader archivo = new StreamReader(ruta);
                string palabra;
                palabra = archivo.ReadLine();
                while (palabra != null)
                {
                    Regex R = new Regex(" +");
                    string[] Palabras = R.Split(palabra);
                    foreach (String W in Palabras)
                    {
                        if (Diccionario.ContainsKey(W))
                        {
                            Diccionario[W]++;
                        }
                        else
                        {
                            Diccionario.Add(W, 1);
                        }
                    }
                    palabra = archivo.ReadLine();
                }

                var items = from pair in Diccionario
                            orderby pair.Value descending
                            select pair;
                int cont = 0;
                Console.WriteLine("-----Palabras Comunes-----");
                foreach (KeyValuePair<string, int> pair in items)
                {
                    if (cont >= Numero)
                    {
                        break;
                    }
                    else if (pair.Key != "")
                    {
                        Console.WriteLine("{0}: {1}", pair.Key, pair.Value + " Veces");
                        cont++;
                    }
                }
            }
            else
            {
                Dictionary<string, int> Diccionario = new Dictionary<string, int>();
                var archivo = new System.Net.WebClient().DownloadString(ruta);
                Regex R = new Regex(" +");
                string[] Palabras = R.Split(archivo);
                foreach (String W in Palabras)
                {
                    if (Diccionario.ContainsKey(W))
                    {
                        Diccionario[W]++;
                    }
                    else
                    {
                        Diccionario.Add(W, 1);
                    }
                }

                var items = from pair in Diccionario
                            orderby pair.Value descending
                            select pair;
                int cont = 0;
                foreach (KeyValuePair<string, int> pair in items)
                {
                    if (cont >= Numero)
                    {
                        break;
                    }
                    else if (pair.Key != "")
                    {
                        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                        cont++;
                    }
                }
            }
        }

        //Funcion que llama de manera secuencial  a los 3 metodos anterioress, y mide el tiempo de ejecucion de cada uno mediante los 
        //temporizadores declarados anteriormente.
        public static void EjecucionSecuencial(string Palabrap, int numerop, string ruta, bool Web)
        {
            temporizadorTotal = Stopwatch.StartNew();

            temporizador1 = Stopwatch.StartNew();
            PalabraMasLarga(ruta, Web);
            tiempo1 = temporizador1.ElapsedMilliseconds;

            temporizador2 = Stopwatch.StartNew();
            NumeroDeVeces(Palabrap, ruta, Web);
            tiempo2 = temporizador2.ElapsedMilliseconds;

            temporizador3 = Stopwatch.StartNew();
            PalabrasComunes(numerop, ruta, Web);
            tiempo3 = temporizador3.ElapsedMilliseconds;

            tiempoTotal = temporizadorTotal.ElapsedMilliseconds;
            Console.WriteLine();
            Console.WriteLine("Tiempo de ejecución total en modalidad secuencial: " + tiempoTotal + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la primera operación: " + tiempo1 + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la segunda operación: " + tiempo2 + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la tercera operación: " + tiempo3 + " milisegundos");
            Console.ReadLine();

        }

        //Funcion que llama a las 3 Operaciones anteriores de manera paralela, utilizando un Parallel.Invoke, ademas mide el tiempo de ejecucion 
        //de cada una mediante los temporizadores declarados anteriormente.
        public static void EjecucionParalela(string Palabrap, int numerop, string ruta, bool Web)
        {
            temporizadorTotal = Stopwatch.StartNew();
            Parallel.Invoke(() =>
            {
                temporizador1 = Stopwatch.StartNew();
                PalabraMasLarga(ruta, Web);
                tiempo1 = temporizador1.ElapsedMilliseconds;
            },

            () =>
            {
                temporizador2 = Stopwatch.StartNew();
                NumeroDeVeces(Palabrap, ruta, Web);
                tiempo2 = temporizador2.ElapsedMilliseconds;
            },

             () =>
             {
                 temporizador3 = Stopwatch.StartNew();
                 PalabrasComunes(numerop, ruta, Web);
                 tiempo3 = temporizador3.ElapsedMilliseconds;
                 tiempoTotal = temporizadorTotal.ElapsedMilliseconds;
             });

            Console.WriteLine();
            Console.WriteLine("Tiempo de ejecución total en modalidad parelela: " + tiempoTotal + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la primera operación: " + tiempo1 + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la segunda operación: " + tiempo2 + " milisegundos");
            Console.WriteLine("Tiempo de ejecución de la tercera operación: " + tiempo3 + " milisegundos");
            Console.ReadLine();

        }
    }
}
