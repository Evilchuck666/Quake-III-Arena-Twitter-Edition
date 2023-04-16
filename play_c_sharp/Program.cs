using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quake3configurator
{
    internal class Program
    {
        private static void Greeting()
        {
            Console.WriteLine();
            Console.WriteLine("                       .,o'       `o,.");
            Console.WriteLine("                     ,o8'           `8o.");
            Console.WriteLine("                    o8'               `8o");
            Console.WriteLine("                   o8:                 ;8o");
            Console.WriteLine("                  .88                   88.");
            Console.WriteLine("                  :88.                 ,88:");
            Console.WriteLine("                  `888                 888'");
            Console.WriteLine("                   888o   `888 888'   o888");
            Console.WriteLine("                   `888o,. `88 88' .,o888'");
            Console.WriteLine("                    `8888888888888888888'");
            Console.WriteLine("                      `888888888888888'");
            Console.WriteLine("                         `::88;88;:'");
            Console.WriteLine("                            88 88");
            Console.WriteLine("                            88 88");
            Console.WriteLine("                            `8 8'");
            Console.WriteLine("                             ` ' ");
            Console.WriteLine("################################################################");
            Console.WriteLine("#### ¡BIENVENIDO AL CONFIGURADOR BÁSICO DE QUAKE III ARENA! ####");
            Console.WriteLine("################################################################");
        }

        private static int GetInputMenu(Dictionary<string, string> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option.Value);
            }

            Console.WriteLine("\nElige una opción del menú:");
            Console.Write(">>> ");

            var input = Console.ReadLine();
            while (!options.ContainsKey(input))
            {
                Console.WriteLine("Opción incorrecta, intentémoslo de nuevo");
                Console.Write(">>> ");
                input = Console.ReadLine();
            }
            return Convert.ToInt32(input);
        }

        private static int MainMenu()
        {
            var options = new Dictionary<string, string>
            {
                { "1", "[1] - Configuración básica" },
                { "2", "[2] - Ejecutar Quake III Arena" },
                { "3", "[3] - Ejecutar Quake III Arena con mods" },
                { "0", "[0] - Salir del programa" }
            };

            return GetInputMenu(options);
        }

        private static int ModMenu()
        {
            var options = new Dictionary<string, string>
            {
                { "1", "[1] - Ejecutar Quake III Team Arena" },
                { "2", "[2] - Ejecutar OSP" },
                { "3", "[3] - Ejecutar CPMA" },
                { "0", "[0] - Salir de este menú" }
            };

            Console.WriteLine();
            Console.WriteLine("##########################################");
            Console.WriteLine("#### MENÚ PARA LANZAR QUAKE III ARENA ####");
            Console.WriteLine("##########################################");

            return GetInputMenu(options);
        }

        private static void LaunchMod(bool launchBaseGame = false)
        {
            var autoexec_cfg = " +exec autoexec.cfg";
            var arguments = new Dictionary<int, string>
            {
                { 1, " +set fs_game missionpack" + autoexec_cfg },
                { 2, " +set fs_game osp" + autoexec_cfg },
                { 3, " +set fs_game cpm" + autoexec_cfg },
                { 4, autoexec_cfg }
            };

            int mod = !launchBaseGame ? ModMenu() : 4;
            if (mod != 0)
            {
                var game = new ProcessStartInfo { Arguments = arguments[mod], FileName = "./quake3.exe", UseShellExecute = false };
                Process.Start(game);
            }
        }

        private static void LaunchGame()
        {
            LaunchMod(true);
        }

        private static string WritePlayerName()
        {
            Console.WriteLine("\nEscribe el nombre que quieres tener en el juego");
            Console.Write(">>> ");

            var playerName = Console.ReadLine().Trim();
            while (playerName == "")
            {
                Console.WriteLine("No has escrito nada, intentémoslo de nuevo");
                Console.Write(">>> ");
                playerName = Console.ReadLine().Trim();
            }

            return playerName;
        }

        private static string WriteSensitivity()
        {
            Console.WriteLine("\nEscribe un número decimal con máximo de 2 decimales, entre 1 y 5, más alto el valor, mayor sensibilidad");
            Console.Write(">>> ");
            try
            {
                var sensitivity = Convert.ToDecimal(Console.ReadLine().Replace(",", "."));
                while (sensitivity < 1.00M || sensitivity > 5.00M)
                {
                    Console.WriteLine("El valor introducido no es correcto, intentémoslo de nuevo");
                    Console.Write(">>> ");
                    sensitivity = Convert.ToDecimal(Console.ReadLine().Replace(",", "."));
                }
                return sensitivity.ToString();
            }
            catch (FormatException)
            {
                Console.WriteLine("El valor introducido no es correcto, intentémoslo de nuevo");
                return WriteSensitivity();
            }
        }

        private static string WriteModel()
        {
            var modelsList = new string[]
            {
                "anarki/blue",      "anarki/default",   "anarki/red",       "biker/blue",
                "biker/cadavre",    "biker/default",    "biker/hossman",    "biker/red",
                "biker/slammer",    "biker/stroggo",    "bitterman/blue",   "bitterman/default",
                "bitterman/red",    "bones/blue",       "bones/bones",      "bones/default",
                "bones/red",        "crash/blue",       "crash/default",    "crash/red",
                "doom/blue",        "doom/default",     "doom/phobos",      "doom/red",
                "grunt/blue",       "grunt/default",    "grunt/red",        "grunt/stripe",
                "hunter/blue",      "hunter/default",   "hunter/harpy",     "hunter/red",
                "keel/blue",        "keel/default",     "keel/red",         "klesk/blue",
                "klesk/default",    "klesk/flisk",      "klesk/red",        "lucy/angel",
                "lucy/blue",        "lucy/default",     "lucy/red",         "major/blue",
                "major/daemia",     "major/default",    "major/red",        "mynx/blue",
                "mynx/default",     "mynx/red",         "orbb/blue",        "orbb/default",
                "orbb/red",         "ranger/blue",      "ranger/default",   "ranger/red",
                "ranger/wrack",     "razor/blue",       "razor/default",    "razor/id",
                "razor/patriot",    "razor/red",        "sarge/blue",       "sarge/default",
                "sarge/krusade",    "sarge/red",        "slash/blue",       "slash/default",
                "slash/grrl",       "slash/red",        "slash/yuriko",     "sorlag/blue",
                "sorlag/default",   "sorlag/red",       "tankjr/blue",      "tankjr/default",
                "tankjr/red",       "uriel/blue",       "uriel/default",    "uriel/red",
                "uriel/zael",       "visor/blue",       "visor/default",    "visor/gorre",
                "visor/red",        "xaero/blue",       "xaero/default",    "xaero/red",
            };

            var random = new Random();
            var index = random.Next(0, modelsList.Length);

            return modelsList[index];
        }

        private static void WriteFile()
        {
            var files = new string[]
            {
                "baseq3/autoexec.cfg.orig",
                "cpma/autoexec.cfg.orig",
                "missionpack/autoexec.cfg.orig",
                "osp/autoexec.cfg.orig",
            };

            var player = WritePlayerName();
            var sensitivity = WriteSensitivity();
            var model = WriteModel();

            foreach (var file in files)
            {
                var newFile = file.Replace(".orig", "");
                var cfg = File.ReadAllText(file);

                cfg = cfg.Replace("{quake_3_player}", player);
                cfg = cfg.Replace("{quake_3_sensitivity}", sensitivity);
                cfg = cfg.Replace("{quake_3_model}", model);

                File.WriteAllText(newFile, cfg);
            }
        }

        static void Main()
        {
            Greeting();
            int method = MainMenu();
            while (method != 0)
            {
                switch (method)
                {
                    case 1:
                        WriteFile();
                        break;
                    case 2:
                        LaunchGame();
                        break;
                    case 3:
                        LaunchMod();
                        break;
                    default:
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("########################");
                Console.WriteLine("#### MENÚ PRINCIPAL ####");
                Console.WriteLine("########################");

                method = MainMenu();
            }
            Console.WriteLine("\n¡HASTA LA PRÓXIMA!\n");
        }
    }
}
