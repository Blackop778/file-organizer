using System;
using System.Web;
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
    public class EditorModel : PageModel
    {
        private readonly ILogger<EditorModel> _logger;
        public static Controller Controller { get; set; }

        public EditorModel(ILogger<EditorModel> logger)
        {
            _logger = logger;
        }

        public OkResult OnPostRemove(string fileName) {
            Controller.DisableEntry(HttpUtility.UrlDecode(fileName));
            return new OkResult();
        }

        public OkResult OnPostMove(int toIndex, string fileName) {
            Controller.MoveEntry(toIndex, HttpUtility.UrlDecode(fileName));
            return new OkResult();
        }

        public static int GetNumberInputWidth() {
            int width = 3;

            if (Controller != null && Controller.Entries.Any()) {
                OrganizerEntry lastEntry = Controller.Entries.Last();
                width = (int)Math.Floor(Math.Log10(lastEntry.Number)) + 1;

                if (width < 3)
                    width = 3;
            }

            return width;
        }
    }
}
