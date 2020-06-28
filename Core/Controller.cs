using System;
using System.IO;
using System.Collections.Generic;

namespace file_organizer.Core {
    public class Controller {
        private List<OrganizerEntry> _entries;
        public IReadOnlyCollection<OrganizerEntry> Entries => _entries.AsReadOnly();

        public Controller(string directoryPath) {
            _entries = new List<OrganizerEntry>();

            List<string> files = new List<string>(Directory.EnumerateFiles(directoryPath));
            files.Sort();

            foreach (string file in files) {
                FileInfo fileInfo = new FileInfo(file);
                
                if (_entries.Count > 0 && _entries[_entries.Count - 1].FileName.Equals(fileInfo.Name)) {
                    _entries[_entries.Count - 1].AddExtension(fileInfo.Extension);
                } else {
                    // TODO: Do some pre-processing
                    string prettyName = fileInfo.Name;

                    _entries.Add(new OrganizerEntry(fileInfo.Name, prettyName, fileInfo.Extension));
                }
            }
        }
    }
}
