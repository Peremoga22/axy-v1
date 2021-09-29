using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Description { get; set; }
        public string CurentData { get; set; }
     
        public bool IsIncome { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<Expenditure> Expenditures { get; set; }
    }
}
