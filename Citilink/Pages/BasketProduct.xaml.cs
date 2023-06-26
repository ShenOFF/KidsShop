using HappyKids.Classes;
using HappyKids.Elements;
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

namespace HappyKids.Pages
{
	/// <summary>
	/// Логика взаимодействия для BasketProduct.xaml
	/// </summary>
	public partial class BasketProduct : Page
	{
		MainWindow mainWindow;
		Classes.Product products;
		Classes.Basket baskets;
        Elements.ProductItem item;
        Classes.Login logins;


        public BasketProduct(MainWindow mainWindow,Classes.Product products,Classes.Basket baskets)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			this.products = products;
			this.baskets = baskets;

			mainWindow.LoadBasket();
			AllBasketLook(mainWindow.baskets);
			

			//TestImage.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])products.src));
			//TestImage.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])products.src));
		}

		public void AllBasketLook(List<Classes.Basket> baskets)
		{

			baskets = mainWindow.baskets.FindAll(x => x.id_seller.ToString().Contains(Classes.data.id.ToString()));
			parrent.Children.Clear();
			foreach (var item in baskets)
			{
				parrent.Children.Add(new BasketItem(mainWindow,item,products,logins));
			}
		}

		private void Click_Main_Page(object sender, RoutedEventArgs e)
		{
            mainWindow.OpenPage(new Pages.main(mainWindow,products,baskets,item));
        }
		int total;

		private void Back(object sender, RoutedEventArgs e)
		{
            mainWindow.OpenPage(new Pages.main(mainWindow, products, baskets, item));
        }
	}
}
