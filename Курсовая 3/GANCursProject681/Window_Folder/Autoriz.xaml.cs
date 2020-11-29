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
    /// Логика взаимодействия для Autoriz.xaml
    /// </summary>
    public partial class Autoriz : Window
    {
        ANDBEntities db = new ANDBEntities();
        Class_Folder.DB_Work BDWork = new Class_Folder.DB_Work();
        Class_Folder.InfoSave IS = new Class_Folder.InfoSave();
        public Autoriz()
        {
            MessageBox.Show("Данные пользователя изменены");
            MessageBox.Show("Пользователь добавлен");
            MessageBox.Show("Поставщик добавлен");
            MessageBox.Show("Информация о поставке добаленна");
            InitializeComponent();
            popup1.IsOpen = false;
            PpTxt.Visibility = Visibility.Hidden;
        }
        public void testforanyoshib()
        {
            if (LoginTxt.Text == "" && PswrdBox.Password == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите Логин и пароль";
            }
            else if (LoginTxt.Text != "" && PswrdBox.Password == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите пароль";
            }
            else if (LoginTxt.Text == "" && PswrdBox.Password != "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Введите логин";
            }
            else
            {
                int idLogin = BDWork.returnidlog(LoginTxt.Text);

                if (idLogin > 0)
                {
                    int tempid = BDWork.returnid(LoginTxt.Text, PswrdBox.Password);
                    if (tempid != 0)
                    {
                        IS.idAutSot = tempid;
                        IS.Access = BDWork.ReturnlvlAccess(tempid);
                        MainWindow mw = new MainWindow();
                        Hide();
                        mw.ShowDialog();
                    }
                    else
                    {
                        PpTxt.Visibility = Visibility.Visible;
                        popup1.IsOpen = true;
                        PpTxt.Text = "Введенный пароль неверен";
                    }
                }
                else
                {
                    PpTxt.Visibility = Visibility.Visible;
                    popup1.IsOpen = true;
                    PpTxt.Text = "Пользователя не существует";
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testforanyoshib();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
