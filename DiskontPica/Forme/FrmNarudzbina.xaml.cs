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
    /// Interaction logic for FrmNarudzbina.xaml
    /// </summary>
    public partial class FrmNarudzbina : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView pomocniRed;

        public FrmNarudzbina()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }

        public FrmNarudzbina(bool azuriraj, DataRowView pomocniRed)
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
                
                string vratiFakture = @"Select id_faktura, convert(nvarchar(MAX), datum_fakture, 20) + ' ' + CAST(suma_za_uplatu AS VARCHAR) as Podaci from Faktura";
                DataTable dtFaktura = new DataTable();
                SqlDataAdapter daFaktura = new SqlDataAdapter(vratiFakture, konekcija);
                daFaktura.Fill(dtFaktura);
                cbFaktura.ItemsSource = dtFaktura.DefaultView;
                dtFaktura.Dispose(); daFaktura.Dispose();
               
                string vratiPica = @"Select Pice.id_pica, ime_pica + ' '+  CAST(ambalaza AS VARCHAR) as ime_pica from Pice
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
                
                command.Parameters.Add("@kolicina", SqlDbType.Decimal).Value = txtKolicina.Text;
                command.Parameters.Add("@id_fakture", SqlDbType.Int).Value = cbFaktura.SelectedValue;
                command.Parameters.Add("@id_pica", SqlDbType.Int).Value = cbPice.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    command.Parameters.Add(@"id", SqlDbType.Int).Value = red["ID"];
                    command.CommandText = @"Update Narudzbina Set id_fakture=@id_fakture, kolicina=@kolicina, id_pica=@id_pica Where id_narudzbina = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    command.CommandText = @"insert into Narudzbina (kolicina, id_fakture, id_pica) values (@kolicina, @id_fakture, @id_pica)";
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
