using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using MessageBox = System.Windows.MessageBox;
using HappyKids.Classes;
using System.Security.Cryptography.X509Certificates;
using Connection;

namespace HappyKids.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddItems.xaml
    /// </summary>
    public partial class AddItems : Page
    {
        MainWindow mainWindow;
        Classes.Product products;
        public byte[] image;
        public byte[] image1;
        Classes.Basket baskets;
        Elements.ProductItem item;
        public AddItems(MainWindow mainWindow, Classes.Product products)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.products = products;

        }

        private void Click_Reg_Accept(object sender, RoutedEventArgs e)
        {
            //Проверка на введеное название
            if (!mainWindow.ItsOnlyFIO(tb_name.Text) || tb_name.Text == " ")
            {
                MessageBox.Show("Вы не правильно написали название.Перепроверьте!");
                return;
            }

            //Проверка на введеное цены на товар
            if (!mainWindow.ItsNumber(tb_price.Text) || tb_price.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали цену.Перепроверьте!");
                return;
            }

            //Проверка на введеное кол-во на складе
            if (!mainWindow.ItsNumber(tb_count_storage.Text) || tb_count_storage.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали вол-во товара.Перепроверьте!");
                return;
            }

            //Проверка на введеное возростного ограничения
            if (!mainWindow.ItsNumber(tb_old.Text) || tb_old.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали цену.Перепроверьте!");
                return;
            }

            //Проверка на введеное веса
            if (!mainWindow.ItsNumber(tb_weight.Text) || tb_weight.Text == " ")
            {
                MessageBox.Show("Вы не правильно указали вес.Перепроверьте!");
                return;
            }

            //Проверка на введеный адрес
            if (!mainWindow.ItsOnlyFIO(tb_description.Text) || tb_description.Text == " ")
            {
                MessageBox.Show("Вы не правильно написали описание.Перепроверьте!");
                return;
            }

            if (image == null)
            {
                MessageBox.Show("Вы не указали картинку!");
                return ;
            }


            string connectionString = Class1.pathDB;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                
                command.CommandText = $"INSERT INTO Product(src,name,weight,count_storage,old,price,description) " +
                    $"VALUES ((@ImageData),'{tb_name.Text}','{tb_weight.Text}','{tb_count_storage.Text}','{tb_old.Text}','{tb_price.Text}','{tb_description.Text}')";

                SqlParameter sqlParam = command.Parameters.AddWithValue("@ImageData", image);
                sqlParam.DbType = DbType.Binary;
                command.ExecuteNonQuery();

                MessageBox.Show("Товар успешно добавлен!");
            }

            mainWindow.LoadProduct();
            
        }

        private void Click_Main_Page(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
        }
        
        private void Click_Image_Items(object sender, RoutedEventArgs e)
        {
            
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = File.ReadAllBytes(openFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Картинка не выбрана!");
                return;
            }

            src.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])image));
            
        }
    }
}
