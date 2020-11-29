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
using System.Windows.Shapes;
using System.Data.Entity;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для DeliverInfo.xaml
    /// </summary>
    public partial class DeliverInfo : Window
    {
        ANDBEntities db = new ANDBEntities();
        int check;
        public DeliverInfo()
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
                GridTable.ItemsSource = db.TheSupplier.Include(i =>i.SuplierStatus).ToList();
            }
            else
            {
                return;
            }
        }
        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TheSupplier Works = GridTable.SelectedItem as TheSupplier;

            if (Works != null)
            {
                check = db.TheSupplier.Where(w => w.Name == Works.Name && w.ContactNumber == Works.ContactNumber && w.TheContactPerson == Works.TheContactPerson).Select(s => s.id).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewSupplier ans = new AddNewSupplier();
            ans.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (check > 0)
            {
                var UPDATE = db.TheSupplier.Where(w => w.id == check).FirstOrDefault();
                UPDATE.FK_SuplierStatus_id = 1;
                db.SaveChanges();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
