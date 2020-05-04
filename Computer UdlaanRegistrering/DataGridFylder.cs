using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Controls;

namespace Computer_UdlaanRegistrering
{
    class DataGridFylder
    {
        private SqlConnection con;
        private DataGrid grid;
        private string view;
        public DataGridFylder(SqlConnection c)
        {
            con = c;
        }
        public DataGrid getsetDataGrid
        {
            get { return grid; }
            set { grid = value; }
        }
        public string getsetView
        {
            get { return view; }
            set { view = value; }
        }
        public void Fyld()
        {
            SqlDataAdapter loadUdlaansda = new SqlDataAdapter();
            DataTable Udlaandt = new DataTable();
            loadUdlaansda.SelectCommand = new SqlCommand(view, con);
            loadUdlaansda.Fill(Udlaandt);
            grid.ItemsSource = Udlaandt.DefaultView;
        }
    }
}
