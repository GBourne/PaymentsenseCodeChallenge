using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class Country
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Alpha3Code { get; set; }
        public string Flag { get; set; }
        public Currency[] Currencies { get; set; }



    }
}
