using System;
using System.Collections.Generic;
using System.Text;

namespace Producks.Repo.ViewModels
{
    public class ProductRepoVM
    {
        public virtual int Id { get; set; }
        public virtual string Category { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
        public int StockLevel { get; set; }
        public bool Undercutters { get; set; }
        public bool inStock { get; set; }
        public string StockStatus
        {
            get
            {
                return (StockLevel > 0) ? "in stock" : "out of stock";
            }
            set
            {
                return;
            }
        }
    }
}
