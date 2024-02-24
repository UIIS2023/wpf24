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
    /// Interaction logic for FrmZalihe.xaml
    /// </summary>
    public partial class FrmZalihe : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmZalihe()
        {
            InitializeComponent();;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            
        }

        public FrmZalihe(bool azuriraj, DataRowView pomocniRed)
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
                string vratiPica = @"Select id_pica, ime_pica from Pice";
                DataTable dtPice = new DataTable();
                SqlDataAdapter daPice = new SqlDataAdapter(vratiPica, konekcija);
                daPice.Fill(dtPice);
                cbPice.ItemsSource = dtPice.DefaultView;
                dtPice.Dispose(); daPice.Dispose();
               
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
                command.Parameters.Add("@id_pica", SqlDbType.Int).Value = cbPice.SelectedValue;
                command.Parameters.Add("@kolicina", SqlDbType.Int).Value = txtKolicina.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Zalihe Set id_pica=@id_pica, kolicina=@kolicina Where id_zalihe = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Zalihe(id_pica, kolicina) values (@id_pica, @kolicina)";
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
