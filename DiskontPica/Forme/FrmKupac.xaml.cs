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
    /// Interaction logic for FrmKupac.xaml
    /// </summary>
    public partial class FrmKupac : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmKupac()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmKupac(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
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
                command.Parameters.Add("@ime_kupca", SqlDbType.NVarChar).Value = txtIme.Text ;
                command.Parameters.Add("@prezime_kupca", SqlDbType.NVarChar).Value = txtPrezime.Text;
                command.Parameters.Add("@email_kupca", SqlDbType.NVarChar).Value = txtEmail.Text;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Kupac Set ime_kupca=@ime_kupca, prezime_kupca=@prezime_kupca, email_kupca=@email_kupca Where id_kupca = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Kupac(ime_kupca, prezime_kupca, email_kupca) values (@ime_kupca, @prezime_kupca, @email_kupca)";
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
