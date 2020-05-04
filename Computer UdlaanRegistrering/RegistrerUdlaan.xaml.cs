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
    /// Interaction logic for RegistrerUdlaan.xaml
    /// </summary>
    public partial class RegistrerUdlaan : Page
    {
        SqlConnection con;
        public RegistrerUdlaan()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrerUdlaanCon"].ConnectionString);
            FyldGrids();
            FyldMustype();
        }
        private void loadGrid(string view, DataGridFylder grid)
        {
            grid.getsetView = view;

            grid.Fyld();
        }
        private void FyldGrids()
        {
            string[] viewQueries = { "SELECT * FROM EleverView", "SELECT * FROM ComputerView"};
            DataGridFylder[] GridFyldere = {new DataGridFylder(con), new DataGridFylder(con)};
            DataGrid[] Grids = { ElevGrid, ComputerGrid };
            for (int i = 0; i < Grids.Length; i++)
            {
                GridFyldere[i].getsetDataGrid = Grids[i];
                loadGrid(viewQueries[i], GridFyldere[i]);
            }
        }
        private void FyldMustype()
        {
            string musTyper = "SELECT Mustype FROM Mus;";
            string musTypeKolonne = "Mustype";
            ComboFylder fyldCombo = new ComboFylder(con);
            fyldCombo.getsetCombo = Mustype;
            fyldCombo.getsetCmd = new SqlCommand(musTyper);
            fyldCombo.getsetdataKolonne = musTypeKolonne;
            fyldCombo.FyldDropDownBoks();
        }
        private void ElevGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestLabel01.Content = (ElevGrid.SelectedCells[0].Column.GetCellContent(ElevGrid.SelectedItem) as TextBlock).Text;
        }
        private void ComputerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestLabel02.Content = (ComputerGrid.SelectedCells[0].Column.GetCellContent(ComputerGrid.SelectedItem) as TextBlock).Text;
        }

        private void Registrer_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateFromString = DateTime.ParseExact(UdløbsDato.Text, "dd-MM-yyyy", null);
            string InsertUdlaanQuery = "INSERT INTO Udlån (Elevnummer, Mus, Udlånsdato, Udløbsdato, Lånstatus) OUTPUT INSERTED.Id VALUES (@Elevnummer, @Mus, @Udlaan, @Udloeb, @Laanstatus);";
            SqlCommand insertUdlaanCmd = new SqlCommand(InsertUdlaanQuery, con);
            insertUdlaanCmd.Parameters.AddWithValue("@Elevnummer", (ElevGrid.SelectedCells[0].Column.GetCellContent(ElevGrid.SelectedItem) as TextBlock).Text);
            insertUdlaanCmd.Parameters.AddWithValue("@Mus", Mustype.Text);
            insertUdlaanCmd.Parameters.AddWithValue("@Udlaan", DateTime.Today.ToString("dd-MM-yyyy"));
            insertUdlaanCmd.Parameters.AddWithValue("@Udloeb", dateFromString.ToString());
            insertUdlaanCmd.Parameters.AddWithValue("@Laanstatus", "Aktiv");
            con.Open();
            string UdlaanId = insertUdlaanCmd.ExecuteScalar().ToString();
            con.Close();
            string InsertUdlaanComputerQuery = "INSERT INTO UdlånComputere (UdlånId, Computernummer, Computerantal) VALUES (@UdlånId, @ComputerNummer, @ComputerAntal);";
            SqlCommand insertUdlaanComputerCmd = new SqlCommand(InsertUdlaanComputerQuery, con);
            insertUdlaanComputerCmd.Parameters.AddWithValue("@UdlånId", UdlaanId);
            insertUdlaanComputerCmd.Parameters.AddWithValue("@ComputerNummer", (ComputerGrid.SelectedCells[0].Column.GetCellContent(ComputerGrid.SelectedItem) as TextBlock).Text);
            insertUdlaanComputerCmd.Parameters.AddWithValue("@ComputerAntal", ComputerAntal.Text);
            con.Open();
            insertUdlaanComputerCmd.ExecuteNonQuery();
            con.Close();


        }
    }
}
