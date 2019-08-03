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
            this.pathToFile = "C:\\bidask\\logFile";
            this.createLoggingFile(this.pathToFile);
            try
            {

                this.telegram = new Telegram.Telegram();
            }
            catch
            {

            }
        }
        public void Log(Exception ex)
        {
            var loggingMessage = ex.Message;
            using (StreamWriter sw = File.AppendText(this.pathToFile))
            {
                sw.WriteLine(DateTime.Now.ToString() +"------"+ex.Message);
            }

            try
            {
                this.telegram.sendMessage(ex.Message);
            }
            catch
            {

            }

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
