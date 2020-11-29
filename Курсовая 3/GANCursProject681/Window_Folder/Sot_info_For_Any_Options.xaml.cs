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
    /// Логика взаимодействия для Sot_info_For_Any_Options.xaml
    /// </summary>
    public partial class Sot_info_For_Any_Options : Window
    {
        ANDBEntities db = new ANDBEntities();
        Class_Folder.DB_Work OWBDC = new Class_Folder.DB_Work();
        int CurrentLvlOfDostup;
        String CurrentPositionOfSotr;
        public Sot_info_For_Any_Options()
        {
            InitializeComponent();

        }
        public int idSotr;
        public void ConstructForm(int a)
        {
            if (a == 1)
            {
                btn_back.Margin = new Thickness(93, 60, 0, 0);
                btn_Update.Margin = new Thickness(10, 61, 0, 0);
                btn_Update.Visibility = Visibility.Visible;
                btn_Update.IsEnabled = true;
            }
            else
            {
                btn_back.Margin = new Thickness(10, 61, 0, 0);
                btn_Update.Visibility = Visibility.Hidden;
                btn_Update.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        public void Fill()
        {
            var InfoOfSotrToFill = db.Workers.Include(i => i.Role).Where(w => w.id == idSotr).FirstOrDefault();
            FName.Text = InfoOfSotrToFill.FirstName;
            LName.Text = InfoOfSotrToFill.LastName;
            MName.Text = InfoOfSotrToFill.MiddleName;
            Phon.Text = InfoOfSotrToFill.ContactNumber;
            EmailTxt.Text = InfoOfSotrToFill.Email;
            Pos.Text = InfoOfSotrToFill.Role.Name;
            LelelOfDostup.Text = InfoOfSotrToFill.Role.LevelOfAccess.ToString();
            CurrentPositionOfSotr = InfoOfSotrToFill.Role.Name;
            CurrentLvlOfDostup = InfoOfSotrToFill.Role.LevelOfAccess;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int idps = OWBDC.ReturnidRole(Pos.Text, Convert.ToInt32(LelelOfDostup.Text));
            if (FName.Text == "" && LName.Text == "" && MName.Text == "" && Phon.Text == "" && EmailTxt.Text == "" && Pos.Text == "" && LelelOfDostup.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Все поля пусты";
            }
            else if (FName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле имя не заполнено";
            }
            else if (LName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле фамилия не заполнено";
            }
            else if (MName.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле отчество не заполнено";
            }
            else if (Phon.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле номер телефона не заполнено";
            }
            else if (EmailTxt.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле Email не заполнено";
            }
            else if (Pos.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле должность не заполнено";
            }
            else if (LelelOfDostup.Text == "")
            {
                PpTxt.Visibility = Visibility.Visible;
                popup1.IsOpen = true;
                PpTxt.Text = "Поле уровень доступа не заполнено";
            }
            else if (idps == 0)
            {
                Role PosAdd = new Role()
                {
                    Name = Pos.Text,
                    LevelOfAccess = Convert.ToInt32(LelelOfDostup.Text)
                };
                db.Role.Add(PosAdd);
                db.SaveChanges();
                int idps2 = OWBDC.ReturnidRole(Pos.Text, Convert.ToInt32(LelelOfDostup.Text));
                int inf = db.Workers.Where(w => w.id == idSotr).Select(s => s.id).FirstOrDefault();
                var myupdate = db.Workers.Where(w => w.id == inf).FirstOrDefault();
                myupdate.LastName = LName.Text;
                myupdate.FirstName = FName.Text;
                myupdate.MiddleName = MName.Text;
                myupdate.ContactNumber = Phon.Text;
                myupdate.Email = EmailTxt.Text;
                myupdate.FK_Role_id = idps2;
                db.SaveChanges();

            }
            else
            {
                int inf = db.Workers.Where(w => w.id == idSotr).Select(s => s.id).FirstOrDefault();
                var myupdate = db.Workers.Where(w => w.id == inf).FirstOrDefault();
                myupdate.LastName = LName.Text;
                myupdate.FirstName = FName.Text;
                myupdate.MiddleName = MName.Text;
                myupdate.ContactNumber = Phon.Text;
                myupdate.Email = EmailTxt.Text;
                db.SaveChanges();
            }
        }
    }
}
