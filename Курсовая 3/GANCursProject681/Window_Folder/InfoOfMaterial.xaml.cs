using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для InfoOfMaterial.xaml
    /// </summary>
    public partial class InfoOfMaterial : Window
    {
        ANDBEntities db = new ANDBEntities();
        int check;
        InfoAboutMatForPeopl inf = new InfoAboutMatForPeopl();
        public InfoOfMaterial()
        {
            InitializeComponent();
            TakingRepair(true);
        }

        private void Btn_Ifno_Click(object sender, RoutedEventArgs e)
        {
            if (check > 0)
            {
                inf.idCon = check;
                inf.loadinfo();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }
        public bool InfoLoadOfBd()
        {
            var CheckForAnyLoad = db.DeliveryOfConsumables.ToList();
            if (CheckForAnyLoad.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TakingRepair(bool check)
        {
            if (check == true)
            {
                DataGridRepair.ItemsSource = db.DeliveryOfConsumables.
                    Include(i => i.Consumable).Include(i => i.Workers).
                    Include(i => i.StatusOfConsumableIssued).Include(i=>i.Consumable.TypeOfConsumable).Include(i => i.Workers.Role).ToList();
            }
            else
            {

            }
        }
        private void DataGridRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryOfConsumables OgrEmpl = DataGridRepair.SelectedItem as DeliveryOfConsumables;
            if (OgrEmpl != null)
            {
                check = db.DeliveryOfConsumables.Where(w => w.DateOfIssue == OgrEmpl.DateOfIssue && w.Quantity == OgrEmpl.Quantity).Select(s => s.id).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            var inforeapirtoupdate = db.DeliveryOfConsumables.Where(w => w.id == check).FirstOrDefault();
            if (check > 0)
            {
                var myupdate = db.DeliveryOfConsumables.Where(w => w.id == check).FirstOrDefault();
                myupdate.FK_StatusOfConsumableIssued_id = 3;
                db.SaveChanges();
            }
            else
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MatOut MO = new MatOut();
            Hide();
            MO.Show();
        }
    }
}
