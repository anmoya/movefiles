using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DistribuyeArchivos._services
{
    public class FileService
    {
        public static int NumeroArchivos(string path, string extension) => Directory.GetFiles(path, $"*.{extension}").Length;
        internal static string[] ListaArchivos(string path, string extension) => Directory.GetFiles(path, $"*.{extension}");

        internal static List<string> ObtenerRutas(IConfiguration config)
        {
            List<string> carpetas = new List<string>();

            // Rescatamos rutas desde el config
            foreach (var element in config.GetSection("Out").GetChildren())
                carpetas.Add(element.Value);
                
            return carpetas;
        }


    }
}
