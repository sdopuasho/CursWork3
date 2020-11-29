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

namespace GANCursProject681.Window_Folder
{
    /// <summary>
    /// Логика взаимодействия для AddNewSupplier.xaml
    /// </summary>
    public partial class AddNewSupplier : Window
    {
        ANDBEntities db = new ANDBEntities();
        public AddNewSupplier()
        {
            InitializeComponent();
        }
        public int added()
        {
            if(NameOrg.Text == "" && ContPhone.Text == "" && ContPers.Text == "" && Email.Text == "" && Adress.Text == "")
            {
                return 0;
            }
            if (ContPhone.Text == "")
            {
                return 0;
            }
            if (ContPers.Text == "")
            {
                return 0;

            }
            if (Email.Text == "")
            {
                return 0;
            }
            if (Adress.Text == "")
            {
                return 0;
            }
            if (NameOrg.Text == "")
            {
                return 0;
            }
            else
            {
                TheSupplier sup = new TheSupplier()
                {
                    Name = NameOrg.Text,
                    ContactNumber = ContPhone.Text,
                    TheContactPerson = ContPers.Text,
                    Email = Email.Text,
                    LegalAddress = Adress.Text
                };
                db.TheSupplier.Add(sup);
                db.SaveChanges();
                MessageBox.Show("Новый поставщик добавлен");
                return 1;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeliverInfo dev = new DeliverInfo();
            this.Hide();
            dev.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            added();
        }
    }
}
