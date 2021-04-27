using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaApi.Models
{
    public class Tv_Show
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public bool IsActive { get; set; }
        public string Categoryname { get; set; }
        public DataType Year { get; set; }
        public Category category { get; set; }
        public ICollection<Shows>  shows { get; set; }
    }
}
