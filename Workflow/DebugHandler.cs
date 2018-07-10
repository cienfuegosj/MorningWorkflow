using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    class DebugHandler
    {
        DebugHandler(String DebugFileName = null)
        {
            // Check if the Workflow Metadata Debug Folder and Path is valid
        }
        private void ValidateMetaDebugFolderandPath()
        {
            /*
             * Check if environment variable is set and if not, set the environment variable
             * WORKFLOW_META to user's Program Files/MorningWorkflow/Meta folder.
            */
            if(Environment.GetEnvironmentVariable("WORKFLOW_META") != null)
            {
                // Get file and log Debug Handler connection
                string s_WORKFLOW_META_path = Environment.GetEnvironmentVariable("WORKFLOW_META");
                s_WORKFLOW_META_path.Trim();
                try
                {
                    FileStream f_WORKFLOW_META = File.OpenWrite(s_WORKFLOW_META_path);
                    f_WORKFLOW_META.Write();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        

    }
}
