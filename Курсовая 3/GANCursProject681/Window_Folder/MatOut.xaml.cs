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
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для MatOut.xaml
    /// </summary>
    public partial class MatOut : Window
    {
        ANDBEntities db = new ANDBEntities();
        public MatOut()
        {
            InitializeComponent();
            Register();
        }
        public void Register()
        {
            ObservableCollection<Role> ItemsForRoles = new ObservableCollection<Role>();

            var query = db.Role.ToList();

            foreach (var roles in query)
            {
                ItemsForRoles.Add(
                    new Role { id = roles.id, Name = roles.Name }
                    );
                Cmbbx2.ItemsSource = ItemsForRoles;
            }
            Cmbbx2.ItemsSource = ItemsForRoles;
            ObservableCollection<Consumable> OFR = new ObservableCollection<Consumable>();

            var query2 = db.Consumable.ToList();

            foreach (var roles in query2)
            {
                OFR.Add(
                    new Consumable { id = roles.id, Name = roles.Name }
                    );
                Cmbbx1.ItemsSource = OFR;
            }
            Cmbbx1.ItemsSource = OFR;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoOfMaterial eas = new InfoOfMaterial();
            Hide();
            eas.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int idk = db.Role.Where(w => w.Name == Cmbbx2.Text).Select(s => s.id).FirstOrDefault();
            DeliveryOfConsumables DOC = new DeliveryOfConsumables()
            {
                FK_Consumable_id = db.Consumable.Where(w => w.Name == Cmbbx1.Text).Select(s => s.id).FirstOrDefault(),
                FK_Worker_id = db.Workers.Where(w => w.FirstName == Fname.Text && w.LastName == Lname.Text && w.FK_Role_id == idk).Select(s => s.id).FirstOrDefault(),
                Quantity = Convert.ToInt32(quant.Text),
                FK_StatusOfConsumableIssued_id = 1,
                DateOfIssue = DateTime.Now
            };
            db.DeliveryOfConsumables.Add(DOC);
            db.SaveChanges();
        }
    }
}
