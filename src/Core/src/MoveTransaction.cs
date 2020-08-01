using System.Collections.Generic;

namespace file_organizer.Core {
    public class MoveTransaction : ITransaction {
        private List<MoveTransactionEntry> entries;

        public MoveTransaction() {
            entries = new List<MoveTransactionEntry>();
        }

        public void AddMoveEntry(OrganizerEntry item, int fromIndex, int toIndex) {
            entries.Add(new MoveTransactionEntry(item, fromIndex, toIndex));
        }

        public void Apply() {
            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.ToIndex;
            }
        }

        public void Undo() {
            foreach(MoveTransactionEntry entry in entries) {
                entry.Item.Number = entry.FromIndex;
            }
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
