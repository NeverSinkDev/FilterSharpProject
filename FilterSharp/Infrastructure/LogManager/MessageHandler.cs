using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.LogManager
{
     

    public enum LL { Debug, Warning, Error, Info }

    public class MessageHandler
    {
        // [Ini] 
        public LL LogLevelThreshold { get; set; } = LL.Debug;
    
        private static MessageHandler instance;
 
        public static void Write(string text, LL logLevel)
        {
            if (instance == null)
            {
                instance = new MessageHandler();
            } 
            instance.WriteLN(text, logLevel); 
        }
        private string GetLLText(LL Loglevel)
        {
            if (Loglevel == LL.Debug)
            {
                return "Debug  ";
            }

            if (Loglevel == LL.Warning)
            {
                return "Warning";
            }

            if (Loglevel == LL.Error)
            {
                return "Error  ";
            }

            return "Info   "; 
        }
        private void WriteLN(string text, LL logLevel)
        {
            if (logLevel >= LogLevelThreshold)
            {
                string output = System.DateTime.Now.ToString()
                    + " : " + new StackFrame(2).GetMethod().Module
                    + " : " + new StackFrame(2).GetMethod().Name;  

                output =  "["+ GetLLText(logLevel) + "] " + output + " : " + text;

                Console.WriteLine(output);
                 
                string date =    System.DateTime.Now.Year.ToString() + "."+
                                 System.DateTime.Now.Month.ToString() + "." +
                                 System.DateTime.Now.Day.ToString() + " " +
                                 System.DateTime.Now.Hour.ToString() + "_00_00";

                string fullPath1 = AppDomain.CurrentDomain.BaseDirectory;
                if (!Directory.Exists(fullPath1 + "log"))
                {
                    Directory.CreateDirectory(fullPath1 + "log");
                }
                string filenameall = fullPath1 + "log\\"  + "all " + date + ".txt";

                using (StreamWriter sw = File.AppendText(filenameall))
                {
                    sw.WriteLine(output); 
                }

                string filenameLoglevel = fullPath1 + "log\\" + logLevel.ToString() +" " + date + ".txt";

                using (StreamWriter sw = File.AppendText(filenameLoglevel))
                {
                    sw.WriteLine(output);
                }
                  
            }
        } 
    }

    [TestFixture]
    class LogHndTest
    {
        [Test]
        public static void LogHndAcceptanceTest()
        {
             

            MessageHandler.Write("Debug", LL.Debug);
            MessageHandler.Write("Warning", LL.Warning);
            MessageHandler.Write("Error", LL.Error);
            MessageHandler.Write("Info", LL.Info); 
        }
    }
}
