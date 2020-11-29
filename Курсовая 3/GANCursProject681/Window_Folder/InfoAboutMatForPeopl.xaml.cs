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
    /// Логика взаимодействия для InfoAboutMatForPeopl.xaml
    /// </summary>
    public partial class InfoAboutMatForPeopl : Window
    {
        ANDBEntities db = new ANDBEntities();
        public InfoAboutMatForPeopl()
        {
            InitializeComponent();
            
        }

             public int idCon;
        public void loadinfo()
        {
            var info = db.DeliveryOfConsumables.
                    Include(i => i.Consumable).Include(i => i.Workers).
                    Include(i => i.StatusOfConsumableIssued).Include(i => i.Consumable.TypeOfConsumable).Include(i => i.Workers.Role).Where(w=>w.id == idCon).FirstOrDefault();
            LFMname.Text = info.Workers.LastName + " " + info.Workers.FirstName.Substring(0, 1) + "." + info.Workers.LastName.Substring(0, 1);
            Quat.Text = info.Quantity.ToString();
            NameMater.Text = info.Consumable.Name;
            Date.Text = info.DateOfIssue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Info_Watch_Or_Change ins = new Info_Watch_Or_Change();
            this.Hide();
            ins.Show();
        }
    }
}

