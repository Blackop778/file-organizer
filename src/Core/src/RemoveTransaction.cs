using System.Collections.Generic;

namespace file_organizer.Core {
    public class RemoveTransaction : MoveTransaction {
        private List<OrganizerEntry> removedEntries;

        public RemoveTransaction(Controller controller) : base(controller) {
            removedEntries = new List<OrganizerEntry>();
        }

        public void AddRemoveEntry(OrganizerEntry item) {
            removedEntries.Add(item);
        }

        public override void Apply() {
            foreach (OrganizerEntry entry in removedEntries)
            {
                controller._entries.Remove(entry);
            }

            base.Apply();
        }

        public override void Undo() {
            foreach (OrganizerEntry entry in removedEntries)
            {
                controller._entries.Add(entry);
            }

            base.Apply();
        }
    }
}
