using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace Racun
{
    internal class Root
    {
        public int ErrCode { get; set; }
        public object ErrDesc { get; set; }
        public object ErrArgs { get; set; }

        public Root()
        {
            try
            {
                string jsonString = new WebClient().DownloadString("https://apica.iplus.si/api/Naloga?API_KEY=5DC144EF-434C-43F6-8014-7ADD3CE06DAB");
                dynamic jsonFile = JsonConvert.DeserializeObject(jsonString);
                this.ErrCode = Int32.Parse(jsonFile.SelectToken("ErrCode").ToString());
                this.ErrDesc = jsonFile.SelectToken("ErrDesc").ToString();
                this.ErrArgs = jsonFile.SelectToken("ErrArgs").ToString();
            }
            catch (Exception ex) { MessageBox.Show("Prislo je do napake med branjem JSON\nException: " + ex.Message); }
        }
    }
}
