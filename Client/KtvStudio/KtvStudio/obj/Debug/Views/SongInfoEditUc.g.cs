﻿#pragma checksum "..\..\..\Views\SongInfoEditUc.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84A558D9ED8F2AA5350274EBAEB625128D63EE74"
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
using Fluent;
using Fluent.Converters;
using Fluent.Metro.Behaviours;
using Fluent.Metro.Controls;
using KtvStudio.Helpers.Converters;
using KtvStudio.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
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
        
        
        #line 13 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal KtvStudio.Views.SongInfoEditUc ThisUc;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SingerNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo1;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.MetroTabControl SelectedCategorysTabControl;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton toggleButtonBaseInfo2;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement vedio;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sliderPosition;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\Views\SongInfoEditUc.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock vedioAllTime;
        
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
            
            #line 75 "..\..\..\Views\SongInfoEditUc.xaml"
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
            
            #line 150 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FirstCategoryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 158 "..\..\..\Views\SongInfoEditUc.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SecondCategoryComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SelectedCategorysTabControl = ((MahApps.Metro.Controls.MetroTabControl)(target));
            
            #line 162 "..\..\..\Views\SongInfoEditUc.xaml"
            this.SelectedCategorysTabControl.TabItemClosingEvent += new MahApps.Metro.Controls.BaseMetroTabControl.TabItemClosingEventHandler(this.SelectedCategorysTabControl_TabItemClosingEvent);
            
            #line default
            #line hidden
            return;
            case 9:
            this.toggleButtonBaseInfo2 = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 10:
            this.vedio = ((System.Windows.Controls.MediaElement)(target));
            
            #line 210 "..\..\..\Views\SongInfoEditUc.xaml"
            this.vedio.MediaOpened += new System.Windows.RoutedEventHandler(this.vedio_MediaOpened);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 212 "..\..\..\Views\SongInfoEditUc.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Replay_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 217 "..\..\..\Views\SongInfoEditUc.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayOrPause_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 222 "..\..\..\Views\SongInfoEditUc.xaml"
            ((Fluent.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Stop_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.sliderPosition = ((System.Windows.Controls.Slider)(target));
            
            #line 230 "..\..\..\Views\SongInfoEditUc.xaml"
            this.sliderPosition.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.sliderPosition_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            this.vedioAllTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            
            #line 255 "..\..\..\Views\SongInfoEditUc.xaml"
            ((MahApps.Metro.Controls.ToggleSwitch)(target)).IsCheckedChanged += new System.EventHandler(this.PreviewType_IsCheckedChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

