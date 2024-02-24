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
    /// Interaction logic for FrmDobavljac.xaml
    /// </summary>
    public partial class FrmDobavljac : Window
    { 
    SqlConnection konekcija = new SqlConnection();
    Konekcija kon = new Konekcija();
    private bool azuriraj;
    private DataRowView pomocniRed;
    
        public FrmDobavljac()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }

    public FrmDobavljac(bool azuriraj, DataRowView pomocniRed)
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
                command.Parameters.Add("@ime_kompanije", SqlDbType.NVarChar).Value = txtIme.Text; ;
                command.Parameters.Add("@pib", SqlDbType.NChar).Value = txtPib.Text;
                command.Parameters.Add("@sifra_delatnosti", SqlDbType.NChar).Value = txtSifra.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Dobavljac Set ime_kompanije=@ime_kompanije, pib=@pib, sifra_delatnosti=@sifra_delatnosti Where id_dobavljac = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"Insert into Dobavljac(ime_kompanije, pib, sifra_delatnosti) values (@ime_kompanije, @pib, @sifra_delatnosti)";
                }
                command.ExecuteNonQuery();
                command.Dispose();
                this.Close();


            }
            catch (SqlException)
            {
                MessageBox.Show("Unos vrednosti nije ispravan cccc", "Greska", MessageBoxButton.OK,MessageBoxImage.Error);

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
