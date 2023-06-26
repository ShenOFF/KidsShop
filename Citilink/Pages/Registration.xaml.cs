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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using static HappyKids.Pages.login;
using System.Data;
using HappyKids.Classes;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public MainWindow mainWindow;
        Classes.Login logins;
        public Registration(MainWindow mainWindow,Classes.Login logins)
        {
            InitializeComponent();
            page_select = pagess.none;
            this.mainWindow = mainWindow;
            this.logins= logins;
        }

        
        private void Click_Log_Page(object sender, RoutedEventArgs e)
        {

            mainWindow.OpenPage(new Pages.login(mainWindow));
        }
        string login_t;
        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {
            var login = login_reg.Text;
            var password = passw_reg.Text;

            

            for(int i = 0; i < mainWindow.logins.Count; i++)
            {
                login_t = mainWindow.logins[i].name_user;
                if(login_t == login)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует");
                    return;
                }
                
            }

            //Проверка на ФИО
            string[] check = tb_fio.Text.Split(' ');
            if (check[0]!=null && check[0].Length < 2)
            {
                MessageBox.Show("Введенное ваши ФИО слишком короткое.  Перепроверьте!");
                return;
            }
            try
            {
                if (check[1]!=null && check[1].Length < 2)
                {
                    MessageBox.Show("Введенное ваши Имя слишком короткое.  Перепроверьте!");
                    return;
                }
                if (check[2] != null && check[2].Length < 2)
                {
                    MessageBox.Show("Введенное ваши Отчество слишком короткое.  Перепроверьте!");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Установите пробел!");
                return;
            }
            for(int i = 0; i < tb_fio.Text.Length; i++)
            {
                if (!mainWindow.ItsOnlyFIO(tb_fio.Text) || tb_fio.Text == " ")
                {
                    MessageBox.Show("Указанно вами ФИО содержит цифры.Перепроверьте!");
                    return;
                }
            }
           

            //Проверка на введеное цены на номер
            if (!mainWindow.ItsNumber(tb_number.Text) || tb_number.Text == " " || tb_number.Text.Length != 11)
            {
                MessageBox.Show("Вы не правильно указали номер.Перепроверьте!");
                return;
            }

            //Проверка на введеное адреса
            if (tb_addres.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали адрес.Перепроверьте!");
                return;
            }

            //Проверка на почту
            if (!mainWindow.IsValidEmailAddress(tb_mail.Text))
            {
                MessageBox.Show("Почта введина не верно!");
                return;

            }
            

          
            //Проверка даты
            if (!DateTime.TryParse(tb_bith.Text, out _) || DateTime.Parse(tb_bith.Text) > DateTime.Now)
            {
                MessageBox.Show("Дата введина не верно!");
                return;
            }

            
            

            
            string query = $"INSERT INTO Login(name_user,password_user,fio,number,addres,mail,bith,role) values('{login}','{password}','{tb_fio.Text}','{tb_number.Text}','{tb_addres.Text}','{tb_mail.Text}','{tb_bith.Text}','User')";
            Connection.Class1.Select(query);

            
                MessageBox.Show("Аккаунт зарегестирован!","Успешно!");
                mainWindow.OpenPage(new Pages.login(mainWindow));
           
        }
    }
}
