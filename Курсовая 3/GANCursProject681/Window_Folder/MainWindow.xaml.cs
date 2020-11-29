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
using System.Data.Entity;

namespace GANCursProject681
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ANDBEntities db = new ANDBEntities();
        int check;
        public MainWindow()
        {
            InitializeComponent();
            Tk(InfoLoadOfBd());
        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.Workers.ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Tk(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.Warehouse.ToList();
            }
            else
            {
                return;
            }
        }
        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryOfConsumables OgrEmpl = GridTable.SelectedItem as DeliveryOfConsumables;
            if (OgrEmpl != null)
            {
                check = db.DeliveryOfConsumables.Where(w => w.DateOfIssue == OgrEmpl.DateOfIssue && w.Quantity == OgrEmpl.Quantity).Select(s => s.id).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }
        private void Btn_Adm_Click(object sender, RoutedEventArgs e)
        {
            Window_Folder.Info_Watch_Or_Change OnfSotr = new Window_Folder.Info_Watch_Or_Change();
            Hide();
            OnfSotr.Show();
        }

        private void Btn_LowAdm_Click(object sender, RoutedEventArgs e)
        {
            Window_Folder.DeliverInfo Deliver = new Window_Folder.DeliverInfo();
            Hide();
            Deliver.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_Folder.InfoOfMaterial Material = new Window_Folder.InfoOfMaterial();
            Hide();
            Material.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window_Folder.MaterialOnWarehouse MON = new Window_Folder.MaterialOnWarehouse();
            Hide();
            MON.ShowDialog();
        }
    }
}
