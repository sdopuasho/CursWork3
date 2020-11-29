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
using System.Windows.Shapes;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для AddNewMat.xaml
    /// </summary>
    public partial class AddNewMat : Window
    {
        ANDBEntities db = new ANDBEntities();
        public AddNewMat()
        {
            InitializeComponent();
            loadinfo();
        }
        public void loadinfo()
        {
            ObservableCollection<TypeOfConsumable> Items = new ObservableCollection<TypeOfConsumable>();

            var query = db.TypeOfConsumable.ToList();

            foreach (var Rls in query)
            {
                Items.Add(
                    new TypeOfConsumable { id = Rls.id, Name = Rls.Name }
                    );
                Typemat.ItemsSource = Items;
            }
            Typemat.ItemsSource = Items;
            ObservableCollection<Unit> Items1 = new ObservableCollection<Unit>();

            var query1 = db.Unit.ToList();

            foreach (var Rls in query1)
            {
                Items1.Add(
                    new Unit { id = Rls.id, Name = Rls.Name }
                    );
                unitmat.ItemsSource = Items1;
            }
            unitmat.ItemsSource = Items1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoOfMaterial infoOf = new InfoOfMaterial();
            this.Hide();
            infoOf.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Consumable cons = new Consumable()
            {
                Name = Namemat.Text,
                InventoryNumber = Convert.ToInt32(InventNum.Text),
                FK_Unit_id = Convert.ToInt32(db.Unit.Where(w => w.Name == unitmat.Text).Select(s => s.id).FirstOrDefault()),
                FK_TypeOfConsumable_id = Convert.ToInt32(db.TypeOfConsumable.Where(w => w.Name == Typemat.Text).Select(s => s.id).FirstOrDefault())
            };
        }
    }
}
