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
using System.Data.Entity;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для Info_Watch_Or_Change.xaml
    /// </summary>
    public partial class Info_Watch_Or_Change : Window
    {
        ANDBEntities db = new ANDBEntities();
        int check;
        Window_Folder.Sot_info_For_Any_Options inf = new Sot_info_For_Any_Options();
        public Info_Watch_Or_Change()
        {
            InitializeComponent();
            TakingEmploye(InfoLoadOfBd());
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
        public void TakingEmploye(bool check)
        {
            if (check == true)
            {
                GridTable.ItemsSource = db.Workers.Include(i => i.Role).ToList();
            }
            else
            {

            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

            if (check > 0)
            {
                inf.idSotr = check;
                inf.ConstructForm(0);
                inf.Fill();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void GridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Workers Works = GridTable.SelectedItem as Workers;

            if (Works != null)
            {
                check = db.Workers.Where(w => w.FirstName == Works.FirstName && w.LastName == Works.LastName && w.MiddleName == Works.MiddleName).Select(s => s.id).FirstOrDefault();
            }
            else
            {
                check = 0;
            }
        }

        private void Btn1_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (check > 0)
            {
                inf.idSotr = check;
                inf.ConstructForm(1);
                inf.Fill();
                inf.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            Window_Folder.AddNewSot ad = new AddNewSot();
            ad.ShowDialog();
            InfoLoadOfBd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
