using HappyKids.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersControl.xaml
    /// </summary>
    public partial class UsersControl : Page
    {
        MainWindow mainWindow;
        Classes.Login logins;
        Classes.Product products;
        Classes.Basket baskets;
        Elements.ProductItem item;
        Classes.Purchased purchased;
        public UsersControl(MainWindow mainWindow, Classes.Login logins,Classes.Purchased purchased)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.logins = logins;
            this.purchased=purchased;

            mainWindow.LoadLoginInfo();
            LoadDataUsers();
            LoadDataBuy();
        }
        public void LoadDataUsers()
        {
            //Вывод данных о пользователях
            listView_users.Items.Clear();
            var a = mainWindow.logins/*.FindAll(x=>x.role=="User")*/;
            for (int i = 0; i < a.Count; i++)
            {
                listView_users.Items.Add(a[i]);
            }

            
        }

        public void LoadDataBuy()
        {
            //Вывод все покупок которые сделали пользователи
            listView_Buy.Items.Clear();
            var buy = mainWindow.purchaseds;
            for (int i = 0; i < buy.Count; i++)
            {
                listView_Buy.Items.Add(buy[i]);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.main(mainWindow, products, baskets, item));
        }

        private void add_user(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(new Pages.AddAccount(mainWindow,logins));
        }
        
        private void delete_user(object sender, MouseButtonEventArgs e)
        {
            if(listView_users.SelectedIndex != -1)
            {
                int select = mainWindow.logins[listView_users.SelectedIndex].id_user;
                if (select == data.id)
                {
                    MessageBox.Show("Вы не можете удалить самого себя,вы же администратор!");
                    return;
                }

                Connection.Class1.Select("DELETE FROM Login WHERE id_user=" + mainWindow.logins[listView_users.SelectedIndex].id_user);
                mainWindow.LoadLoginInfo();
                LoadDataUsers();
                MessageBox.Show("Вы успешно удалили пользователя!");
            }
            else
            {
                MessageBox.Show("Выберите пользователя!");
            }
        }

        
        private void change_user(object sender, MouseButtonEventArgs e)
        {
            if (listView_users.SelectedIndex != -1)
            {
                mainWindow.OpenPage(new Pages.ChangeAccount(mainWindow, mainWindow.logins[listView_users.SelectedIndex]));
            }
            else
            {
                MessageBox.Show("Выберите пользователя!");
            }
        }

        public static int u_Buy=-1;
        private void save_buy(object sender, MouseButtonEventArgs e)
        {
            if (u_buy.Text != "")
            {
                u_Buy = Convert.ToInt32(u_buy.Text);
                mainWindow.SaveProducts();
            }

            mainWindow.SaveProducts();
            
        }

        
    }
}
