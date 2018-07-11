using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugHandler dbHandler = new DebugHandler();
            if (!dbHandler.Validated)
            {
                Console.WriteLine(StringConstants.CONSOLE_MSG_DBG_NOTVALIDATED);
            }

            // Get default Application Starter .json file
            string appStarterFile = ApplicationStarter.getDefaultAppStarterPath();
            if (String.IsNullOrEmpty(appStarterFile))
            {
                //TODO: Will retrieve Google Docs .json file. To implement later.
            }
            else
            {
                ApplicationStarter _appStarter = new ApplicationStarter(appStarterFile, dbHandler);
                _appStarter.Start();
            }
            Console.ReadKey();
        }
    }
}
