using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaApi.ViewModels
{
    public class Moviemodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string Categoryname { get; set; }

        public IFormFile  file { get; set; }
    }
}
