﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "65073B93F76B43A38AE629CB7E0111A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Expression.Interactivity.Core;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using Microsoft.Windows.Themes;
using SyncButlerUI;
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
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Explorer_Tree;


namespace SyncButlerUI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\MainWindow.xaml"
        internal SyncButlerUI.MainWindow Window;
        
        #line default
        #line hidden
        
        
        #line 336 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 344 "..\..\MainWindow.xaml"
        internal SyncButlerUI.HomeWindowControl homeWindow1;
        
        #line default
        #line hidden
        
        
        #line 345 "..\..\MainWindow.xaml"
        internal System.Windows.Shapes.Rectangle Sidebar;
        
        #line default
        #line hidden
        
        
        #line 346 "..\..\MainWindow.xaml"
        internal System.Windows.Shapes.Rectangle Sidebar_Copy;
        
        #line default
        #line hidden
        
        
        #line 354 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button HomeButton;
        
        #line default
        #line hidden
        
        
        #line 355 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button SettingsButton;
        
        #line default
        #line hidden
        
        
        #line 356 "..\..\MainWindow.xaml"
        internal System.Windows.Controls.Button SyncButlerSyncButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SyncButlerUI;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Window = ((SyncButlerUI.MainWindow)(target));
            
            #line 13 "..\..\MainWindow.xaml"
            this.Window.Closing += new System.ComponentModel.CancelEventHandler(this.cleanUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.homeWindow1 = ((SyncButlerUI.HomeWindowControl)(target));
            return;
            case 4:
            this.Sidebar = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 5:
            this.Sidebar_Copy = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.HomeButton = ((System.Windows.Controls.Button)(target));
            
            #line 354 "..\..\MainWindow.xaml"
            this.HomeButton.Click += new System.Windows.RoutedEventHandler(this.goHome);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SettingsButton = ((System.Windows.Controls.Button)(target));
            return;
            case 8:
            this.SyncButlerSyncButton = ((System.Windows.Controls.Button)(target));
            
            #line 356 "..\..\MainWindow.xaml"
            this.SyncButlerSyncButton.Click += new System.Windows.RoutedEventHandler(this.goToSyncButlerSync);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
