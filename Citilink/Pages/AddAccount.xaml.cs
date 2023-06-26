using HappyKids.Classes;
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
using static HappyKids.Pages.login;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Page
    {
        public MainWindow mainWindow;
        Classes.Login logins;
        Classes.Purchased purchased;
        public AddAccount(MainWindow mainWindow, Classes.Login logins)
        {
            InitializeComponent();
            page_select = pagess.none;
            this.mainWindow = mainWindow;
            this.logins = logins;
        }
        private void Click_Log_Page(object sender, RoutedEventArgs e)
        {

            mainWindow.OpenPage(new Pages.UsersControl(mainWindow,logins,purchased));
        }
        string login_t;
        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {
            var login = login_reg.Text;
            var password = passw_reg.Text;

            

            for (int i = 0; i < mainWindow.logins.Count; i++)
            {
                login_t = mainWindow.logins[i].name_user;
                if (login_t == login)
                {
                    MessageBox.Show("Аккаунт с таким логином уже существует");
                    return;
                }

            }


            if (!mainWindow.ItsOnlyFIO(tb_fio.Text) || tb_fio.Text == " ")
            {
                MessageBox.Show("Вы не правильно написали ФИО.Перепроверьте!");
                return;
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

            string query = $"INSERT INTO Login(name_user,password_user,fio,number,addres,mail,bith,role) values('{login}','{password}','{tb_fio.Text}','{tb_number.Text}','{tb_addres.Text}','{tb_mail.Text}','{tb_bith.Text}','{cb_role.Text}')";
            
            Connection.Class1.Select(query);

            
            MessageBox.Show("Аккаунт зарегестирован!", "Успешно!");
            mainWindow.OpenPage(new Pages.UsersControl(mainWindow,logins,purchased));

            
        }
    }
}
