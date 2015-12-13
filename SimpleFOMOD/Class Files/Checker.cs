using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFOMOD.Class_Files
{
    class Checker
    {
        public static bool URLCheck(string url)
        {
            if (url != "")
            {
                if (url.Contains("nexusmods.com") && url.Contains("/mods/"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool VerNumberCheck(string verNumber)
        {
            if (verNumber != "")
            {
                double Num;
                bool isNum = double.TryParse(verNumber, out Num);
                if (isNum)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
