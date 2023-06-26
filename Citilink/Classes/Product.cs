using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyKids.Classes
{
    public class Product
    {
        public Product()
        {

        }
        public Product(int id, byte[] src, string name, string weight, string count_storage, string old, string price, string description)
        {
            this.id = id;
            this.src = src;
            this.name = name;
            this.weight = weight;
            this.count_storage = count_storage;
            this.old = old;
            this.price = price;
            this.description = description;
        }

        public int id { get; set; }
        public byte[] src { get; set; }
        public string name { get; set; }
        public string weight { get; set; }
        public string count_storage{ get; set; }
        public string old { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        
    }
}
