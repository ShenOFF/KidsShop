using HappyKids.Classes;
using HappyKids.Elements;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Page
    {
        public static main mainItem;
        public MainWindow mainWindow;
        Classes.Product products;
        Classes.Login login;
        public static Elements.ProductItem item;
        Classes.Basket baskets;
        Classes.Purchased purchased;
        public main(MainWindow mainWindow,Classes.Product products,Classes.Basket baskets, Elements.ProductItem item)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.products = products;
            this.baskets = baskets;
            mainItem = this;
            //this.item = item;
            AllProductLook(mainWindow.products);
            //CreatedItem();
            //LoadP();

            if (data.role != "Admin") admin_role.Visibility = Visibility.Hidden;
            if(data.role=="Guest") btn_exitGuest.Visibility = Visibility.Visible;

        }

        public void LoadP()
        {
            mainWindow.products.Clear();
            mainWindow.LoadProduct();
        }

        public void AllProductLook(List<Classes.Product> products)
        {
            parrent.Children.Clear();
            foreach (var item in products)
                parrent.Children.Add(new ProductItem(mainWindow, item,baskets));
        }
        

        private void Click_Search(object sender, RoutedEventArgs e)
        {
            List<Classes.Product> result = null;

            if (result != null)
            {
                result = result.FindAll(x => x.name.Contains(tb_search.Text));
            }
            else
            {
                result = mainWindow.products.FindAll(x => x.name.Contains(tb_search.Text));
            }

            AllProductLook(result);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.ClientInfo(mainWindow,login));
        }
        
        private void Click_AddItems(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.AddItems(mainWindow,products));
        }
        

		private void basket_click(object sender, MouseButtonEventArgs e)
		{
            mainWindow.OpenPage(new Pages.BasketProduct(mainWindow,products,baskets));
            //ProductItem.productItem.main_label.Visibility = Visibility.Hidden;
            //ProductItem.productItem.main_button.Visibility = Visibility.Hidden;

            //ProductItem.productItem.basket_button.Visibility = Visibility.Visible;
            //ProductItem.productItem.basket_label.Visibility = Visibility.Visible;
        }

        private void BuyList_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.BuyList(mainWindow));
            
        }

        private void btn_controlU(object sender, MouseButtonEventArgs e)
        {
            mainWindow.OpenPage(new Pages.UsersControl(mainWindow,login,purchased));
        }

        private void Click_clear_filter(object sender, RoutedEventArgs e)
        {
            tb_search.Text = "";
            AllProductLook(mainWindow.products);
        }

        private void exitGuest(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(new Pages.login(mainWindow));
        }
    }
}
