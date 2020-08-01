using System.Collections.Generic;

namespace file_organizer.Core {
    public class RemoveTransaction : MoveTransaction {
        private List<OrganizerEntry> removedEntries;
        private Controller controller;

        public MoveTransaction(Controller controller) {
            entries = new List<MoveTransactionEntry>();
            this.controller = controller;
        }

        public void AddRemoveEntry(OrganizerEntry item) {
            entries.Add(new MoveTransactionEntry(item, fromIndex, toIndex));
        }

        public void Apply() {
            base.Apply();

            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.ToIndex;
            }
        }

        public void Undo() {
            base.Apply();
            
            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.FromIndex;
            }
        }
    }
}
