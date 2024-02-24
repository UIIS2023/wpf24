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
    /// Interaction logic for FrmKupovina.xaml
    /// </summary>
    public partial class FrmKupovina : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmKupovina()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public FrmKupovina(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }


        private void PopuniPadajuceListe()
        {
            //+' '+ 
            try
            {
                string vratiRacune = @"Select id_racun,  convert(nvarchar(MAX), datum, 20) + ' ' + CAST(suma_za_uplatu AS VARCHAR)  as podaci from Racun";
                DataTable dtRacun = new DataTable();
                SqlDataAdapter daRacun = new SqlDataAdapter(vratiRacune, konekcija);
                daRacun.Fill(dtRacun);
                cbRacun.ItemsSource = dtRacun.DefaultView;
                dtRacun.Dispose(); daRacun.Dispose();
                string vratiPica = @"Select id_pica, ime_pica + ' '+  CAST(ambalaza AS VARCHAR) as ime_pica from Pice
                                            join VrstaPica on Pice.id_vrsta = VrstaPica.id_vrsta";
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
              
                command.Parameters.Add("@kolicina", SqlDbType.Int).Value = txtKolicina.Text;
                command.Parameters.Add("@id_racun", SqlDbType.Int).Value = cbRacun.SelectedValue;
                command.Parameters.Add("@id_pica", SqlDbType.Int).Value = cbPice.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Kupovina Set id_racun=@id_racun, id_pica=@id_pica, kolicina=@kolicina Where id_kupovina = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Kupovina(id_racun, id_pica, kolicina) values (@id_racun, @id_pica, @kolicina)";
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
