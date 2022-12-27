using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nextwo.Models.SharedProp;

namespace Nextwo.Models.ViewModel
{
    public class SliderViewModel : CommonProp
    {
        public IFormFile SliderImg { get; set; }
    }
}
