using System;
using System.Collections.Generic;

namespace file_organizer.Core {
    public class OrganizerEntry : IComparable {
        private List<string> _extensions;

        public string PrettyName { get; private set; }
        public string FileName { get; private set; }
        public IReadOnlyCollection<string> Extensions => _extensions.AsReadOnly();
        public int Number { get; set; }

        public OrganizerEntry(string fileName, string prettyName, string extension, int number) {
            _extensions = new List<string>();

            FileName = fileName;
            PrettyName = prettyName;
            _extensions.Add(extension);
            Number = number;
        }

        public void AddExtension(string extension) {
            _extensions.Add(extension);
        }

        public int CompareTo(object obj) {
            if (obj is OrganizerEntry other) {
                if (Number != other.Number)
                    return Number - other.Number;

                return PrettyName.CompareTo(other.PrettyName);
            }

            return 1;
        }
    }
}
