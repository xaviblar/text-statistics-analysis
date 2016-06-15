//---Librerias Utilizadas---
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace IIPrograArqui
{
    class IIPrograArqui
    {
        //---Funcion Principal---
        static void Main(string[] args)
        {
            bool Web;
            //---Flag para indicar si se trabajara con un archivo desde web o local
            Console.WriteLine("Digite 1 si desea leer un archivo desde HDD u otra tecla si desea leer un archivo desde Web");
            string opcionWeb = Console.ReadLine();
            if (opcionWeb.Equals("1"))
            {
                Web = false;
            }
            else
            {
                Web = true;
            }
            //Brindar Ruta exacta del archivo
            Console.WriteLine("Digite la ruta de ubicacion exacta del archivo (Web o local,si este se encuentra en la misma carpeta que el ejecutable solo ingrese el nombre del archivo) si necesita ingresar un slash invertido representelo con un doble slash invertido");
            string ruta = Console.ReadLine();
            Console.WriteLine("Digite la palabra de la cual desea conocer sus apariciones en el archivo: ");
            string Palabra = Console.ReadLine();
            Console.WriteLine("Digite la cantidad de palabras mas comunes que desea conocer:  ");
            int Numero = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite 1 para una ejecución secuencial del programa o 2 para una ejecución paralela");
            //Usuario determina si desea ejecutar el programa secuencial o paralelamente
            string opcion = Console.ReadLine();
            if (opcion.Equals("1"))
            {
                Metodos.EjecucionSecuencial(Palabra, Numero, ruta, Web);
                Console.ReadLine();
            }
            else
            {
                Metodos.EjecucionParalela(Palabra, Numero, ruta, Web);
                Console.ReadLine();
            }
        }
    }
}
