using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public static class Logging
    {
        static string APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static void StartLog()
        {
            if (!Directory.Exists(APPDATA + "\\MRCH"))
                Directory.CreateDirectory(APPDATA + "\\MRCH");
            if(!Directory.Exists(APPDATA + "\\MRCH\\log.txt"))
                File.Create(APPDATA + "\\MRCH\\log.txt").Dispose();
            File.AppendAllText(APPDATA + "\\MRCH\\log.txt", $"---Starting File System fo MES/RAI Configuration Helper ({DateTime.Now.Date}_{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond})---");
        }

        public static void Log(string text)
        {
            Debug.Log(text);
            if (!Directory.Exists(APPDATA + "\\MRCH"))
                Directory.CreateDirectory(APPDATA + "\\MRCH");

            if (!File.Exists(APPDATA + "\\MRCH\\log.txt"))
                File.Create(APPDATA + "\\MRCH\\log.txt").Dispose();

            string str = $"LOG:{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}:";
            str += $"{text};";

            File.AppendAllText(APPDATA + "\\MRCH\\log.txt", str);
        }
        public static void LogWrn(string text)
        {
            Debug.LogWarning(text);
            if (!Directory.Exists(APPDATA + "\\MRCH"))
                Directory.CreateDirectory(APPDATA + "\\MRCH");

            if (!File.Exists(APPDATA + "\\MRCH\\log.txt"))
                File.Create(APPDATA + "\\MRCH\\log.txt").Dispose();

            string str = $"WRN:{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}:";
            str += $"{text};";

            File.AppendAllText(APPDATA + "\\MRCH\\log.txt", str);
        }
        public static void LogErr(string text)
        {
            Debug.LogError(text);
            if (!Directory.Exists(APPDATA + "\\MRCH"))
                Directory.CreateDirectory(APPDATA + "\\MRCH");

            if (!File.Exists(APPDATA + "\\MRCH\\log.txt"))
                File.Create(APPDATA + "\\MRCH\\log.txt").Dispose();

            string str = $"ERR:{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}:";
            str += $"{text};";

            File.AppendAllText(APPDATA + "\\MRCH\\log.txt", str);
        }
    }
}
