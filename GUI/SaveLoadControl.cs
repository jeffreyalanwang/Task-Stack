using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_Stack.State;
using static Task_Stack.GUI.SaveLoadControl;

namespace Task_Stack.GUI
{
    // All instances of [ ProgramState ] originate in, and are destroyed by, this file.
    // In addition, SaveLoadControl is the exclusive runtime accessor/mutator of:
    // - [ ProgramState.SaveFile ]
    // - [ ProgramState.save() ]
    public partial class SaveLoadControl : UserControl
    {
        #region Fields + constructor
        private Func<bool> _getUnsavedStateChanges = null;
        private Action _callProgramStateSave = null;
        private SetState _setGlobalProgramState;

        // Used by SaveLoadControl to change the program's global state when "Load" function is used.
        public delegate void SetState(ProgramState newState);
        public SaveLoadControl()
        {            
            InitializeComponent();
            InitializeBackgroundWorker();

            // At this time, there is no ProgramState created yet.
            this._canSave = false;
            this._needsSave = false;
            this._canLoad = ProgramState.LoadFileAvailable();
            this._canCreate = true;
        }
        // Call immediately after constructor.
        public void InitializeToState(SetState setGlobalProgramState)
        {
            this._setGlobalProgramState = setGlobalProgramState;
        }
        #endregion Fields + constructor

        #region backgroundWorker1 boilerplate
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerReportsProgress = false;
            backgroundWorker1.WorkerSupportsCancellation = false;

            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        private List<DoWorkEventHandler> backgroundWorker1_registeredTasks = new List<DoWorkEventHandler>();
        private List<RunWorkerCompletedEventHandler> backgroundWorker1_onCompleted_registeredTasks = new List<RunWorkerCompletedEventHandler>();
        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Clear tasks for background worker
            for (int i = 0; i < backgroundWorker1_registeredTasks.Count; i++)
            {
                backgroundWorker1.DoWork -= backgroundWorker1_registeredTasks[i];
                backgroundWorker1_registeredTasks.RemoveAt(i);
            }

            // Did our thread throw an error?
            if (e.Error != null)
            {
                MessageBox.Show($"Error: {e.Error.ToString()}");
            }

            // Run all requested onCompleted tasks
            for (int i = 0; i < backgroundWorker1_onCompleted_registeredTasks.Count; i++)
            {
                RunWorkerCompletedEventHandler task = backgroundWorker1_onCompleted_registeredTasks[i];
                task(sender, e);
                backgroundWorker1_onCompleted_registeredTasks.RemoveAt(i);
            }

            // Reset cursor
            Cursor.Current = Cursors.Default;
        }
        #endregion backgroundWorker1 boilerplate

