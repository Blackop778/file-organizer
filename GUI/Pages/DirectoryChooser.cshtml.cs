using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ElectronNET.API;
using ElectronNET.API.Entities;

using file_organizer.Core;
using Controller = file_organizer.Core.Controller;

namespace file_organizer.GUI.Pages
{
    public class DirectoryChooserModel : PageModel
    {
        private readonly ILogger<EditorModel> _logger;

        public DirectoryChooserModel(ILogger<EditorModel> logger)
        {
            _logger = logger;
        }

        public async Task OnPostOpenDialog()
        {
            BrowserWindow window = Electron.WindowManager.BrowserWindows.First();
            OpenDialogProperty[] properties = new OpenDialogProperty[] { OpenDialogProperty.openDirectory };
            string[] directory = await Electron.Dialog.ShowOpenDialogAsync(window, new OpenDialogOptions() { Properties = properties });

            if (directory.Length <= 0)
            {
                MessageBoxOptions options = new MessageBoxOptions("Error: Please choose a folder to open")
                {
                    Buttons = new string[] { "OK" },
                    Type = MessageBoxType.error,
                    Title = "No folder chosen"
                };
                await Electron.Dialog.ShowMessageBoxAsync(options);
                window.Close();
            } else {
                EditorModel.Controller = new Controller(directory[0]);
                window.LoadURL("http://localhost:8001/editor");
            }
        }
    }
}
