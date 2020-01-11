using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (object i in args)
            {
                Console.WriteLine(i);
            }
            try
            {
                if (args.Length != 0)
                {
                    string executable = args[0];
                    /*uncomment the below three lines if the exe file is in the Assets  
                     folder of the project and not installed with the system*/
                    /*string path=Assembly.GetExecutingAssembly().CodeBase;
                    string directory=Path.GetDirectoryName(path);
                    process.Start(directory+"\\"+executable);*/
                    Process.Start(executable);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }
    }
}
