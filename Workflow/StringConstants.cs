using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    static class StringConstants
    {
        public const string STRING_DEBUG_HANDLER_CONNECTED = "DEBUG HANDLER CONNECTED\n";
        public const string STRING_DEBUG_HANDLER_DISCONNECTED = "DEBUG HANDLER DISCONNECTED\n";
        public const string STRING_DEFAULT_META_DEBUG_FOLDER_NAME = "META_DEBUG";
        public const string STRING_DEFAULT_META_DEBUG_FILE_NAME = "meta_debug.txt";
        public const string STRING_DEFAULT_STARTUP_APP_JSON_FILE_NAME = "startupapps.json";
        public const string STRING_MORNING_WKFLOW_FOLDER_NAME = "Morning Workflow";
        public const string STRING_META_DEBUG_FOLDER_EV = "WORKFLOW_META";
        public const string STRING_ERROR_ON_PROCESS_START = "An error occurred while trying to start the application.\n";
        public const string STRING_STARTUP_JSON_FILE_NOTEXIST = "The start-up application .json given does not exist.\n";
        public const string STRING_SUCCESS_ON_PROCESS_START = "The start up application, {0}, was successfully launched.\n";

        // Any CONSOLE prepended constant string cannot end in a new line character.
        public const string CONSOLE_MSG_TEMP_META_DEBUG = "An error occurred while trying to create specified WORKFLOW_META folder: \n\tError:\t{0}\n\tCreating temp folder at and setting environment variable:\t{1}";
        public const string CONSOLE_MSG_DBG_NOTVALIDATED = "The debug handler has not been validated. Please try again.";
        public const string CONSOLE_NO_META_DEBUG_FOLDER_EV = "The WORKFLOW_META environment variable was not set up.\nCreating or validating folder at:\n\t{0}";
        public const string CONSOLE_ERROR_ON_WRITE = "An error occurred when trying to write to the debug file.\nError:\n\t{0}";
        public const string CONSOLE_ERROR_ON_DESERIALIZE = "An error occurred when trying to deserialize the .json file. Error:\n\t{0}";
        public const string CONSOLE_ERROR_ON_PROCESS_START = "An error occurred while trying to start the application.";

        // Application Names
        public const string STRING_APP_NAME_OUTLOOK = "Outlook";
        public const string STRING_APP_NAME_JABBER = "Jabber";
    }
}
