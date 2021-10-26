using System;
using System.Collections.Generic;
using System.Text;

namespace Producks.Repo.ViewModels
{
    public class CategoryRepoVM
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
