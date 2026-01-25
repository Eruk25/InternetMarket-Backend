using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        public Category(string title)
        {
            Title = title;
        }
    }
}