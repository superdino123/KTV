﻿#pragma checksum "..\..\..\Views\SongInfoEditUc.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D3258BE87369235CBB8922C26D2028591296D6BD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using ClientControls.Behaviours;
using KtvStudio.Views;
using MahApps.Metro.Controls;
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


namespace KtvStudio.Views {
    
    
    /// <summary>
    /// SongInfoEditUc
    /// </summary>
    public partial class SongInfoEditUc : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KtvStudio.Views.SongInfoEditUc ThisUc;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SingerNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo1;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo2;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement vedio;
        
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
            System.Uri resourceLocater = new System.Uri("/KtvStudio;component/views/songinfoedituc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\SongInfoEditUc.xaml"
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
            this.ThisUc = ((KtvStudio.Views.SongInfoEditUc)(target));
            return;
            case 2:
            this.toggleButtonBaseInfo = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 3:
            
            #line 64 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MusicNameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SingerNameComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.toggleButtonBaseInfo1 = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 6:
            
            #line 148 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FirstCategoryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 156 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SecondCategoryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 168 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.DataGrid)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectedCategoryDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.toggleButtonBaseInfo2 = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 10:
            this.vedio = ((System.Windows.Controls.MediaElement)(target));
            return;
            case 11:
            
            #line 218 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

