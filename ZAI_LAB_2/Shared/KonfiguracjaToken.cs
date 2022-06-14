using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAI_LAB_2.Shared
{
    public class KonfiguracjaToken
    {
        public string TokenSecret { get; set; }
        public double TokenMinutes { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }

    }
}
