using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketServidor
{
    class Program
    {
        static void GenerarComunicacion(ClienteCom clienteCom)
        {
            bool terminar = false;
            while (!terminar)
            {
                string mensaje = clienteCom.Leer();
                if(mensaje != null)
                {
                    Console.WriteLine("C: {0}", mensaje);
                    if(mensaje.ToLower() == "chao")
                    {
                        terminar = true;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese respuesta:");
                        mensaje = Console.ReadLine().Trim();
                        clienteCom.Escribir(mensaje);
                        if(mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    }
                }
                else
                {
                    terminar = false;
                }
            }
            clienteCom.Desconectar();

        }
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            ServidorSocket servidorSocket = new ServidorSocket(puerto);
            if (servidorSocket.Iniciar())
            {
                while (true)
                {


                    Console.WriteLine("Esperando cliente...");
                    Socket socket = servidorSocket.ObtenerCliente();
                    Console.WriteLine("Cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(socket);
                    GenerarComunicacion(clienteCom);
                }
            }
            else
            {
                Console.WriteLine("Error al tomar posesión del puerto {0}", puerto);
            }


        }
    }
}
