using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script;
using System.Threading.Tasks;

namespace Workflow {
    class ApplicationStarter {
        /*
         * Application Starter reads the "StartupApps.json" file, parses the applications in
         * the program files available, starts the executables, and releases them at the user
         * level. */
        ApplicationStarter(String StartupAppsJSONFilename /* Debugger Object */)
        {
            if (File.Exists(StartupAppsJSONFilename))
            {
                String s_Content = File.ReadAllText(StartupAppsJSONFilename);
                var JSON_Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var StartupApps = JSON_Serializer.Deserialize<Dictionary<string, Dictionary<string, string>>[]>(s_Content);
            }
            else
            {
                // Debugging Info must be outputted
            }        
        }
    }
}
