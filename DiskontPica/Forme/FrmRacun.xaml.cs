using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DiskontPica.Forme
{
    /// <summary>
    /// Interaction logic for FrmRacun.xaml
    /// </summary>
    public partial class FrmRacun : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmRacun()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public FrmRacun(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                string vratiKupce = @"Select id_kupca, ime_kupca+ ' ' +prezime_kupca as Kupac from Kupac";
                DataTable dtKupac = new DataTable();
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupce, konekcija);
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                dtKupac.Dispose(); daKupac.Dispose();
                string vratiZaposlene = @"Select id_zaposleni, ime_zaposleni + ' ' +  prezime_zaposleni as Zaposleni from Zaposleni";
                DataTable dtZaposleni = new DataTable();
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposlene, konekcija);
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                dtZaposleni.Dispose(); daZaposleni.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = konekcija

                }; ;
                command.Parameters.Add("@datum", SqlDbType.Date).Value = dpDatum.SelectedDate; ;
                command.Parameters.Add("@suma_za_uplatu", SqlDbType.Decimal).Value = txtSuma.Text;
                command.Parameters.Add("@valuta", SqlDbType.NChar).Value = txtValuta.Text;
                command.Parameters.Add("@id_kupac", SqlDbType.Int).Value = cbKupac.SelectedValue;
                command.Parameters.Add("@id_zaposleni", SqlDbType.Int).Value = cbZaposleni.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Racun Set datum=@datum, suma_za_uplatu=@suma_za_uplatu,valuta=@valuta, id_kupac=@id_kupac, id_zaposleni=@id_zaposleni Where id_racun = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Racun(datum, suma_za_uplatu,valuta, id_kupac, id_zaposleni) values (@datum, @suma_za_uplatu, @valuta, @id_kupac, @id_zaposleni)";
                }
                command.ExecuteNonQuery();
                command.Dispose();
                this.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("Unos vrednosti nije ispravan", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
