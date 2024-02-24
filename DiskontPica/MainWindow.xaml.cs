using DiskontPica.Forme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiskontPica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        string ucitanaTabela;
        bool azuriraj;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();


        string kupacSelect = @"Select id_kupca as ID, ime_kupca, prezime_kupca, email_kupca From Kupac;";
        static string zaposleniSelect = @"Select id_zaposleni as ID, ime_zaposleni, prezime_zaposleni, JMBG, email_zaposleni  from Zaposleni";
        static string dobavljacSelect = @"Select id_dobavljac as ID,ime_kompanije, pib, sifra_delatnosti from Dobavljac";
        static string narudzbinaSelect = @"Select id_narudzbina as ID, kolicina, datum_fakture, suma_za_uplatu, ime_pica from Narudzbina
                                                    join Faktura on Narudzbina.id_fakture = Faktura.id_faktura
                                                    join Pice on Narudzbina.id_pica = Pice.id_pica";
                                                   
                                                
        static string kupovinaSelect = @"Select id_kupovina as ID, kolicina, datum, suma_za_uplatu, ime_pica from Kupovina
                                                    join Racun on Kupovina.id_racun = Racun.id_racun
                                                    join Pice on Kupovina.id_pica = Pice.id_pica";
                                                    
        static string racunSelect = @"Select id_racun as ID, datum, suma_za_uplatu as IZNOS, valuta, ime_zaposleni + ' ' + prezime_zaposleni as ZAPOSLENI, 
                                    ime_kupca + ' ' + prezime_kupca as KUPAC from Racun
                                                    join Zaposleni on Racun.id_zaposleni = Zaposleni.id_zaposleni
                                                    join Kupac on Racun.id_kupac = Kupac.id_kupca";
        static string fakturaSelect = @"Select id_faktura as ID, datum_fakture as DATUM, suma_za_uplatu as IZNOS, ime_kompanije as KOMPANIJA,
                                        ime_zaposleni + ' ' + prezime_zaposleni as ZAPOSLENI from Faktura
                                                    join Zaposleni on Faktura.id_zaposleni = Zaposleni.id_zaposleni
                                                    join Dobavljac on Faktura.id_dobavljac = Dobavljac.id_dobavljac";

        static string piceSelect = @"Select id_pica as ID, ime_pica,ambalaza, cena, ime_proizvodjaca from Pice
                                                    join VrstaPica on Pice.id_vrsta = VrstaPica.id_vrsta";
        static string vrstaPicaSelect = @"Select id_vrsta as ID, ime_proizvodjaca, ambalaza, alkohol from VrstaPica";
        static string zaliheSelect = @"Select id_zalihe as ID, ime_pica, kolicina from Zalihe
                                                    join Pice on Zalihe.id_pica = Pice.id_pica";


        //Select sa uslovom
        string selectUslovKupac = @"Select * from Kupac where id_kupca=";
        string selectUslovZaposleni = @"Select * from Zaposleni where id_zaposleni=";
        string selectUslovDobavljac = @"Select * from Dobavljac where id_dobavljac=";
        string selectUslovNarudzbina = @"Select * from Narudzbina where id_narudzbina=";
        string selectUslovKupovina = @"Select * from Kupovina where id_kupovina=";
        string selectUslovRacun = @"Select * from Racun where id_racun=";
        string selectUslovFaktura = @"Select * from Faktura where id_faktura=";
        string selectUslovPice = @"Select * from Pice where id_pica=";
        string selectUslovVrstaPica = @"Select * from VrstaPica where id_vrsta=";
        string selectUslovZalihe = @"Select * from Zalihe where id_zalihe=";


        //Delete upiti
        static string deleteKupac = @"delete from Kupac where id_kupca=";
        static string deleteZaposleni = @"delete from Zaposleni where id_zaposleni=";
        static string deleteDobavljac = @"delete from Dobavljac where id_dobavljac=";
        static string deleteNarudzbina = @"delete from Narudzbina where id_narudzbina=";
        static string deleteKupovina = @"delete from Kupovina where id_kupovina=";
        static string deleteRacun = @"delete from Racun where id_racun=";
        static string deleteFaktura = @"delete from Faktura where id_faktura=";
        static string deletePice = @"delete from Pice where id_pica=";
        static string deleteVrstaPica = @"delete from VrstaPica where id_vrsta=";
        static string deleteZalihe = @"delete from Zalihe where id_zalihe=";

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, kupacSelect);
        }


        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspešno učitani podaci.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, kupacSelect);
        }

        private void btnZaposeleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zaposleniSelect);
        }

        private void btnDobavljac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, dobavljacSelect);
        }

        private void btnNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, narudzbinaSelect);
        }

        private void btnKupovina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, kupovinaSelect);
        }

        private void btnRacun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, racunSelect);
        }

        private void btnFaktura_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, fakturaSelect);
        }

        private void btnZalihe_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zaliheSelect);
        }

        private void btnVrstePica_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, vrstaPicaSelect);
        }

        private void btnPica_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, piceSelect);
        }

        private void btnDODAJ_Click(object sender, RoutedEventArgs e)
        {
            Window prozorcic;

            if (ucitanaTabela == kupacSelect)
            {
                prozorcic = new FrmKupac();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, kupacSelect);
            }
            if (ucitanaTabela == zaposleniSelect)
            {
                prozorcic = new FrmZaposleni();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            if (ucitanaTabela == dobavljacSelect)
            {
                prozorcic = new FrmDobavljac();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, dobavljacSelect);
            }
            if (ucitanaTabela == narudzbinaSelect)
            {
                prozorcic = new FrmNarudzbina();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, narudzbinaSelect);
            }
            if (ucitanaTabela == kupovinaSelect)
            {
                prozorcic = new FrmKupovina();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, kupovinaSelect);
            }
            if (ucitanaTabela == racunSelect)
            {
                prozorcic = new FrmRacun();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            if (ucitanaTabela == fakturaSelect)
            {
                prozorcic = new FrmFaktura();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, fakturaSelect);
            }
            if (ucitanaTabela == piceSelect)
            {
                prozorcic = new FrmPice();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, piceSelect);
            }
            if (ucitanaTabela == vrstaPicaSelect)
            {
                prozorcic = new FrmVrstaPica();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, vrstaPicaSelect);
            }
            if (ucitanaTabela == zaliheSelect)
            {
                prozorcic = new FrmZalihe();
                prozorcic.ShowDialog();
                UcitajPodatke(dataGridCentralni, zaliheSelect);
            }

               

        }


        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                
                SqlCommand command = new SqlCommand
                {
                    Connection = konekcija
                };
                command.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];

                command.CommandText = selectUslov + "@id";
                SqlDataReader citaj = command.ExecuteReader();
                command.Dispose();
                while (citaj.Read())
                {
                    if (ucitanaTabela == kupacSelect)
                    {
                        FrmKupac prozorcicKupac = new FrmKupac(azuriraj, red);
                        prozorcicKupac.txtIme.Text = citaj["ime_kupca"].ToString();
                        prozorcicKupac.txtPrezime.Text = citaj["prezime_kupca"].ToString();
                        prozorcicKupac.txtEmail.Text = citaj["email_kupca"].ToString();
                        prozorcicKupac.ShowDialog();
                    }
                    if (ucitanaTabela == zaposleniSelect)
                    {
                        FrmZaposleni prozorcicZaposleni = new FrmZaposleni(azuriraj, red);
                        prozorcicZaposleni.txtIme.Text = citaj["ime_zaposleni"].ToString();
                        prozorcicZaposleni.txtPrezime.Text = citaj["prezime_zaposleni"].ToString();
                        prozorcicZaposleni.txtJMBG.Text = citaj["JMBG"].ToString();
                        prozorcicZaposleni.txtEmail.Text = citaj["email_zaposleni"].ToString();
                        prozorcicZaposleni.ShowDialog();
                    }
                    if (ucitanaTabela == dobavljacSelect)
                    {
                        FrmDobavljac prozorcicDobavljac = new FrmDobavljac(azuriraj, red);
                        prozorcicDobavljac.txtIme.Text = citaj["ime_kompanije"].ToString();
                        prozorcicDobavljac.txtPib.Text = citaj["pib"].ToString();
                        prozorcicDobavljac.txtSifra.Text = citaj["sifra_delatnosti"].ToString();
                        prozorcicDobavljac.ShowDialog();
                    }
                    if (ucitanaTabela == narudzbinaSelect)
                    {
                        FrmNarudzbina prozorcicNarudzbina = new FrmNarudzbina(azuriraj, red);
                        prozorcicNarudzbina.cbFaktura.SelectedValue = citaj["id_fakture"].ToString();
                        prozorcicNarudzbina.cbPice.SelectedValue = citaj["id_pica"].ToString();
                        prozorcicNarudzbina.txtKolicina.Text = citaj["kolicina"].ToString();
                        prozorcicNarudzbina.ShowDialog();
                    }
                    if (ucitanaTabela == kupovinaSelect)
                    {
                        FrmKupovina prozorcicKupovina = new FrmKupovina(azuriraj, red);
                        prozorcicKupovina.txtKolicina.Text = citaj["kolicina"].ToString();
                        prozorcicKupovina.cbRacun.SelectedValue = citaj["id_racun"].ToString();
                        prozorcicKupovina.cbPice.SelectedValue = citaj["id_pica"].ToString();
                        prozorcicKupovina.ShowDialog();
                    }
                    if (ucitanaTabela == racunSelect)
                    {
                        FrmRacun prozorcicRacun = new FrmRacun(azuriraj, red);
                        prozorcicRacun.txtSuma.Text = citaj["suma_za_uplatu"].ToString();
                        prozorcicRacun.txtValuta.Text = citaj["valuta"].ToString();
                        prozorcicRacun.cbKupac.SelectedValue = citaj["id_kupac"].ToString();
                        prozorcicRacun.cbZaposleni.SelectedValue = citaj["id_zaposleni"].ToString();
                        prozorcicRacun.dpDatum.SelectedDate = (DateTime)citaj["datum"];
                        prozorcicRacun.ShowDialog();
                    }
                    if (ucitanaTabela == fakturaSelect)
                    {
                        FrmFaktura prozorcicFaktura = new FrmFaktura(azuriraj, red);
                        prozorcicFaktura.txtSuma.Text = citaj["suma_za_uplatu"].ToString();
                        prozorcicFaktura.cbDobavljac.SelectedValue = citaj["id_dobavljac"].ToString();
                        prozorcicFaktura.cbZaposleni.SelectedValue = citaj["id_zaposleni"].ToString();
                        prozorcicFaktura.dpDatum.SelectedDate = (DateTime) citaj["datum_fakture"];
                        prozorcicFaktura.ShowDialog();
                    }
                    if (ucitanaTabela == piceSelect)
                    {
                        FrmPice prozorcicPice = new FrmPice(azuriraj, red);
                        prozorcicPice.txtIme.Text = citaj["ime_pica"].ToString();
                        prozorcicPice.txtCena.Text = citaj["cena"].ToString();
                        prozorcicPice.cbVrsta.SelectedValue = citaj["id_vrsta"].ToString();
                        prozorcicPice.ShowDialog();
                    }
                    if (ucitanaTabela == vrstaPicaSelect)
                    {
                        FrmVrstaPica prozorcicVrstaPica = new FrmVrstaPica(azuriraj, red);
                        prozorcicVrstaPica.txtIme.Text = citaj["ime_proizvodjaca"].ToString();
                        prozorcicVrstaPica.txtAmbalaza.Text = citaj["ambalaza"].ToString();
                        prozorcicVrstaPica.cbAlkohol.IsChecked = (Boolean)citaj["alkohol"];
                        prozorcicVrstaPica.ShowDialog();
                    }
                    if (ucitanaTabela == zaliheSelect)
                    {
                        FrmZalihe prozorcicZalihe = new FrmZalihe(azuriraj, red);
                        prozorcicZalihe.txtKolicina.Text = citaj["kolicina"].ToString();
                        prozorcicZalihe.cbPice.SelectedValue = citaj["id_pica"].ToString();
                        prozorcicZalihe.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
                azuriraj = false;
            }
        }

        private void btnIZMENI_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela == kupacSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovKupac);
                UcitajPodatke(dataGridCentralni, kupacSelect);
            }
            else if (ucitanaTabela == zaposleniSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovZaposleni);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            else if (ucitanaTabela == dobavljacSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovDobavljac);
                UcitajPodatke(dataGridCentralni, dobavljacSelect);
            }
            else if (ucitanaTabela == narudzbinaSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovNarudzbina);
                UcitajPodatke(dataGridCentralni, narudzbinaSelect);
            }
            else if (ucitanaTabela == kupovinaSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovKupovina);
                UcitajPodatke(dataGridCentralni, kupovinaSelect);
            }
            else if (ucitanaTabela == racunSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovRacun);
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            else if (ucitanaTabela == fakturaSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovFaktura);
                UcitajPodatke(dataGridCentralni, fakturaSelect);
            }
            else if (ucitanaTabela == piceSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovPice);
                UcitajPodatke(dataGridCentralni, piceSelect);
            }
            else if (ucitanaTabela == vrstaPicaSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovVrstaPica);
                UcitajPodatke(dataGridCentralni, vrstaPicaSelect);
            }
            else if (ucitanaTabela == zaliheSelect)
            {
                PopuniFormu(dataGridCentralni, selectUslovZalihe);
                UcitajPodatke(dataGridCentralni, zaliheSelect);
            }
        }

        private void btnOBRISI_Click(object sender, RoutedEventArgs e)
        {

            if (ucitanaTabela == kupacSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteKupac);
                UcitajPodatke(dataGridCentralni, kupacSelect);
            }
            else if (ucitanaTabela == zaposleniSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteZaposleni);
                UcitajPodatke(dataGridCentralni, zaposleniSelect);
            }
            else if (ucitanaTabela == dobavljacSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteDobavljac);
                UcitajPodatke(dataGridCentralni, dobavljacSelect);
            }
            else if (ucitanaTabela == narudzbinaSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteNarudzbina);
                UcitajPodatke(dataGridCentralni, narudzbinaSelect);
            }
            else if (ucitanaTabela == kupovinaSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteKupovina);
                UcitajPodatke(dataGridCentralni, kupovinaSelect);
            }
            else if (ucitanaTabela == racunSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteRacun);
                UcitajPodatke(dataGridCentralni, racunSelect);
            }
            else if (ucitanaTabela == fakturaSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteFaktura);
                UcitajPodatke(dataGridCentralni, fakturaSelect);
            }
            else if (ucitanaTabela == piceSelect)
            {
                ObrisiZapis(dataGridCentralni, deletePice);
                UcitajPodatke(dataGridCentralni, piceSelect);
            }
            else if (ucitanaTabela == vrstaPicaSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteVrstaPica);
                UcitajPodatke(dataGridCentralni, vrstaPicaSelect);
            }
            else if (ucitanaTabela == zaliheSelect)
            {
                ObrisiZapis(dataGridCentralni, deleteZalihe);
                UcitajPodatke(dataGridCentralni, zaliheSelect);
            }
        }
        private void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];

                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand komanda = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    komanda.CommandText = deleteUpit + "@id";
                    komanda.ExecuteNonQuery();
                    komanda.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }
    }
}
