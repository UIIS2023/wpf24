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
    /// Interaction logic for FrmFaktura.xaml
    /// </summary>
    public partial class FrmFaktura : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmFaktura()
        {
            InitializeComponent();
            dpDatum.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public FrmFaktura(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            dpDatum.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }


        private void PopuniPadajuceListe()
        {
            try {
                string vratiDobavljace = @"Select id_dobavljac, ime_kompanije from Dobavljac";
                DataTable dtDobavljac = new DataTable();
                SqlDataAdapter daDobavljac = new SqlDataAdapter(vratiDobavljace, konekcija);
                daDobavljac.Fill(dtDobavljac);
                cbDobavljac.ItemsSource = dtDobavljac.DefaultView;
                dtDobavljac.Dispose(); daDobavljac.Dispose();
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
                if (konekcija != null) {
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
                command.Parameters.Add("@datum_fakture", SqlDbType.Date).Value = dpDatum.SelectedDate; ;
                command.Parameters.Add("@suma_za_uplatu", SqlDbType.Decimal).Value = txtSuma.Text;
                command.Parameters.Add("@id_dobavljac", SqlDbType.Int).Value = cbDobavljac.SelectedValue;
                command.Parameters.Add("@id_zaposleni", SqlDbType.Int).Value = cbZaposleni.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Faktura Set datum_fakture=@datum_fakture, suma_za_uplatu=@suma_za_uplatu, id_dobavljac=@id_dobavljac, id_zaposleni=@id_zaposleni Where id_faktura = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Faktura(datum_fakture, suma_za_uplatu, id_dobavljac, id_zaposleni) values (@datum_fakture, @suma_za_uplatu, @id_dobavljac, @id_zaposleni)";
                }
                command.ExecuteNonQuery();
                command.Dispose();
                this.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("Unos vrednosti nije ispravan", "Greska", MessageBoxButton.OK,MessageBoxImage.Error);

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
