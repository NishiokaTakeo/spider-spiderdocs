using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System;
using System.Reflection;

namespace InspectorWrapperExplained {

    /// <summary>
    /// Eventhandler used to correctly clean up resources
    /// </summary>
    /// <param name="id">The unique id of the Inspector instance</param>
    internal delegate void InspectorWrapperClosedEventHandler(Guid id);

    /// <summary>
    /// The base class for all InspectorWrappers
    /// </summary>
    internal abstract class InspectorWrapper {

        /// <summary>
        /// Event notifier for the InspectorWrapper.Closed event.
        /// Is raised when an Inspector has been closed.
        /// </summary>
        public event InspectorWrapperClosedEventHandler Closed;

        /// <summary>
        /// The unique Id the identifies the Inspector Window
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// The Outlook Inspector Instance
        /// </summary>
        public Outlook.Inspector Inspector { get; private set; }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance that should be handled</param>
        public InspectorWrapper(Outlook.Inspector inspector) {
            Id = Guid.NewGuid();
            Inspector = inspector;
            // register for Inspector events here
            ((Outlook.InspectorEvents_10_Event)Inspector).Close += new Outlook.InspectorEvents_10_CloseEventHandler(Inspector_Close);
            ((Outlook.InspectorEvents_10_Event)Inspector).Activate += new Outlook.InspectorEvents_10_ActivateEventHandler(Activate);
            ((Outlook.InspectorEvents_10_Event)Inspector).Deactivate += new Outlook.InspectorEvents_10_DeactivateEventHandler(Deactivate);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMaximize += new Outlook.InspectorEvents_10_BeforeMaximizeEventHandler(BeforeMaximize);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMinimize += new Outlook.InspectorEvents_10_BeforeMinimizeEventHandler(BeforeMinimize);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMove += new Outlook.InspectorEvents_10_BeforeMoveEventHandler(BeforeMove);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeSize += new Outlook.InspectorEvents_10_BeforeSizeEventHandler(BeforeSize);
            ((Outlook.InspectorEvents_10_Event)Inspector).PageChange += new Outlook.InspectorEvents_10_PageChangeEventHandler(PageChange);

            // Initialize is called to give the derived Wrappers a chance to do initialization
            Initialize();
        }

        /// <summary>
        /// Eventhandler for the Inspector close event
        /// </summary>
        private void Inspector_Close() {
            // call the Close Method - the derived classes can implement cleanup code
            // by overriding the Close method
            Close();
            // unregister Inspector events
            ((Outlook.InspectorEvents_10_Event)Inspector).Close -= new Outlook.InspectorEvents_10_CloseEventHandler(Inspector_Close);
            ((Outlook.InspectorEvents_10_Event)Inspector).Activate -= new Outlook.InspectorEvents_10_ActivateEventHandler(Activate);
            ((Outlook.InspectorEvents_10_Event)Inspector).Deactivate -= new Outlook.InspectorEvents_10_DeactivateEventHandler(Deactivate);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMaximize -= new Outlook.InspectorEvents_10_BeforeMaximizeEventHandler(BeforeMaximize);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMinimize -= new Outlook.InspectorEvents_10_BeforeMinimizeEventHandler(BeforeMinimize);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeMove -= new Outlook.InspectorEvents_10_BeforeMoveEventHandler(BeforeMove);
            ((Outlook.InspectorEvents_10_Event)Inspector).BeforeSize -= new Outlook.InspectorEvents_10_BeforeSizeEventHandler(BeforeSize);
            ((Outlook.InspectorEvents_10_Event)Inspector).PageChange -= new Outlook.InspectorEvents_10_PageChangeEventHandler(PageChange);
            // clean up resources and do a GC.Collect();
            Inspector = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            // raise the Close event.
            if (Closed != null) Closed(Id);
        }

        /// <summary>
        /// Method is called after the internal initialization of the Wrapper
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Method gets called when another Page of the Inspector has been selected
        /// </summary>
        /// <param name="ActivePageName">The active page name by reference</param>
        protected virtual void PageChange(ref string ActivePageName) { }

        /// <summary>
        /// Method gets called before the Inspector is resized
        /// </summary>
        /// <param name="Cancel">To prevent resizing set Cancel to true</param>
        protected virtual void BeforeSize(ref bool Cancel) { }

        /// <summary>
        /// Method gets called before the Inspector is moved around
        /// </summary>
        /// <param name="Cancel">To prevent moving set Cancel to true</param>
        protected virtual void BeforeMove(ref bool Cancel) { }

        /// <summary>
        /// Method gets called before the Inspector is minimized
        /// </summary>
        /// <param name="Cancel">To prevent minimizing set Cancel to true</param>
        protected virtual void BeforeMinimize(ref bool Cancel) { }

        /// <summary>
        /// Method gets called before the Inspector is maximized
        /// </summary>
        /// <param name="Cancel">To prevent maximizing set Cancel to true</param>
        protected virtual void BeforeMaximize(ref bool Cancel) { }

        /// <summary>
        /// Method gets called when the Inspector is deactivated
        /// </summary>
        protected virtual void Deactivate() { }

        /// <summary>
        /// Method gets called when the Inspector is activated
        /// </summary>
        protected virtual void Activate() { }

        /// <summary>
        /// Derived classes can do a cleanup by overriding this method.
        /// </summary>
        protected virtual void Close() { }

        /// <summary>
        /// This Fabric method returns a specific InspectorWrapper or null if not handled.
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance</param>
        /// <returns>Returns the specific Wrapper or null</returns>
        public static InspectorWrapper GetWrapperFor(Outlook.Inspector inspector) {

            // retrieve the message class using late binding
            string messageClass = inspector.CurrentItem.GetType().InvokeMember("MessageClass", BindingFlags.GetProperty, null, inspector.CurrentItem, null);

            // depending on a messageclass you can instantiate different Wrappers
            // explicitely for a given MessageClass
            // using a switch statement
            switch (messageClass) {
                case "IPM.Contact":
                    return new ContactItemWrapper(inspector);
                case "IPM.Journal":
                    return new ContactItemWrapper(inspector);
                case "IPM.Note":
                    return new MailItemWrapper(inspector);
                case "IPM.Post":
                    return new PostItemWrapper(inspector);
                case "IPM.Task":
                    return new TaskItemWrapper(inspector);
            }

            // or check if the messageclass begins with a specific fragment
            if (messageClass.StartsWith ("IPM.Contact.X4U")){
                return new X4UContactItemWrapper(inspector);
            }

            // or check the interface type of the Item
            if (inspector.CurrentItem is Outlook.AppointmentItem) {
                return new AppointmentItemWrapper(inspector);
            }

            // no wrapper found
            return null;
        }
    }
}
