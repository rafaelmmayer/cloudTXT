using ClassLibrary.Api;
using ClassLibrary.Models;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += delegate {
                Environment.Exit(0);
            };

            Console.WriteLine("<------Monitoramente Cloud TXT----->\n");

            var path = ConfigurationManager.AppSettings.Get("Path");
            ArquivoApi api = new ArquivoApi();

            while (true)
            {
                var files = Directory.EnumerateFiles(path, "*.txt");

                foreach (var file in files)
                {
                    try
                    {
                        Arquivo arquivo = new Arquivo()
                        {
                            nome = Path.GetFileNameWithoutExtension(file),
                            conteudo = File.ReadAllText(file)
                        };
                        api.SaveArquivo(arquivo).Wait();
                        File.Delete(file);
                        Console.WriteLine($"{arquivo.nome} salvo com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                Thread.Sleep(1000);
            }

        }


    }
}
