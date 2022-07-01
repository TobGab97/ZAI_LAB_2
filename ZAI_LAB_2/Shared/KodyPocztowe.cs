using System;
using System.Collections.Generic;


#nullable disable

namespace ZAI_LAB_2.Shared
{
    public partial class KodyPocztowe
    {


        public long Id { get; set; }
        public string KodPocztowy { get; set; }
        public string Adres { get; set; }
        public string Miejscowosc { get; set; }
        public string Wojewodztwo { get; set; }
        public string Powiat { get; set; }
    }
}
