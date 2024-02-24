using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiskontPica
{
    public class Konekcija
    {
        public SqlConnection KreirajKonekciju()
        {
            
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            { 
                DataSource = @"M\SQLEXPRESS", 
                InitialCatalog = "Diskont pica", 
                IntegratedSecurity = true 
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }
    }
}
