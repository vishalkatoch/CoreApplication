using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Utilities
{
    public class ValidEmailDomainAttribute:ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            string strings = value.ToString().Substring(value.ToString().IndexOf('@')+1);
            return strings.ToUpper() == allowedDomain.ToUpper();
        }
    }
}
