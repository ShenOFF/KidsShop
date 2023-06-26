using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyKids.Classes
{
	public class Basket
	{
		public int id { get; set; }
		public int id_product { get; set; }
		public int id_seller{get;set;}
		public string product_name { get; set; }
		//public byte[] product_src { get; set; }
		public int product_price { get; set; }
		public int product_old { get; set; }
		public int product_count { get; set; }
		public string product_description { get; set; }

    }
}
