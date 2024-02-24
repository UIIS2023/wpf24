using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace DiskontPica.Forme
{
    /// <summary>
    /// Interaction logic for FrmZaposleni.xaml
    /// </summary>
    public partial class FrmZaposleni : Window
    {

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;
        public FrmZaposleni()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmZaposleni(bool azuriraj, DataRowView pomocniRed)
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
                command.Parameters.Add("@ime_zaposleni", SqlDbType.NVarChar).Value = txtIme.Text;
                command.Parameters.Add("@prezime_zaposleni", SqlDbType.NVarChar).Value = txtPrezime.Text;
                command.Parameters.Add("@JMBG", SqlDbType.NChar).Value = txtJMBG.Text;
                command.Parameters.Add("@email_zaposleni", SqlDbType.NVarChar).Value = txtEmail.Text;


                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Zaposleni Set ime_zaposleni=@ime_zaposleni, prezime_zaposleni=@prezime_zaposleni, JMBG=@JMBG, email_zaposleni=@email_zaposleni Where id_zaposleni = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"Insert into Zaposleni(ime_zaposleni, prezime_zaposleni, JMBG, email_zaposleni) values (@ime_zaposleni, @prezime_zaposleni, @JMBG, @email_zaposleni)";
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
