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
    /// Interaction logic for FrmPice.xaml
    /// </summary>
    public partial class FrmPice : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmPice()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public FrmPice(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                string vratiVPica = @"Select id_vrsta, ime_proizvodjaca + ' ' + CAST(ambalaza AS VARCHAR) as Vrsta from VrstaPica";
                DataTable dtVPice = new DataTable();
                SqlDataAdapter daVPice = new SqlDataAdapter(vratiVPica, konekcija);
                daVPice.Fill(dtVPice);
                cbVrsta.ItemsSource = dtVPice.DefaultView;
                dtVPice.Dispose(); daVPice.Dispose();
               

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
               
                command.Parameters.Add("@ime_pica", SqlDbType.NVarChar).Value = txtIme.Text;
                command.Parameters.Add("@id_vrsta", SqlDbType.Int).Value = cbVrsta.SelectedValue;
                command.Parameters.Add("@cena", SqlDbType.Decimal).Value = txtCena.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Pice Set ime_pica=@ime_pica, id_vrsta=@id_vrsta, cena=@cena Where id_pica = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Pice(ime_pica, id_vrsta,cena) values (@ime_pica, @id_vrsta, @cena)";
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
