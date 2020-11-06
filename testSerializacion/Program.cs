using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace testSerializacion
{
    class MainClass
    {
        static bool terminada = false;

        public static void Main(string[] args)
        {
            byte[] archivo1 = new byte[256];
            byte[] archivo2 = new byte[256];
            byte[] archivo3 = new byte[256];
            archivo1 = LeerArchivoBinario("animal1");
            archivo2 = LeerArchivoBinario("animal2");
            archivo3 = LeerArchivoBinario("animal3");

            Animal animal1 = (Animal)AObjeto(archivo1);
            Animal animal2 = (Animal)AObjeto(archivo2);
            Animal animal3 = (Animal)AObjeto(archivo3);

            ArrayList animales = new ArrayList
            {
                animal1, animal2, animal3
            };

            Console.WriteLine("-----Animales en competencia-----\n{0}\n{1}\n", animales[2], animales[1]);
            Console.WriteLine("-----Carrera-----");

            //Expresion lambda, no me funciona usar ParameterizardThreadStart o colocar parametro en el metodo start
            //https://docs.microsoft.com/en-us/dotnet/api/system.threading.parameterizedthreadstart?view=netcore-3.1
            Thread t1 = new Thread(() => Avanzar((Animal)animales[2])); 
            Thread t2 = new Thread(() => Avanzar((Animal)animales[1]));
            //Thread t3 = new Thread(() => Avanzar((Animal)animales[2]));

            t1.Start();
            t2.Start();
        }

        public static void Avanzar(Animal competidor)
        {
            Random rd = new Random();
            int evaluar = rd.Next(1, 800);
            for (int i = 1; i <= 800; i++)
            {
                if (terminada)
                    Thread.CurrentThread.Abort();               
                else 
                {
                    Thread.Sleep(100 - competidor.Velocidad);
                    Console.Write(competidor.Nombre.Substring(0, 1));
                    if (i == evaluar)
                    {
                        if (competidor.SeDuerme)
                        {
                            Console.Write(" ***{0} se ha dormido*** ", competidor.Nombre);
                            Thread.Sleep(competidor.Velocidad * 10);
                            Console.Write(" ***{0} ha despertado*** ", competidor.Nombre);
                        }
                    }
                }
            }
            terminada = true;
            Console.Write(" ***{0} es el ganador*** ", competidor.Nombre);
        }

        public static byte[] LeerArchivoBinario(string archivo)
        {
            try
            {
                byte[] buffer = new byte[256];
                FileStream fs = new FileStream(archivo, FileMode.Open);
                fs.Read(buffer, 0, 256);
                fs.Close();
                return buffer;
            }
            catch (Exception e)
            {
                Console.Write("***Error**** : {0}", e.ToString());
            }
            return null;
        }

        public static Object AObjeto(byte[] buffer)
        {
            try
            {
                Stream ms = new MemoryStream(buffer);
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(ms);
            }
            catch (Exception e)
            {
                Console.Write("***Error**** : {0}", e.ToString());
            }
            return null;
        }
    }
}



