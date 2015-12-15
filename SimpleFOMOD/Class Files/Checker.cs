using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleFOMOD.Class_Files
{
    class MainWindowChecker
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
                Regex regex = new Regex("^[a-z0-9._-]+$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(authName))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool VerNumberCheck(string verNumber)
        {
            if (verNumber.Length > 0 && verNumber.Length < 10)
            {
                Regex regex = new Regex("^[abv0-9.]+$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(verNumber))
                {
                    return true;
                }
                return false;
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

    public class ModuleConfigWindowChecker
    {

    }

}
