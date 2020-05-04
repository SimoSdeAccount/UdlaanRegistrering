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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Computer_UdlaanRegistrering
{
    /// <summary>
    /// Interaction logic for Oversigter.xaml
    /// </summary>
    public partial class Oversigter : Page
    {
        DataGridFylder grid;
        public Oversigter()
        {
            InitializeComponent();
            grid = new DataGridFylder(new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrerUdlaanCon"].ConnectionString));
            grid.getsetDataGrid = OversigtGrid;
        }
        private void loadGrid(string view)
        {
            grid.getsetView = view;
            grid.Fyld();
        }
        private void NuvUdlaan_Click(object sender, RoutedEventArgs e)
        {
            loadGrid("SELECT Elevnavn, Udløbsdato FROM UdlaanView WHERE Lånstatus = 'Aktiv' ");
        }
        private void OverskredetUdlaan_Click(object sender, RoutedEventArgs e)
        {
            loadGrid("SELECT Elevnavn, Udløbsdato FROM UdlaanView WHERE Lånstatus = 'Aktiv' AND Udløbsdato < GETDATE();");
        }
        private void TilgaengComp_Click(object sender, RoutedEventArgs e)
        {
            loadGrid("SELECT * FROM TilgaengeligComputere");
        }
        private void AlleUdlaan_Click(object sender, RoutedEventArgs e)
        {
            loadGrid("SELECT * FROM UdlaanView");
        }


    }
}
