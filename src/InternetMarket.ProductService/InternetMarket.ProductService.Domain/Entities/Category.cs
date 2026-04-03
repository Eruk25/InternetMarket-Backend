using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMarket.ProductService.Domain.ValueObjects.Category;

namespace InternetMarket.ProductService.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; private set; }
        public CategoryName CategoryName { get; private set; }

        public Category(CategoryName categoryName)
        {
            CategoryName = categoryName;
        }
    }
}