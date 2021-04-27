using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaApi.Models
{
    public class Shows
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Episode { get; set; }
        public bool IsActive { get; set; }
        public string Tvshowname { get; set; }
        public string Img { get; set; }
        public string Url { get; set; }
        public Tv_Show tv_Shows { get; set; }
    }
}
