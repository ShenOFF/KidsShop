//using HappyKids.Classes;
using Connection;
using HappyKids.Classes;
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
    /// Логика взаимодействия для ClientInfo.xaml
    /// </summary>
    public partial class ClientInfo : Page
    {
        public MainWindow mainWindow;
        Classes.Login logins;
        Classes.Product products;
        Classes.Basket baskets;
        Elements.ProductItem item;

        public ClientInfo(MainWindow mainWindow, Classes.Login logins)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.logins = logins;

            
            LoadlInfo();

            //tb_fio.Text = logins.fio.ToString();
            //tb_addres.Text = logins.addres.ToString();
            //tb_mail.Text = logins.mail.ToString();
            //tb_number.Text = logins.number.ToString();

            //mainWindow.LoadLoginInfo();
        }

        private void Click_Main_Page(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
        }

        public void LoadlInfo()
        {
            //SqlConnection sqlConnection = new SqlConnection("server=10.0.146.3;Trusted_Connection=NO;database=KidsShop;user=c;PWD=1");
            SqlConnection sqlConnection = new SqlConnection(Class1.pathDB);
            sqlConnection.Open();
            string sql = $"SELECT fio,number,addres,mail,bith FROM Login where Login.id_user={Classes.data.id}";
            SqlCommand command=new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                tb_fio.Text=(reader[0].ToString());
                tb_number.Text=(reader[1].ToString());
                tb_addres.Text=(reader[2].ToString());
                tb_mail.Text=(reader[3].ToString());
                tb_bith.Text=(reader[4].ToString());
            }
            reader.Close();

            sqlConnection.Close();
        }

        private void Click_Info_Accept(object sender, RoutedEventArgs e)
        {
            
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

            Connection.Class1.Select($"UPDATE Login SET fio='{tb_fio.Text}',number='{tb_number.Text}',addres='{tb_addres.Text}',mail='{tb_mail.Text}',bith='{tb_bith.Text}' where Login.id_user={data.id}");
            MessageBox.Show("Данные пользователя успешно изменены.Удачного пользования!");
            mainWindow.LoadLoginInfo();
            //Connection.Class1.Select($"INSERT INTO Client(fio,number,addres,mail) VALUES ('{tb_fio.Text}','{tb_number.Text}','{tb_addres.Text}','{tb_mail.Text}') ");
            //string query = $"INSERT INTO Client (fio,number,addres,mail) VALUES ('{tb_fio.Text}','{tb_number.Text}','{tb_addres.Text}','{tb_mail.Text}') WHERE Client.id={data.id}";
            //Connection.Class1.Select(query);
            //}
        }

        private void Click_remove(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.login(mainWindow));
        }
    }
}
