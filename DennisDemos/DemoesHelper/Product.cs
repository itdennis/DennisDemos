using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DennisDemos.DemosHelper
{
    public class Product
    {
        private string name;
        public string Name { get { return name; } }
        private decimal? price;
        public decimal? Price
        {
            get { return price; }
            set { this.price = value; }
        }
        public Product(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>()
            {
                new Product(name: "IphoneX", price:8888),
                new Product(name: "Iphone8", price:7888),
                new Product(name: "Iphone7", price:6888),
                new Product(name: "Iphone6", price:4888),
                new Product(name: "Iphone6s", price:5888),
            };
        }
    }
}
