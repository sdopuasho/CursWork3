using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.Entity;
using System.Windows.Shapes;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для AddNewDelivery.xaml
    /// </summary>
    public partial class AddNewDelivery : Window
    {
        ANDBEntities db = new ANDBEntities();
        public AddNewDelivery()
        {
            InitializeComponent();
            Register();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MaterialOnWarehouse MOW = new MaterialOnWarehouse();
            MOW.Show();
            Hide();
        }
        public void Register()
        {
            ObservableCollection<TheSupplier> ItemsForRoles = new ObservableCollection<TheSupplier>();

            var query = db.TheSupplier.ToList();

            foreach (var roles in query)
            {
                ItemsForRoles.Add(
                    new TheSupplier { id = roles.id, Name = roles.Name }
                    );
                CmbbxSup.ItemsSource = ItemsForRoles;
            }
            CmbbxSup.ItemsSource = ItemsForRoles;

            ObservableCollection<Warehouse> OFR = new ObservableCollection<Warehouse>();

            var query2 = db.Warehouse.ToList();

            foreach (var roles in query2)
            {
                OFR.Add(
                    new Warehouse { id = roles.id, WarehouseNumber = roles.WarehouseNumber }
                    );
                CmbbxWare.ItemsSource = OFR;
            }
            CmbbxWare.ItemsSource = OFR;

            ObservableCollection<Consumable> OF2R = new ObservableCollection<Consumable>();

            var query3 = db.Consumable.ToList();

            foreach (var roles in query3)
            {
                OF2R.Add(
                    new Consumable { id = roles.id, Name = roles.Name }
                    );
                CmbbxCons.ItemsSource = OF2R;
            }
            CmbbxCons.ItemsSource = OF2R;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int idSup = db.TheSupplier.Where(w=>w.Name == CmbbxSup.Text).Select(s=>s.id).FirstOrDefault(), Wnum = Convert.ToInt32(CmbbxWare.Text);
            int idWarehouse = db.Warehouse.Where(w=>w.WarehouseNumber == Wnum).Select(s => s.id).FirstOrDefault();
            int idconsum = db.Consumable.Where(w=>w.Name == CmbbxCons.Text).Select(s => s.id).FirstOrDefault();
            Supply sup = new Supply()
            {
                FK_TheSuplier_id = idSup,
                FK_WarehouseNumber_id = idWarehouse,
                DateOfIssue = DateTime.Now
            };
            db.Supply.Add(sup);
            db.SaveChanges();
            int idsupp = db.Supply.Where(w => w.FK_WarehouseNumber_id == idWarehouse && w.FK_TheSuplier_id== idSup).Select(s => s.id).FirstOrDefault();
            SuppliesInDelivery supindev = new SuppliesInDelivery()
            {
                FK_Supply_id = idsupp,
                FK_Consumable_id = idconsum,
                DateOfManufacture = DateTime.Now.AddDays(3),
                Quantity = Convert.ToInt32( Quantity.Text)
            };
            db.SuppliesInDelivery.Add(supindev);
            db.SaveChanges();
            MessageBox.Show("Поставка добавлена");
        }
    }
}
