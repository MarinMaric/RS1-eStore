using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.ViewModel
{
    public class RegisterVM
    {
        public int DrzavaValue { get; set; }
        public List<SelectListItem> Drzava { get; set; }
    }
}
