using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeAccount.xaml
    /// </summary>
    public partial class ChangeAccount : Page
    {
        public MainWindow mainWindow;
        Classes.Login logins;
        Classes.Product products;
        Classes.Basket baskets;
        Elements.ProductItem item;
        Classes.Purchased purchased;

        public ChangeAccount(MainWindow mainWindow, Classes.Login logins)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.logins = logins;

            tb_fio.Text = logins.fio.ToString();
            tb_addres.Text = logins.addres.ToString();
            tb_mail.Text = logins.mail.ToString();
            tb_number.Text = logins.number.ToString();
            login_reg.Text = logins.name_user.ToString();
            passw_reg.Text = logins.password_user.ToString();
            tb_bith.Text = logins.bith.ToString();
            cb_role.Text = logins.role.ToString();
        }


        string login_t;
        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {

            var login = login_reg.Text;


            //Проверка на существовангие аккаунта
            Connection.Class1.Select($"UPDATE Login SET name_user='5' where id_user={logins.id_user}");
            mainWindow.LoadLoginInfo();
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
            if (!mainWindow.ItsNumber(tb_number.Text) || tb_number.Text == " " || tb_number.Text.Length!=11)
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




            Connection.Class1.Select($"UPDATE Login SET name_user='{login_reg.Text}',password_user='{passw_reg.Text}', fio='{tb_fio.Text}',number='{tb_number.Text}',addres='{tb_addres.Text}',mail='{tb_mail.Text}',bith='{tb_bith.Text}',role='{cb_role.Text}' where id_user='{logins.id_user}'");

            MessageBox.Show("Данные пользователя успешно изменены.Удачного пользования!");
            mainWindow.OpenPage(new Pages.UsersControl(mainWindow,logins,purchased));
            mainWindow.LoadLoginInfo();
            
        }

        private void Click_Log_Page(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.UsersControl(mainWindow,logins,purchased));
            mainWindow.LoadLoginInfo();
        }
    }
}
