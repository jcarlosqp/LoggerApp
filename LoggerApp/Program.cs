using LogPluginContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using LoggerApp.Parameters;

namespace LoggerApp
{
    class Program
    {
        private static List<KeyValuePair<string, string>> _MyPluginNames;
        static void Main(string[] args)
        {
            ShowMenu();
            Console.ReadKey();

        }

        static void ShowMenu()
        {
            Console.Clear();
            ShowTitle("Pluggable Logger!");
            

            Console.WriteLine("[1] - Registrar en todas las estrategias.  ");
            Console.WriteLine("[2] - Registrar en una estrategia específica. ");
            Console.WriteLine("[3] - Salir. ");
            Console.WriteLine("");
            Console.WriteLine("Ingrese su opción [1-3]: ");

            
            int opt;
            string myOption = Console.ReadLine();
            var ok = int.TryParse(myOption, out opt);

            switch (opt)
            {
                case 1:
                    AllLoggerOption();
                    break;

                case 2:
                    OneLoggerOption();
                    break;

                case 3:
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine("* Presione cualquier tecla para volver al menú: ");
            Console.ReadKey();
            ShowMenu();

        }
        
        static void ShowTitle(string pTitle)
        {
            Console.WriteLine("_____________________________________________________________");
            Console.WriteLine(pTitle);
            Console.WriteLine("_____________________________________________________________");
        }

        public static void ShowLoggers()
        {

            _MyPluginNames = MyLogger.Logger(null, false);
            foreach (var item in _MyPluginNames)
            {
                var idx = _MyPluginNames.IndexOf(item);
                Console.WriteLine($"[{idx + 1}] - {item.Value} ");
            }
        }

        public static void ShowMessageTypes()
        {
            foreach (var item in Enum.GetValues(typeof(MessageTypeEnum)))
            {
                Console.WriteLine($"[{(int)item}] - {item.ToString()} ");
            }
        }

        static void AllLoggerOption()
        {
            
            Console.Clear();
            ShowTitle("Registrar en todas las estrategias");

            var myLog = new LogParameter();
            
            Console.WriteLine("- Ingrese Mensaje: ");
            myLog.Message = Console.ReadLine();

            Console.WriteLine("- Ingrese Tipo de Mensaje: ");
            ShowMessageTypes();
            
            try
            {
                myLog.MessageType = (MessageTypeEnum)Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception)
            {
                Console.WriteLine("Tipo incorrecto, presione cualquier tecla para volver al menú: ");
                Console.ReadKey();
                ShowMenu();
            }

            Console.WriteLine("CONFIRMA REGISTRO? Digite [S] para ejecutar o [N] para cancelar y retornar al Menú.  ");
            string rpta = Console.ReadLine();
            if (rpta.ToUpper() == "N")
            {
                ShowMenu();
            }
            else if (rpta.ToUpper() == "S")
            {
                myLog.PluginToExecute = "*";
                Console.WriteLine("RESULTADOS:");
                MyLogger.Logger(myLog, true);
            }
        }

        static void OneLoggerOption()
        {
            Console.Clear();
            ShowTitle("Registrar en estrategia específica");

            var myLog = new LogParameter();

            Console.WriteLine("Estrategias encontradas: ");
            ShowLoggers();
            Console.WriteLine("");
            Console.WriteLine("Ingrese número de estrategia elegida: ");
            
            try
            {
                int idxLog = Convert.ToInt32(Console.ReadLine());
                myLog.PluginToExecute = _MyPluginNames[idxLog - 1].Key;
            }
            catch (Exception)
            {
                Console.WriteLine("Número de estrategia incorrecto, presione cualquier tecla para volver al menú: ");
                Console.ReadKey();
                ShowMenu();
            }


            Console.WriteLine("- Ingrese Mensaje: ");
            myLog.Message = Console.ReadLine();

            Console.WriteLine("- Ingrese Tipo de Mensaje: ");
            ShowMessageTypes();
            try
            {
                myLog.MessageType = (MessageTypeEnum)Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Tipo incorrecto, presione cualquier tecla para volver al menú: ");
                Console.ReadKey();
                ShowMenu();
            }

            Console.WriteLine("CONFIRMA REGISTRO? Digite [S] para ejecutar o [N] para cancelar y retornar al Menú.  ");
            string rpta = Console.ReadLine();
            if (rpta.ToUpper() == "N")
            {
                ShowMenu();
            }
            else if (rpta.ToUpper() == "S")
            {
                Console.WriteLine("RESULTADOS:");
                MyLogger.Logger(myLog, true);
            }
        }

    }

}
