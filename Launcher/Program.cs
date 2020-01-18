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
            try
            {
                string executable = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\AllGameLauncher\start.txt");
                Process.Start(executable);
            }
            catch (Exception e)
            {}

        }
    }
}
