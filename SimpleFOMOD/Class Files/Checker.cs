using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFOMOD.Class_Files
{
    class Checker
    {
        public static bool ModNameCheck (string modName)
        {
            if(modName.Length > 0 && modName.Length < 30)
            {
                return true;
            }
            return false;
        }

        public static bool AuthorNameCheck (string authName)
        {
            if (authName.Length > 0 && authName.Length < 30)
            {
                return true;
            }
            return false;
        }

        public static bool VerNumberCheck(string verNumber)
        {
            if (verNumber.Length > 0 && verNumber.Length < 10)
            {
                return true;
            }
            return false;
        }

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
    }
}
