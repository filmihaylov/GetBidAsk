using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Logger
{
    public class Logger
    {
        private string pathToFile;
        private Telegram.Telegram telegram;
        public Logger()
        {
            this.pathToFile = "logFile";
            this.createLoggingFile(this.pathToFile);
            this.telegram = new Telegram.Telegram();
        }
        public void Log(Exception ex)
        {
            var loggingMessage = ex.Message;
            using (StreamWriter sw = File.AppendText(this.pathToFile))
            {
                sw.WriteLine(ex.Message);
            }

            this.telegram.sendMessage(ex.Message);
        } 

        private void createLoggingFile(string logFile)
        {
            if (!File.Exists(logFile))
            {
                File.Create(logFile);
            }
        }

    }
}
