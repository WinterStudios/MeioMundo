using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeioMundoWPF
{
    public static class UpdateSystem
    {
        public static string Version 
        {
            get
            {
                return Properties.Settings.Default.Version;
            }
        }
        public static void SetVersion()
        {
            string major = DateTime.Now.Year.ToString();
            string minor = DateTime.Now.Month.ToString();
            string build = DateTime.Now.Day.ToString();
            string revision = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");

            Properties.Settings.Default.Version = string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
            Properties.Settings.Default.Save();
        }
        
    }
}
