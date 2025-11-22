using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachHussien.Domain.Entities
{
    public class FoodCategory
    {
        public FoodCategory()
        {
            Foods = new HashSet<Food>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
