﻿#pragma checksum "..\..\..\Pages\UserPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A11D94EDEEECCCA17F740C273CE3ACF401DBED3A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LovePlus.Pages {
    
    
    /// <summary>
    /// UserPage
    /// </summary>
    public partial class UserPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SettingLabel;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label WelcomeLabel;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FindLove;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LeaveAccount;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoseLove;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteAccount;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Pages\UserPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatusLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LovePlus;component/pages/userpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\UserPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SettingLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.WelcomeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.FindLove = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Pages\UserPage.xaml"
            this.FindLove.Click += new System.Windows.RoutedEventHandler(this.FindLove_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LeaveAccount = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Pages\UserPage.xaml"
            this.LeaveAccount.Click += new System.Windows.RoutedEventHandler(this.LeaveAccount_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LoseLove = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Pages\UserPage.xaml"
            this.LoseLove.Click += new System.Windows.RoutedEventHandler(this.LoseLove_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DeleteAccount = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Pages\UserPage.xaml"
            this.DeleteAccount.Click += new System.Windows.RoutedEventHandler(this.DeleteAccount_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.StatusLabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

