using System.Collections.Generic;

namespace file_organizer.Core {
    public class TransactionHistory {
        private List<ITransaction> transactions;
        private int currentTransactionIndex;

        public TransactionHistory() {
            transactions = new List<ITransaction>();
            currentTransactionIndex = -1;
        }

        public bool Forward() {
            if (!CanForward())
                return false;

            currentTransactionIndex += 1;
            transactions[currentTransactionIndex].Apply();
            return true;
        }

        public bool Backward() {
            if (!CanBackward())
                return false;

            transactions[currentTransactionIndex].Undo();
            currentTransactionIndex -= 1;
            return true;
        }

        public void AddEntry(ITransaction entry) {
            if (currentTransactionIndex != transactions.Count - 1) {
                transactions.RemoveRange(currentTransactionIndex + 1, transactions.Count - (currentTransactionIndex + 1));
            }
            transactions.Add(entry);
        }

        public void AddAndApplyEntry(ITransaction entry) {
            AddEntry(entry);
            Forward();
        }

        public bool CanForward() {
            return currentTransactionIndex < (transactions.Count - 1) && transactions.Count != 0;
        }

        public bool CanBackward() {
            return currentTransactionIndex >= 0;
        }
    }
}
