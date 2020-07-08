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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public static Controller Controller { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
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
                Controller = new Controller(directory[0]);
            }
        }

        public void OnPostRemove(string prettyName) {
            Controller.DisableEntry(prettyName);
        }
    }
}
