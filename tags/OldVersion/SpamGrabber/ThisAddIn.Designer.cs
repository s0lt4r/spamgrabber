//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.312
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpamGrabber2003
{


    /// 
    [Microsoft.VisualStudio.Tools.Applications.Runtime.StartupObjectAttribute(0)]
    [System.Runtime.InteropServices.ComVisibleAttribute(false)]
    [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public sealed partial class ThisAddIn : Microsoft.Office.Tools.AddIn, Microsoft.VisualStudio.Tools.Applications.Runtime.IStartup
    {

        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private Microsoft.VisualStudio.Tools.Applications.Runtime.IRuntimeServiceProvider RuntimeCallback;

        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider HostItemHost;

        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider DataHost;

        private global::System.Object missing = global::System.Type.Missing;

        internal Microsoft.Office.Interop.Outlook.Application Application;

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public ThisAddIn(Microsoft.VisualStudio.Tools.Applications.Runtime.IRuntimeServiceProvider RuntimeCallback)
            :
                base(((Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)(RuntimeCallback.GetService(typeof(Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)))), RuntimeCallback, "AddIn", null, "ThisAddIn")
        {
            this.RuntimeCallback = RuntimeCallback;
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void Initialize()
        {
            this.HostItemHost = ((Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider)(this.RuntimeCallback.GetService(typeof(Microsoft.VisualStudio.Tools.Applications.Runtime.IHostItemProvider))));
            this.DataHost = ((Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider)(this.RuntimeCallback.GetService(typeof(Microsoft.VisualStudio.Tools.Applications.Runtime.ICachedDataProvider))));
            object hostObject = null;
            this.HostItemHost.GetHostObject("Microsoft.Office.Interop.Outlook.Application", "Application", out hostObject);
            this.Application = ((Microsoft.Office.Interop.Outlook.Application)(hostObject));
            Globals.ThisAddIn = this;
            System.Windows.Forms.Application.EnableVisualStyles();
            this.InitializeCachedData();
            this.InitializeControls();
            this.InitializeComponents();
            this.InitializeData();
            this.BeginInitialization();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void FinishInitialization()
        {
            this.InternalStartup();
            this.OnStartup();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public void InitializeDataBindings()
        {
            this.BindToData();
            this.EndInitialization();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public override void OnShutdown()
        {
            base.OnShutdown();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeCachedData()
        {
            if ((this.DataHost == null))
            {
                return;
            }
            if (this.DataHost.IsCacheInitialized)
            {
                this.DataHost.FillCachedData(this);
            }
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeData()
        {
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void BindToData()
        {
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StartCaching(string MemberName)
        {
            this.DataHost.StartCaching(this, MemberName);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StopCaching(string MemberName)
        {
            this.DataHost.StopCaching(this, MemberName);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        private bool IsCached(string MemberName)
        {
            return this.DataHost.IsCached(this, MemberName);
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void BeginInitialization()
        {
            this.BeginInit();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void EndInitialization()
        {
            this.EndInit();
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeControls()
        {
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeComponents()
        {
        }

        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private bool NeedsFill(string MemberName)
        {
            return this.DataHost.NeedsFill(this, MemberName);
        }
    }

    /// 
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal sealed partial class Globals
    {

        private static ThisAddIn _ThisAddIn;

        internal static ThisAddIn ThisAddIn
        {
            get
            {
                return _ThisAddIn;
            }
            set
            {
                if ((_ThisAddIn == null))
                {
                    _ThisAddIn = value;
                }
                else
                {
                    throw new System.NotSupportedException();
                }
            }
        }
    }
}
