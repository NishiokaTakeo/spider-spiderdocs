using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System;


namespace InspectorWrapperExplained {
    class TaskItemWrapper  : InspectorWrapper {

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance that should be handled</param>
        public TaskItemWrapper(Outlook.Inspector inspector)
            : base(inspector) {
        }

        /// <summary>
        /// The Object instance behind the Inspector (CurrentItem)
        /// </summary>
        public Outlook.TaskItem Item { get; private set; }

        /// <summary>
        /// Method is called when the Wrapper has been initialized
        /// </summary>
        protected override void Initialize() {
            // Get the Item of the current Inspector
            Item = (Outlook.TaskItem)Inspector.CurrentItem;

            // Register for the Item events
            Item.Open += new Outlook.ItemEvents_10_OpenEventHandler(Item_Open);
            Item.Write += new Outlook.ItemEvents_10_WriteEventHandler(Item_Write);
        }

        /// <summary>
        /// This Method is called when the Item is saved.
        /// </summary>
        /// <param name="Cancel">When set to true, the save operation is cancelled</param>
        void Item_Write(ref bool Cancel) {
            
        }

        /// <summary>
        /// This Method is called when the Item is visible and the UI is initialized.
        /// </summary>
        /// <param name="Cancel">When you set this property to true, the Inspector is closed.</param>
        void Item_Open(ref bool Cancel) {
            
        }

        /// <summary>
        /// The Close Method is called when the Inspector has been closed.
        /// Do your cleanup tasks here.
        /// The UI is gone, can't access it here.
        /// </summary>
        protected override void Close() {
            // unregister events
            Item.Write -= new Outlook.ItemEvents_10_WriteEventHandler(Item_Write);
            Item.Open -= new Outlook.ItemEvents_10_OpenEventHandler(Item_Open);

            // required, just stting to NULL may keep a reference in memory of the Garbage Collector.
            Item = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
