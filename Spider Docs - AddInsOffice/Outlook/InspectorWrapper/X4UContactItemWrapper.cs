using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System;

namespace InspectorWrapperExplained {

    /// <summary>
    /// For example the X4U ContactItemWrapper is more specific compared to the ContactItemWrapper class
    /// It should handle all ContactItems having a MessageClass beginning with "IPM.Contact.X4U"
    /// </summary>
    internal class X4UContactItemWrapper : ContactItemWrapper  {

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="inspector">The Outlook Inspector instance that should be handled</param>
        public X4UContactItemWrapper(Outlook.Inspector inspector)
            : base(inspector) {
        }
    
    }
}
