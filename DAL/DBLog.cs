using System;
using System.IO;

namespace DAL
{
    public class DBLog
    {
        private static string LogFile = "../DAL/Log/DB-log.txt";

        public static void ErrorToFile(string msg, string name, Exception feil)
        {
            try
            {
                StreamWriter streamWriter =
                    File.AppendText(LogFile);

                streamWriter.WriteLine("---------Error Log Start---------- on " + DateTime.Now);
                streamWriter.WriteLine(name + "  --  " + msg + "  --  " + feil.Message +
                                       (feil.InnerException == null ? "" : ("  --  " + feil.InnerException.Message)));
                streamWriter.WriteLine("---------Error Log End----------");
                streamWriter.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}