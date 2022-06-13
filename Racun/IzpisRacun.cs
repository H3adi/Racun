using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Racun
{
    public partial class IzpisRacun : Form
    {
        Root napake = new Root();
        Podatki podRacun = new Podatki();
        double skVredArtiklovNeto = 0.0;
        double skVredArtiklovBruto = 0.0;
        double skVredArtiklov = 0.0;

        public IzpisRacun()
        {
            InitializeComponent();
        }

        private void IzpisRacun_Load(object sender, EventArgs e)
        {
            int numPos = podRacun.prestejPostavke();
            int x = 12, y = 330;

            if (!napake.ErrCode.Equals(0))
            {
                MessageBox.Show("Napaka med izdelavo JSON datoteke!");
                Application.Exit();
            }

            for (int i = 0; i < numPos; i++)
            {
                Label artikel = new Label();
                Label kolicina = new Label();
                Label vrednos = new Label();

                artikel.Text = "Artikel: " + podRacun.postavkeRacuna[i].a;
                artikel.Location = new Point(x, y);
                artikel.AutoSize = true;
                this.Controls.Add(artikel);

                kolicina.Text = "Količina: " + podRacun.postavkeRacuna[i].b + "x";
                kolicina.Location = new Point(x, y+20);
                this.Controls.Add(kolicina);

                vrednos.Text = "Vrednost: " + podRacun.postavkeRacuna[i].c + "€";
                vrednos.Location = new Point(x, y+40);
                this.Controls.Add(vrednos);

                skVredArtiklovNeto += podRacun.postavkeRacuna[i].c;
                y+=70;
            }
            
            skVredArtiklovBruto = skVredArtiklovNeto * podRacun.DDV;
            skVredArtiklov = skVredArtiklovBruto + skVredArtiklovNeto;

            skVredArtiklovNeto = Math.Round(skVredArtiklovNeto, 2);
            skVredArtiklovBruto = Math.Round(skVredArtiklovBruto, 2);
            skVredArtiklov = Math.Round(skVredArtiklov, 2);

            nazivPod.Text += " " + podRacun.nazivPodjetja;
            naslovPod.Text += " " + podRacun.naslovPodjetja;
            postaPod.Text += " " + podRacun.postaPodjetja;

            idDDV.Text += " " + podRacun.idDDV;
            stRacun.Text += " " + podRacun.stRacuna;
            ddvStopnja.Text += " " + podRacun.DDV*100 + "%";
            netoVrednos.Text += " " + skVredArtiklovNeto + "€";
            ddv.Text += " " + skVredArtiklovBruto + "€";
            brutoVrednost.Text += " " + skVredArtiklov + "€";

            imeProd.Text += " " + podRacun.prodajalec;
            casIzdaje.Text += " " + podRacun.datum;
            ZOI.Text += " " + podRacun.ZOI;
            EOR.Text += " " + podRacun.EOR;
            skupajPlacilo.Text += " " + skVredArtiklov + "€";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            this.Hide();
            menu.Show();
        }

        private void btnZapri_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
