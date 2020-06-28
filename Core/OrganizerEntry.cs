using System.Collections.Generic;

namespace file_organizer.Core {
    public class OrganizerEntry {
        private List<string> _extensions;

        public string PrettyName { get; private set; }
        public string FileName { get; private set; }
        public IReadOnlyCollection<string> Extensions => _extensions.AsReadOnly();

        public OrganizerEntry(string fileName, string prettyName, string extension) {
            _extensions = new List<string>();

            FileName = fileName;
            PrettyName = prettyName;
            _extensions.Add(extension);
        }

        public void AddExtension(string extension) {
            _extensions.Add(extension);
        }

        public override int GetHashCode() {
            return FileName.GetHashCode();
        }
    }
}
