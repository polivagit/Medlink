﻿#pragma checksum "C:\GIT\Medlink\UWP_MedlinkApp\UWP_MedlinkApp\View\Pages\LoginPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "89FCD4EF4E0D5EEA0420BB00792AE7EB1564541F861431095A7B43666486563B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWP_MedlinkApp.View.Pages
{
    partial class LoginPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // View\Pages\LoginPage.xaml line 1
                {
                    this.Login = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)this.Login).Loaded += this.Login_Loaded;
                }
                break;
            case 2: // View\Pages\LoginPage.xaml line 83
                {
                    this.btnLogin = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 3: // View\Pages\LoginPage.xaml line 67
                {
                    this.txbLogPswd = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // View\Pages\LoginPage.xaml line 48
                {
                    this.txbLogUsnm = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

