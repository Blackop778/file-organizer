using System.Collections.Generic;

namespace file_organizer.Core {
    public class TransactionHistory {
        private List<ITransaction> transactions;
        private int currentTransactionIndex;

        public TransactionHistory() {
            transactions = new List<ITransaction>();
            currentTransactionIndex = 0;
        }

        public bool Forward() {
            if (currentTransactionIndex >= transactions.Count)
                return false;

            currentTransactionIndex += 1;
            transactions[currentTransactionIndex].Apply();
            return true;
        }

        public bool Backward() {
            if (currentTransactionIndex <= 0)
                return false;

            currentTransactionIndex -= 1;
            transactions[currentTransactionIndex].Apply();
            return true;
        }

        public void AddEntry(ITransaction entry) {
            if (currentTransactionIndex != transactions.Count) {
                transactions.RemoveRange(currentTransactionIndex + 1, transactions.Count - (currentTransactionIndex + 1));
            }
            transactions.Add(entry);
        }

        public void AddAndApplyEntry(ITransaction entry) {
            AddEntry(entry);
            Forward();
        }
    }
}