        #region UI handlers
        private void NewButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) // If multiple clicks before backgroundWorker1 is done working
            {
                return;
            }

            // Display loading cursor until done
            Cursor.Current = Cursors.WaitCursor; // Gets reset by default in [ backgroundWorker1_RunWorkerCompleted ]

            // Run background task in another thread

            // Prepare background task: call createState()
            backgroundWorker1_registeredTasks.Add(createState);
            backgroundWorker1.DoWork += createState;

            // Prepare handler that runs on completion
            RunWorkerCompletedEventHandler onCompletedTask = (object _sender, RunWorkerCompletedEventArgs _e) =>
            {
                // Change UI buttons
                this._needsSave = true;
                this._canCreate = false;
                this._canLoad = false;
                // Update the global program state with the newly-created state
                this._setGlobalProgramState((ProgramState)_e.Result);
            };
            Debug.Assert(this.backgroundWorker1_onCompleted_registeredTasks.Count == 0);
            backgroundWorker1_onCompleted_registeredTasks.Add(onCompletedTask);

            // Execute
            backgroundWorker1.RunWorkerAsync();
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) // If multiple clicks before backgroundWorker1 is done working
            {
                return;
            }

            // Display loading cursor until done
            Cursor.Current = Cursors.WaitCursor; // Gets reset by default in [ backgroundWorker1_RunWorkerCompleted ]

            // Run background task in another thread

            // Prepare background task: call loadState()
            backgroundWorker1_registeredTasks.Add(loadState);
            backgroundWorker1.DoWork += loadState;

            // Prepare handler that runs on completion
            RunWorkerCompletedEventHandler onCompletedTask = (object _sender, RunWorkerCompletedEventArgs _e) =>
            {
                // Change UI buttons
                this._needsSave = false;
                this._canCreate = false;
                this._canLoad = false;
                // Update the global program state with the newly-created state
                this._setGlobalProgramState((ProgramState)_e.Result);
            };
            Debug.Assert(this.backgroundWorker1_onCompleted_registeredTasks.Count == 0);
            backgroundWorker1_onCompleted_registeredTasks.Add(onCompletedTask);

            // Execute
            backgroundWorker1.RunWorkerAsync();
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) // If multiple clicks before backgroundWorker1 is done working
            {
                return;
            }

            // Display loading cursor until done
            Cursor.Current = Cursors.AppStarting; // Gets reset by default in [ backgroundWorker1_RunWorkerCompleted ]

            // Run background task in another thread

            // Prepare background task: call saveState()
            backgroundWorker1_registeredTasks.Add(saveState);
            backgroundWorker1.DoWork += saveState;

            // Prepare handler that runs on completion
            RunWorkerCompletedEventHandler onCompletedTask = (object _sender, RunWorkerCompletedEventArgs _e) =>
            {
                // Change UI buttons
                this._needsSave = false;
                this._canCreate = false;
                this._canLoad = false;
            };
            Debug.Assert(this.backgroundWorker1_onCompleted_registeredTasks.Count == 0);
            backgroundWorker1_onCompleted_registeredTasks.Add(onCompletedTask);

            // Execute
            backgroundWorker1.RunWorkerAsync();
        }
        #endregion UI handlers
        #region State change handlers
        public void OnStateChanged()
        {
            // if unsaved changes (state.UnsavedChanges), change UI to reflect unsaved changes (_needsSave)
            this._needsSave = this._getUnsavedStateChanges();
        }
        #endregion State change handlers

        private void saveState(object sender, DoWorkEventArgs e)
        {
            _callProgramStateSave();
            e.Result = true;
        }
        private void createState(object sender, DoWorkEventArgs e)
        {
            // Create a blank new ProgramState
            ProgramState newState = new ProgramState();            
            // Register as a listener for the state
            newState.OnDataChange(this.OnStateChanged);
            // Set _callProgramStateSave, _getUnsavedStateChanges
            this._callProgramStateSave = newState.Save;
            this._getUnsavedStateChanges = () => newState.UnsavedChanges;
            // Pass the new ProgramState on to do a few UI operations/pass it on to the parent Control
            e.Result = newState;
        }
        // Load a ProgramState from default save file location
        private void loadState(object sender, DoWorkEventArgs e)
        {
            // Load a new ProgramState from given filePath or default location (null)
            ProgramState loadedState = this.loadState((Uri)e.Argument);
            // Pass the new ProgramState on to do a few UI operations/pass it on to the parent Control
            e.Result = loadedState;
        }
        // Load a new ProgramState from given filePath
        private ProgramState loadState(Uri filePath)
        {
            // Load a new ProgramState from given filePath
            ProgramState loadedState = new ProgramState(filePath);
            // Register as a listener for the state
            loadedState.OnDataChange(this.OnStateChanged);
            // Set _callProgramStateSave, _getUnsavedStateChanges
            this._callProgramStateSave = loadedState.Save;
            this._getUnsavedStateChanges = () => loadedState.UnsavedChanges;
            // Pass the new ProgramState on to do a few UI operations/pass it on to the parent Control
            return loadedState;
        }

        // Saving is disabled on program start (but should not be if a program state is instantiated automatically at program start)
        // Saving is enabled once a ProgramState is available.
        // Implicitly mutated by setting [ this._needsSave ].
        private bool _canSave
        {
            set => this.SaveButton.Enabled = value;
        }
        // Loading a file is enabled on program start if there is an available load file
        // Loading a file is *currently* disabled once a ProgramState is available
        private bool _canLoad
        {
            set => this.LoadButton.Enabled = value;
        }
        // Creating a file is always enabled on program start
        // Creating a file is *currently* disabled once a ProgramState is available
        private bool _canCreate
        {
            set => this.NewButton.Enabled = value;
        }

        // Whether a change has been made to the program state (resulting in need to save).
        private bool _needsSave
        {
            set
            {
                Debug.Assert(this.Visible);
                // Debug.Print($"SaveLoadControl._needsSave set to {value}."); // Debug
                this._canSave = value;
                this.ModificationIndicator.Visible = value;
            }
        }
    }
}
