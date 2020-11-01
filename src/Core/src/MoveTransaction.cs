using System.Collections.Generic;

namespace file_organizer.Core {
    public class MoveTransaction : ITransaction {
        protected readonly Controller controller;
        private List<MoveTransactionEntry> entries;

        public MoveTransaction(Controller controller) {
            entries = new List<MoveTransactionEntry>();
            this.controller = controller;
        }

        public void AddMoveEntry(OrganizerEntry item, int fromIndex, int toIndex) {
            entries.Add(new MoveTransactionEntry(item, fromIndex, toIndex));
        }

        public virtual void Apply() {
            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.ToIndex;
            }
            controller.SortEntries();
        }

        public virtual void Undo() {
            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.FromIndex;
            }
            controller.SortEntries();
        }

        private class MoveTransactionEntry {
            public OrganizerEntry Item { get; private set; }
            public int FromIndex { get; private set; }
            public int ToIndex { get; private set; }

            public MoveTransactionEntry(OrganizerEntry item, int fromIndex, int toIndex) {
                Item = item;
                FromIndex = fromIndex;
                ToIndex = toIndex;
            }
        }
    }
}
