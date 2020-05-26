using System;

namespace MeioMundoWPF
{
    public static class UpdateSystem
    {
        public static string Version
        {
            get
            {
#if DEBUG
                return Properties.Settings.Default.Version + "d";
#endif
                return Properties.Settings.Default.Version + "r";
            }
        }
        public static void SetVersion(bool debug)
        {
            string major = DateTime.Now.Year.ToString();
            string minor = DateTime.Now.Month.ToString();
            string build = DateTime.Now.Day.ToString();
            string revision = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");

            if (!debug)
                Properties.Settings.Default.Version = string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
            else
                Properties.Settings.Default.Version = string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
            Properties.Settings.Default.Save();
        }

    }
}
