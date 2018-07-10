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
        private FileStream f_WORKFLOW_META;
        private string s_WORKFLOW_META;
        public bool Validated = false;
        public DebugHandler()
        {
            // Check if the Workflow Metadata Debug Folder and Path is valid
            ValidateMetaDebugFolderandPath();
        }
        ~DebugHandler()
        {
            
        }

        // Public Methods
        public void write(string error)
        {
            error.Trim(); // Make sure to trim all end spaces to append only one newline character.
            File.WriteAllText(s_WORKFLOW_META, error + "\n");
        }


        // Private Methods
        private void ValidateMetaDebugFolderandPath()
        {
            /*
             * Check if environment variable is set and if not, set the environment variable
             * WORKFLOW_META to user's Program Files/MorningWorkflow/Meta folder.
            */
            if(Environment.GetEnvironmentVariable(StringConstants.STRING_META_DEBUG_FOLDER_EV) != null)
            {
                // Check if <User Profile>/<STRING_MORNING_WKFLOW_FOLDER_NAME>/<STRING_META_DEBUG_FOLDER_NAME> directory exists.
                // If it exists and environment variable value is not equal to this path, wipe above directory and proceed.
                string UserFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string metaFolder = Path.Combine(UserFolderPath, StringConstants.STRING_MORNING_WKFLOW_FOLDER_NAME, StringConstants.STRING_DEFAULT_META_DEBUG_FOLDER_NAME);
                if (Directory.Exists(metaFolder) && !Path.Equals(metaFolder, Environment.GetEnvironmentVariable(StringConstants.STRING_META_DEBUG_FOLDER_EV)))
                {
                    Directory.Delete(metaFolder, recursive: true);
                }

                // Get folder and log Debug Handler connection
                metaFolder = Environment.GetEnvironmentVariable(StringConstants.STRING_META_DEBUG_FOLDER_EV);
                metaFolder.Trim();
                string metaFile;

                try
                {
                    f_WORKFLOW_META = File.OpenWrite(s_WORKFLOW_META);
                    byte[] MSG = Encoding.ASCII.GetBytes(StringConstants.STRING_DEBUG_HANDLER_CONNECTED);
                    f_WORKFLOW_META.Write(MSG, 0, MSG.Length);
                    Validated = true;
                }
                catch (Exception ex)
                {
                    // Invalid WORKFLOW_META Path, so create temporary Meta Debug folder
                    UserFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string morningWorkflowFolderPath = Path.Combine(UserFolderPath, StringConstants.STRING_MORNING_WKFLOW_FOLDER_NAME);
                    if (!Directory.Exists(morningWorkflowFolderPath))
                        Directory.CreateDirectory(morningWorkflowFolderPath);
                    string metaFolderPath = Path.Combine(UserFolderPath, StringConstants.STRING_DEFAULT_META_DEBUG_FOLDER_NAME);
                    Directory.CreateDirectory(metaFolderPath);
                    string msg = String.Format(StringConstants.CONSOLE_MSG_TEMP_META_DEBUG, ex.Message, metaFolderPath);
                    Environment.SetEnvironmentVariable(StringConstants.STRING_META_DEBUG_FOLDER_EV, metaFolderPath);
                    Console.WriteLine(msg);
                    ValidateMetaDebugFolderandPath();
                }
            } else
            {
                // Folder set-up
                string UserFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string morningWorkflowFolderPath = Path.Combine(UserFolderPath, StringConstants.STRING_MORNING_WKFLOW_FOLDER_NAME);
                if (!Directory.Exists(morningWorkflowFolderPath))
                    Directory.CreateDirectory(morningWorkflowFolderPath);
                string metaFolderPath = Path.Combine(morningWorkflowFolderPath, StringConstants.STRING_DEFAULT_META_DEBUG_FOLDER_NAME);
                if (!Directory.Exists(metaFolderPath))
                    Directory.CreateDirectory(metaFolderPath);
                string msg = String.Format(StringConstants.CONSOLE_NO_META_DEBUG_FOLDER_EV, metaFolderPath);
                Console.WriteLine(msg);

                // File set-up
                string metaFilePath = Path.Combine(metaFolderPath, StringConstants.STRING_DEFAULT_META_DEBUG_FILE_NAME);
                if (!File.Exists(metaFilePath))
                {
                    File.Create(metaFilePath);
                    s_WORKFLOW_META = metaFilePath;
                } else
                {
                    s_WORKFLOW_META = metaFilePath;
                }

                bool bSuccess = false;
                try
                {
                    File.WriteAllText(s_WORKFLOW_META, StringConstants.STRING_DEBUG_HANDLER_CONNECTED);
                    bSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format(StringConstants.CONSOLE_ERROR_ON_WRITE, ex.Message));
                }
                finally
                {
                    Validated = bSuccess;
                }
            }
        }
    }
}
