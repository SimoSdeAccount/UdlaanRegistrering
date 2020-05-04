using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace Computer_UdlaanRegistrering
{
    class ComboFylder
    {
        public ComboFylder(SqlConnection c)
        {
            con = c;
        }
        private SqlConnection con;
        private SqlCommand cmd;
        private ComboBox combo;
        private string dataKolonne;
        public SqlCommand getsetCmd
        {
            get { return cmd; }
            set { cmd = value; }
        }
        public ComboBox getsetCombo
        {
            get { return combo; }
            set { combo = value; }
        }
        public string getsetdataKolonne
        {
            get { return dataKolonne; }
            set { dataKolonne = value; }
        }
        public void FyldDropDownBoks()
        {
            cmd.Connection = con;
            con.Open();
            SqlDataReader readComboData = cmd.ExecuteReader();
            while (readComboData.Read())
            {
                combo.Items.Add(readComboData[dataKolonne].ToString());
            }
            readComboData.Close();
            con.Close();
        }
    }
}
