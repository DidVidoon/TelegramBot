using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public static class Logs
    {
        readonly static string writePath = @"Logs.txt";

        public static void Writer(string message)
        {
            string dateTime = DateTime.Now.ToString(format: "dd.mm.yyyy hh:mm:ss");

            try
            {
                if (!File.Exists(writePath))
                {
                    using (StreamWriter sw = new(writePath, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLineAsync(dateTime + " " + message);
                    }
                }
                else
                {
                    using (StreamWriter sw = new(writePath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLineAsync(dateTime + " " + message);
                    }
                }

                Console.WriteLine(dateTime + " " + message);
            }
            catch (Exception e)
            {
                Console.WriteLine(dateTime + " " + e.Message);
            }
        }
    }
}
