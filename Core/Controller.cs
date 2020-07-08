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

            int lastNumber = 0;
            foreach (string file in files) {
                FileInfo fileInfo = new FileInfo(file);

                if (_entries.Count > 0 && _entries[_entries.Count - 1].FileName.Equals(Path.GetFileNameWithoutExtension(fileInfo.Name))) {
                    _entries[_entries.Count - 1].AddExtension(fileInfo.Extension);
                } else {
                    // TODO: Do some pre-processing
                    string prettyName = fileInfo.Name;

                    int number;
                    char[] chars = fileInfo.Name.ToCharArray();
                    int i;
                    for (i = 0; i < chars.Length && chars[i] >= '0' && chars[i] <= '9'; i += 1);

                    if (i == 0)
                    {
                        number = lastNumber + 1;
                    }
                    else
                    {
                        number = Convert.ToInt32(fileInfo.Name.Remove(i));
                        prettyName = Path.GetFileNameWithoutExtension(fileInfo.Name).Substring(i);
                    }
                    lastNumber = number;

                    _entries.Add(new OrganizerEntry(Path.GetFileNameWithoutExtension(fileInfo.Name), prettyName, fileInfo.Extension, number));
                }
            }

            SortEntries();
        }

        private void SortEntries()
        {
            _entries.Sort();
        }

        public void DisableEntry(string prettyName) {
            _entries.Find(entry => entry.PrettyName.Equals(prettyName)).Disable();
        }
    }
}
