using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllGameLauncherUWP
{
    public static class Singlton
    {
        public delegate void set();
        public static event set SettingChange;


        public static void ChangingSettings() => SettingChange?.Invoke();
    }
}
