using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class IpValidator : IIpValidator
    {
        public bool ValidateIPv4(string ipString)
        {
            if (string.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            var splitValues = ipString.Split('.');

            return splitValues.Length == 4 
                   && splitValues.All(r => byte.TryParse(r, out var tempForParsing));
        }
    }
}
