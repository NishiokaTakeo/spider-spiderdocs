using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System;

namespace InspectorWrapperExplained {

    /// <summary>
    /// We derive a Wrapper for each MessageClass / ItemType
    /// </summary>
    internal class AppointmentItemWrapper : InspectorWrapper {

        /// <summary>
        /// The Object instance behind the Inspector (CurrentItem)
        /// </summary>
        public Outlook.AppointmentItem Item { get; private set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance that should be handled</param>
        public AppointmentItemWrapper(Outlook.Inspector inspector)
            : base(inspector) {
        }

        /// <summary>
        /// Method is called when the Wrapper has been initialized
        /// </summary>
        protected override void Initialize() {
            // Get the Item of the current Inspector
            Item = (Outlook.AppointmentItem)Inspector.CurrentItem;

            // Register for the Item events
            Item.Open += new Outlook.ItemEvents_10_OpenEventHandler(Item_Open);
            Item.Write += new Outlook.ItemEvents_10_WriteEventHandler(Item_Write);
        }

        /// <summary>
        /// This Method is called when the Item is visible and the UI is initialized.
        /// </summary>
        /// <param name="Cancel">When you set this property to true, the Inspector is closed.</param>
        void Item_Open(ref bool Cancel) {
        }

        /// <summary>
        /// This Method is called when the Item is saved.
        /// </summary>
        /// <param name="Cancel">When set to true, the save operation is cancelled</param>
        void Item_Write(ref bool Cancel) {
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

            // Release references to COM objects
            Item = null;

            // required, just stting to NULL may keep a reference in memory of the Garbage Collector.
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}

