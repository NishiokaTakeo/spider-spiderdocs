using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderDocsModule
{
    public class Factory
    {
        static UserSettings _userSettings;

        static public UserSettings Instance4UserSettins()
        {
            if (_userSettings == null)
                _userSettings = new UserSettings();

            return _userSettings;
        }
    }
}
