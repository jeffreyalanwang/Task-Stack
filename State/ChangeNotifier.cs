using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Stack.State
{
    // Data-change callback management.
    public class ChangeNotifier
    {
        public delegate void DataChangeReaction(); // Other classes (e.g. UI) can register a DataChangeReaction

        // We call all callbacks in list on change
        private List<DataChangeReaction> dataChangeCallbacks = new List<DataChangeReaction>();

        // Register a change to this object.
        public void OnDataChange(DataChangeReaction callback)
        {
            this.dataChangeCallbacks.Add(callback);
        }

        // Notify anyone watching *this* task for changes.
        protected void TriggerDataChange()
        {
            this.dataChangeCallbacks.ForEach((callback) => callback());
        }

        protected void ClearChangeListeners() => this.dataChangeCallbacks.Clear();
    }
}
