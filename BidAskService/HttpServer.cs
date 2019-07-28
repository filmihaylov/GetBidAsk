using BidAskCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BidAskService
{
    public class HttpServer
    {
        private DbOpperations db;
        private HttpListener web;
        public HttpServer()
        {
            this.db = new DbOpperations();
            this.web = new HttpListener();
            this.web.Prefixes.Add("http://localhost:8081/");
            this.web.Start();
        }
        public void start()
        {
            var context = web.GetContext();
            var response = context.Response;
            string responseString = getLastCurrency();
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

        }
        public void stop()
        {
            this.web.Stop();
        }

        public string getLastCurrency()
        {
            Currency lastCurrency =  this.db.GetLastCurrency();
            var json = new JavaScriptSerializer().Serialize(lastCurrency);
            return json;
        }
    }
}
