using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParserApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name}[{Id}]";
        }
    }
}