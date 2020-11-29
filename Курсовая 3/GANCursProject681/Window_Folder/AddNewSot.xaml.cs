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
    /// Логика взаимодействия для AddNewSot.xaml
    /// </summary>
    public partial class AddNewSot : Window
    {
        ANDBEntities db = new ANDBEntities();
        Class_Folder.DB_Work DBW = new Class_Folder.DB_Work();
        public AddNewSot()
        {
            InitializeComponent();
            Register();
            PpTxt.Visibility = Visibility.Hidden;
            popup1.IsOpen = false;
        }
        public int RandomToPassword()
        {
            Random rand = new Random();
            return rand.Next(100, 999);
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
                CmbBox_Role.ItemsSource = ItemsForRoles;
            }
            CmbBox_Role.ItemsSource = ItemsForRoles;
        }

        public bool AddUser(string fnm, string lnm, string mnm, string phn, string eml, string orgempl)
        {
            string LoginForAddUser = LNameTxt.Text.Substring(0, 2) + FNameTxt.Text.Substring(0, 2) + MNameTxt.Text.Substring(0, 2) + PhoneTxt.Text.Substring(PhoneTxt.Text.Length - 2)
                , PasswordForAddUser = PhoneTxt.Text.Substring(0, 2) + RandomToPassword() + PhoneTxt.Text.Substring(PhoneTxt.Text.Length - 2);
            int posit = DBW.ReturnidRoleForRegistration(orgempl);
            if (DBW.ReturnidForFullInfo(fnm, lnm, mnm, phn) == 0)
            {
                Workers organizationEmployee = new Workers()
                {
                    FirstName = fnm,
                    LastName = lnm,
                    MiddleName = mnm,
                    ContactNumber = phn,
                    Email = eml,
                    Login = LoginForAddUser.ToLower(),
                    Password = PasswordForAddUser,
                    FK_Role_id = posit
                };
                db.Workers.Add(organizationEmployee);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FNameTxt.Text == "" && LNameTxt.Text == "" && MNameTxt.Text == "" && PhoneTxt.Text == "" && EmailTxt.Text == "" && CmbBox_Role.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Все поля пусты";
            }
            else if (FNameTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите имя";
            }
            else if (LNameTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите фамилию";
            }
            else if (MNameTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите отчество";
            }
            else if (PhoneTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите номер телефона";
            }
            else if (EmailTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите Email";
            }
            else if (CmbBox_Role.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Выбирите должность";
            }
            else
            {
                if (AddUser(FNameTxt.Text, LNameTxt.Text, MNameTxt.Text, PhoneTxt.Text, EmailTxt.Text, CmbBox_Role.Text) == true)
                {
                    MessageBox.Show("Пользователь добавлен");
                }
                else
                {
                    PpTxt.Visibility = Visibility.Visible;
                    popup1.IsOpen = true;
                    PpTxt.Text = "Пользователь уже существует";
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Info_Watch_Or_Change me = new
                Info_Watch_Or_Change();
            this.Close();
            me.Show();
        }
    }
}
