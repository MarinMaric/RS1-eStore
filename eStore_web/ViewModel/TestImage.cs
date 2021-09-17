using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class TestImage
    {
        public string text { get; set; }
        public IFormFile file { get; set; }
    }
}
