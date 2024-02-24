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
    /// Interaction logic for FrmVrstaPica.xaml
    /// </summary>
    public partial class FrmVrstaPica : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmVrstaPica()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

        public FrmVrstaPica(bool azuriraj, DataRowView pomocniRed)
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
                command.Parameters.Add("@ime_proizvodjaca", SqlDbType.NVarChar).Value = txtIme.Text; ;
                command.Parameters.Add("@ambalaza", SqlDbType.Decimal).Value = txtAmbalaza.Text;
                command.Parameters.Add("@alkohol", SqlDbType.Bit).Value = cbAlkohol.IsChecked;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update VrstaPica Set ime_proizvodjaca=@ime_proizvodjaca, ambalaza=@ambalaza, alkohol=@alkohol Where id_vrsta = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into VrstaPica (ime_proizvodjaca, ambalaza, alkohol) values (@ime_proizvodjaca, @ambalaza, @alkohol)";
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
