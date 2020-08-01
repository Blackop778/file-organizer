namespace file_organizer.Core {
    public interface ITransaction {
        /**
         * Does not sort the controller's entries afterwards
         */
        void Apply();
        /**
         * Does not sort the controller's entries afterwards
         */
        void Undo();
    }
}
