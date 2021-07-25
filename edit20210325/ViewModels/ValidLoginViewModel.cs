using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edit20210325.ViewModels
{
    public class ValidLoginViewModel
    {
        public bool Succeeded { get; set; }

        public string Describe { get; set; }
        public List<Tuple<string, bool>> DisplayAuthentications { get;set;}
    }
}
