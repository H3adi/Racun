using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Racun
{
    internal class Podatki
    {
        public string nazivPodjetja { get; set; }
        public string naslovPodjetja { get; set; }
        public string postaPodjetja { get; set; }
        public string idDDV { get; set; }
        public string prodajalec { get; set; }
        public string stRacuna { get; set; }
        public DateTime datum { get; set; } = DateTime.Now;
        public double DDV { get; set; }
        public string ZOI { get; set; }
        public string EOR { get; set; }
        public List<Postavke> postavkeRacuna { get; set; } = new List<Postavke>();

        public Podatki()
        {
            try
            {
                string jsonString = new WebClient().DownloadString("https://apica.iplus.si/api/Naloga?API_KEY=5DC144EF-434C-43F6-8014-7ADD3CE06DAB");
                //dynamic jsonFile = JsonConvert.DeserializeObject<Podatki>(jsonString); // Gleda direktno v class
                dynamic jsonFile = JsonConvert.DeserializeObject(jsonString);
                string vsiPodOPodjetju = jsonFile.SelectToken("Data.a").ToString();
                string[] podOPod = vsiPodOPodjetju.Split('#');

                this.nazivPodjetja = podOPod[0];
                this.naslovPodjetja = podOPod[1];
                this.postaPodjetja = podOPod[2];
                this.idDDV = podOPod[3];

                string samoPos = jsonFile.SelectToken("Data.z").ToString();
                dynamic jObjPos = JsonConvert.DeserializeObject(samoPos);

                foreach (var package in jObjPos)
                {
                    Postavke novPostavka = new Postavke();
                    novPostavka.a = package["a"];
                    novPostavka.b = package["b"];
                    novPostavka.c = package["c"];
                    this.postavkeRacuna.Add(novPostavka);
                }

                this.DDV = Convert.ToDouble(jsonFile.SelectToken("Data.e").ToString());
                this.prodajalec = jsonFile.SelectToken("Data.b").ToString();
                this.stRacuna = jsonFile.SelectToken("Data.c").ToString();
                this.ZOI = jsonFile.SelectToken("Data.f").ToString();
                this.EOR = jsonFile.SelectToken("Data.g").ToString();
            }
            catch (Exception ex) { MessageBox.Show("Prislo je do napake med branjem JSON\nException: " + ex.Message); }
        }
        public int prestejPostavke()
        {
            try
            {
                string jsonString = new WebClient().DownloadString("https://apica.iplus.si/api/Naloga?API_KEY=5DC144EF-434C-43F6-8014-7ADD3CE06DAB");
                dynamic jsonFile = JsonConvert.DeserializeObject(jsonString);
                dynamic jObj = JsonConvert.DeserializeObject(jsonFile.SelectToken("Data.z").ToString());
                int st = 0;

                foreach (var package in jObj) st++;

                return st;
            }
            catch (Exception ex) { MessageBox.Show("Prislo je do napake med branjem JSON\nException: " + ex.Message); }
            return -1;
        }
    }
}
