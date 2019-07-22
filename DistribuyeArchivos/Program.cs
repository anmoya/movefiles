using System;
using System.IO;
using System.Collections.Generic;
using DistribuyeArchivos._services;
using Microsoft.Extensions.Configuration;

namespace DistribuyeArchivos
{
    class Program
    {
        static int NumeroArchivos = 0;
        static int NumeroRutas = 0;
        static List<string> ListaRutas = new List<string>();
        static string[] ListaArchivos;
        static string Ruta = String.Empty;
        static string Extension = String.Empty;
        static Dictionary<String, String> dicParametros = new Dictionary<String, String>();

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

            RevisarParametros(args);
            Ruta = dicParametros.GetValueOrDefault("Ruta");
            Extension = dicParametros.GetValueOrDefault("Extension");
            NumeroArchivos = FileService.NumeroArchivos(Ruta, Extension);
            ListaArchivos = FileService.ListaArchivos(Ruta, Extension);
            ListaRutas = FileService.ObtenerRutas(config);
            NumeroRutas = ListaRutas.Count;

            // LOG
            // Tenemos x archivos, y rutas. 

            decimal cantidad = Math.Floor((decimal)NumeroArchivos / NumeroRutas);
            int resto = NumeroArchivos % NumeroRutas;

            Console.WriteLine(cantidad);
            Console.WriteLine(resto);

            int contador = 0;
            foreach (string rutaOut in ListaRutas)
            {
                Console.WriteLine($"Contador es {contador}.");
                for (var i = 0; i < cantidad; i++)
                {
                    contador++; 
                    Console.WriteLine($"Moví {Path.GetFileName(ListaArchivos[i])} archivos a {rutaOut + "\\" + Path.GetFileName(ListaArchivos[i])}");
                }

                    //File.Move(ListaArchivos[i], rutaOut);
            }

            //for (var i = 0; i < ListaArchivos.Length; i++)
            //{
            //    Console.WriteLine(Path.GetFileName(ListaArchivos[i]));
            //    contador++;
            //}

            //Console.WriteLine(contador;)

     






            Console.WriteLine($"Existen {NumeroArchivos} con extensión {Extension} en la ruta {Ruta}.");
        }

        private static void RevisarParametros(string[] args)
        {
            // ¿Cuantos parámetros vienen?
            int TotalParametros = args.Length;

            if (TotalParametros == 0)
                throw new Exception("No ha ingresado los parámetros correspondientes. Al menos debe indicar la ruta. ");
            else if (TotalParametros == 1)
            {
                dicParametros.Add("Ruta", args[0]);
                dicParametros.Add("Extension", "txt");
                // TODO: Agrear log indicando que se asume txt
            }
            else
            {
                dicParametros.Add("Ruta", args[0]);
                dicParametros.Add("Extension", args[1]);
            }

            // Comprobamos que el directorio exista
            if (!Directory.Exists(dicParametros.GetValueOrDefault("Ruta")))
                throw new Exception($"La ruta indicada no es valida. No existe o no es accesible. Ruta: {dicParametros.GetValueOrDefault("Ruta")}");
        }
    }
}
