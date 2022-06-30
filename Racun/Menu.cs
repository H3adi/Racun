namespace Racun
{
    public partial class Menu : Form
    {

        IzpisRacun izpisRacun = new IzpisRacun();

        public Menu()
        {
            InitializeComponent();

            izpisRacun.ChangeForm += () => Show();
            izpisRacun.FormClosed += (s, args) => Close();
        }

        private void btnIzpis_Click(object sender, EventArgs e)
        {
            this.Hide();
            izpisRacun.Show();
        }

        private void btnZapri_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
