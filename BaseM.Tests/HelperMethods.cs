using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseM.Tests
{
    public static class HelperMethods
    {
        public static NumericSystem StringToNumericSystem(string system)
        {
            switch(system.ToUpper())
            {
                case "BINARY":
                    return NumericSystem.Binary;
                case "OCTAL":
                    return NumericSystem.Octal;
                case "DECIMAL":
                    return NumericSystem.Decimal;
                case "HEXADECIMAL":
                    return NumericSystem.Hexadecimal;
            }

            throw new ArgumentException("Unknown system");
        }
    }
}
