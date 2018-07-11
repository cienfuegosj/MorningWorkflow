using System;
using System.IO;
using System.Diagnostics;
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

        private Dictionary<string, string[]> StartupApps;
        DebugHandler dbH;
        public bool bDeserialized;


        //Public Methods
        public ApplicationStarter(String StartupAppsJSONFilename, DebugHandler dbH)
        {
            this.dbH = dbH;
            if (File.Exists(StartupAppsJSONFilename))
            {
                String s_Content = File.ReadAllText(StartupAppsJSONFilename);
                var JSON_Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                try
                {
                    StartupApps = JSON_Serializer.Deserialize<Dictionary<string, string[]>>(s_Content);
                    bDeserialized = true;
                }
                catch (Exception ex)
                {
                    dbH.write(String.Format(StringConstants.CONSOLE_ERROR_ON_DESERIALIZE, ex.Message));
                }            
            }
            else
            {
                dbH.write(StringConstants.STRING_STARTUP_JSON_FILE_NOTEXIST);
            }        
        }

        public static string getDefaultAppStarterPath()
        {
            /// <param></param>
            /// <summary>Looks for the Morning Workflow folder and looks for a startup.json file.</summary>
            /// <return>Null string if the default file is not found. File path of the startupapp.json file if found</return>

            string UserProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string MorningWorkflowPath = Path.Combine(UserProfilePath, StringConstants.STRING_MORNING_WKFLOW_FOLDER_NAME);
            string StartupJSONPath = Path.Combine(MorningWorkflowPath, StringConstants.STRING_DEFAULT_STARTUP_APP_JSON_FILE_NAME);
            if (File.Exists(StartupJSONPath))
            {
                return StartupJSONPath;
            }
            else
            {
                return "";
            }
        }

        public void Start()
        {
            DelegateAllStartupApps();
        }

        //Private Methods
        private void DelegateAllStartupApps()
        {
            foreach(KeyValuePair<string, string[]> kvp in StartupApps)
            {
                foreach (string str_app in kvp.Value) 
                {
                    string PathToExecutable = findPathtoExecutable(str_app);
                    if (!String.IsNullOrEmpty(PathToExecutable) && Process.GetProcessesByName(str_app).Length.Equals(0))
                    {
                        try
                        {
                            Process.Start(PathToExecutable);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(StringConstants.CONSOLE_ERROR_ON_PROCESS_START);
                            dbH.write(StringConstants.STRING_ERROR_ON_PROCESS_START);
                        }
                    }
                }
            }
        }

        private string findPathtoExecutable(string strApp)
        {
            switch (strApp)
            {
                case StringConstants.STRING_APP_NAME_JABBER:
                    return @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Cisco Jabber\Cisco Jabber";
                case StringConstants.STRING_APP_NAME_OUTLOOK:
                    return @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Office 2013\Outlook 2013";
                default:
                    return "";
            }
        }
    }
}
