using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для MaterialOnWarehouse.xaml
    /// </summary>
    public partial class MaterialOnWarehouse : Window
    {
        ANDBEntities db = new ANDBEntities();
        int check;
        public MaterialOnWarehouse()
        {
            InitializeComponent();
            TakingEmploye(InfoLoadOfBd());
        }

        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.SuppliesInDelivery.Include(i => i.Supply).Include(i => i.Supply.Warehouse).Include(i => i.Consumable).ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakingEmploye(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.SuppliesInDelivery.Include(i=>i.Supply).Include(i=>i.Supply.Warehouse).Include(i=>i.Consumable).ToList();
            }
            else
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewDelivery AND = new AddNewDelivery();
            Hide();
            AND.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Hide();

        }
    }
}
