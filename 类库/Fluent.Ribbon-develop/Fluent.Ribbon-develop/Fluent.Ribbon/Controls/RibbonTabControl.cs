﻿// ReSharper disable once CheckNamespace
namespace Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using ControlzEx.Standard;
    using Fluent.Internal.KnownBoxes;

    /// <summary>
    /// Represents ribbon tab control
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(RibbonTabItem))]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_TabsContainer", Type = typeof(IScrollInfo))]
    [TemplatePart(Name = "PART_ToolbarPanel", Type = typeof(Panel))]
    [TemplatePart(Name = "PART_SelectedContentPresenter", Type = typeof(ContentPresenter))]
    public class RibbonTabControl : Selector, IDropDownControl
    {
        /// <summary>
        /// Default value for <see cref="ContentGapHeight"/>.
        /// </summary>
        public const double DefaultContentGapHeight = 1;

        /// <summary>
        /// Default value for <see cref="ContentHeight"/>.
        /// </summary>
        public const double DefaultContentHeight = 94;

        #region Fields

        // Collection of toolbar items
        private ObservableCollection<UIElement> toolBarItems;

        // ToolBar panel

        #endregion

        #region Events

        /// <summary>
        /// Event which is fired when the, maybe listening, <see cref="Backstage"/> should be closed
        /// </summary>
        public event EventHandler RequestBackstageClose;

        #endregion

        #region Properties

        #region Menu

        /// <summary>
        /// Gets or sets file menu control (can be application menu button, backstage button and so on)
        /// </summary>
        public UIElement Menu
        {
            get { return (UIElement)this.GetValue(MenuProperty); }
            set { this.SetValue(MenuProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Button.
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty MenuProperty =
            DependencyProperty.Register(nameof(Menu), typeof(UIElement),
            typeof(RibbonTabControl), new PropertyMetadata());

        #endregion

        /// <summary>
        /// Gets drop down popup
        /// </summary>
        public Popup DropDownPopup { get; private set; }

        /// <summary>
        /// Gets the <see cref="ContentPresenter"/> responsible for displaying the selected tabs content.
        /// </summary>
        public ContentPresenter SelectedContentPresenter { get; private set; }

        /// <summary>
        /// Gets a value indicating whether context menu is opened
        /// </summary>
        public bool IsContextMenuOpened { get; set; }

        /// <summary>
        /// Gets content of selected tab item
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedContent
        {
            get
            {
                return this.GetValue(SelectedContentProperty);
            }

            internal set
            {
                this.SetValue(SelectedContentPropertyKey, value);
            }
        }

        // DependencyProperty key for SelectedContent
        private static readonly DependencyPropertyKey SelectedContentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedContent), typeof(object), typeof(RibbonTabControl), new PropertyMetadata());

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="SelectedContent"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SelectedContentProperty = SelectedContentPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets whether ribbon is minimized
        /// </summary>
        public bool IsMinimized
        {
            get { return (bool)this.GetValue(IsMinimizedProperty); }
            set { this.SetValue(IsMinimizedProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="IsMinimized"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsMinimizedProperty = DependencyProperty.Register(nameof(IsMinimized), typeof(bool), typeof(RibbonTabControl), new PropertyMetadata(BooleanBoxes.FalseBox, OnMinimizedChanged));

        /// <summary>
        /// Gets or sets whether ribbon can be minimized
        /// </summary>
        public bool CanMinimize
        {
            get { return (bool)this.GetValue(CanMinimizeProperty); }
            set { this.SetValue(CanMinimizeProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="CanMinimize"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty CanMinimizeProperty = DependencyProperty.Register(nameof(CanMinimize), typeof(bool), typeof(RibbonTabControl), new PropertyMetadata(BooleanBoxes.TrueBox));

        /// <summary>
        /// Gets or sets whether ribbon popup is opened
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)this.GetValue(IsDropDownOpenProperty); }
            set { this.SetValue(IsDropDownOpenProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="IsDropDownOpen"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(nameof(IsDropDownOpen), typeof(bool), typeof(RibbonTabControl), new PropertyMetadata(BooleanBoxes.FalseBox, OnIsDropDownOpenChanged, CoerceIsDropDownOpen));

        private static object CoerceIsDropDownOpen(DependencyObject d, object basevalue)
        {
            var tabControl = d as RibbonTabControl;

            if (tabControl == null)
            {
                return basevalue;
            }

            if (!tabControl.IsMinimized)
            {
                return false;
            }

            return basevalue;
        }

        /// <summary>
        /// Defines if the currently selected item should draw it's highlight/selected borders
        /// </summary>
        public bool HighlightSelectedItem
        {
            get { return (bool)this.GetValue(HighlightSelectedItemProperty); }
            set { this.SetValue(HighlightSelectedItemProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="HighlightSelectedItem"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty HighlightSelectedItemProperty =
            DependencyProperty.RegisterAttached("HighlightSelectedItem", typeof(bool), typeof(RibbonTabControl), new FrameworkPropertyMetadata(BooleanBoxes.TrueBox, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets whether ribbon tabs can scroll
        /// </summary>
        internal bool CanScroll
        {
            get
            {
                var scrollInfo = this.GetTemplateChild("PART_TabsContainer") as IScrollInfo;
                if (scrollInfo != null)
                {
                    return scrollInfo.ExtentWidth > scrollInfo.ViewportWidth;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets selected tab item
        /// </summary>
        internal RibbonTabItem SelectedTabItem
        {
            get { return (RibbonTabItem)this.GetValue(SelectedTabItemProperty); }
            private set { this.SetValue(SelectedTabItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTabItem.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty SelectedTabItemProperty =
            DependencyProperty.Register(nameof(SelectedTabItem), typeof(RibbonTabItem), typeof(RibbonTabControl), new PropertyMetadata());

        /// <summary>
        /// Gets collection of ribbon toolbar items
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<UIElement> ToolBarItems
        {
            get
            {
                if (this.toolBarItems == null)
                {
                    this.toolBarItems = new ObservableCollection<UIElement>();
                    this.toolBarItems.CollectionChanged += this.OnToolbarItemsCollectionChanged;
                }

                return this.toolBarItems;
            }
        }

        internal Panel ToolbarPanel { get; private set; }

        // Handle toolbar iitems changes
        private void OnToolbarItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.ToolbarPanel == null)
            {
                return;
            }

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (var i = 0; i < e.NewItems.Count; i++)
                    {
                        this.ToolbarPanel.Children.Insert(e.NewStartingIndex + i, (UIElement)e.NewItems[i]);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var obj3 in e.OldItems.OfType<UIElement>())
                    {
                        this.ToolbarPanel.Children.Remove(obj3);
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (var obj4 in e.OldItems.OfType<UIElement>())
                    {
                        this.ToolbarPanel.Children.Remove(obj4);
                    }

                    foreach (var obj5 in e.NewItems.OfType<UIElement>())
                    {
                        this.ToolbarPanel.Children.Add(obj5);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.ToolbarPanel.Children.Clear();
                    foreach (var toolBarItem in this.ToolBarItems)
                    {
                        this.ToolbarPanel.Children.Add(toolBarItem);
                    }

                    break;
            }
        }

        /// <summary>
        /// Gets or sets the height of the content area.
        /// </summary>
        public double ContentHeight
        {
            get { return (double)this.GetValue(ContentHeightProperty); }
            set { this.SetValue(ContentHeightProperty, value); }
        }

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="ContentHeight"/>.
        /// </summary>
        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register(nameof(ContentHeight), typeof(double), typeof(RibbonTabControl), new FrameworkPropertyMetadata(DefaultContentHeight, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the height of the gap between the ribbon and the content
        /// </summary>
        public double ContentGapHeight
        {
            get { return (double)this.GetValue(ContentGapHeightProperty); }
            set { this.SetValue(ContentGapHeightProperty, value); }
        }

        /// <summary>
        /// DependencyProperty for <see cref="ContentGapHeight"/>
        /// </summary>
        public static readonly DependencyProperty ContentGapHeightProperty =
            DependencyProperty.Register(nameof(ContentGapHeight), typeof(double), typeof(RibbonTabControl), new PropertyMetadata(DefaultContentGapHeight));

        /// <summary>
        /// DependencyProperty for <see cref="IsMouseWheelScrollingEnabled"/>
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelScrollingEnabledProperty = DependencyProperty.Register(nameof(IsMouseWheelScrollingEnabled), typeof(bool), typeof(RibbonTabControl), new PropertyMetadata(BooleanBoxes.TrueBox));

        /// <summary>
        /// Defines wether scrolling by mouse wheel is enabled or not.
        /// </summary>
        public bool IsMouseWheelScrollingEnabled
        {
            get { return (bool)this.GetValue(IsMouseWheelScrollingEnabledProperty); }
            set { this.SetValue(IsMouseWheelScrollingEnabledProperty, value); }
        }

        #endregion

        #region Initializion

        /// <summary>
        /// Initializes static members of the <see cref="RibbonTabControl"/> class.
        /// </summary>
        static RibbonTabControl()
        {
            var type = typeof(RibbonTabControl);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(typeof(RibbonTabControl)));
            IsTabStopProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(BooleanBoxes.FalseBox));
            KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
            ContextMenuService.Attach(type);
            PopupService.Attach(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonTabControl"/> class.
        /// </summary>
        public RibbonTabControl()
        {
            ContextMenuService.Coerce(this);

            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the System.Windows.FrameworkElement.Initialized event.
        /// This method is invoked whenever System.Windows.
        /// FrameworkElement.IsInitialized is set to true internally.
        /// </summary>
        /// <param name="e">The System.Windows.RoutedEventArgs that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.ItemContainerGenerator.StatusChanged += this.OnGeneratorStatusChanged;
        }

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>The element that is used to display the given item.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new RibbonTabItem();
        }

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is (or is eligible to be) its own container; otherwise, false.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is RibbonTabItem;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or
        /// internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            this.SelectedContentPresenter = this.Template.FindName("PART_SelectedContentPresenter", this) as ContentPresenter;

            this.DropDownPopup = this.Template.FindName("PART_Popup", this) as Popup;

            if (this.DropDownPopup != null)
            {
                this.DropDownPopup.CustomPopupPlacementCallback = this.CustomPopupPlacementMethod;
            }

            if (this.ToolbarPanel != null
                && this.toolBarItems != null)
            {
                for (var i = 0; i < this.toolBarItems.Count; i++)
                {
                    this.ToolbarPanel.Children.Remove(this.toolBarItems[i]);
                }
            }

            this.ToolbarPanel = this.Template.FindName("PART_ToolbarPanel", this) as Panel;

            if (this.ToolbarPanel != null
                && this.toolBarItems != null)
            {
                for (var i = 0; i < this.toolBarItems.Count; i++)
                {
                    this.ToolbarPanel.Children.Add(this.toolBarItems[i]);
                }
            }
        }

        /// <summary>
        /// Updates the current selection when an item in the System.Windows.Controls.Primitives.Selector has changed
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (this.IsMinimized
                && this.IsDropDownOpen == false)
            {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Remove
                && this.SelectedIndex == -1)
            {
                var startIndex = e.OldStartingIndex + 1;
                if (startIndex > this.Items.Count)
                {
                    startIndex = 0;
                }

                var item = this.FindNextTabItem(startIndex, -1);
                if (item != null)
                {
                    item.IsSelected = true;
                }
                else
                {
                    this.SelectedContent = null;
                }
            }
        }

        /// <summary>
        /// Called when the selection changes.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.UpdateSelectedContent();

            if (this.IsKeyboardFocusWithin
                && this.IsMinimized == false)
            {
                // If keyboard focus is within the control, make sure it is going to the correct place
                var item = this.GetSelectedTabItem();
                item?.SetFocus();
            }

            if (e.AddedItems.Count > 0)
            {
                if (this.IsMinimized)
                {
                    this.IsDropDownOpen = true;

                    ((RibbonTabItem)e.AddedItems[0]).IsHitTestVisible = false;
                }
            }
            else
            {
                if (this.IsDropDownOpen)
                {
                    this.IsDropDownOpen = false;
                }
            }

            if (e.RemovedItems.Count > 0)
            {
                ((RibbonTabItem)e.RemovedItems[0]).IsHitTestVisible = true;
            }

            base.OnSelectionChanged(e);
        }

        /// <summary>
        /// Invoked when an unhandled System.Windows.Input.Mouse.PreviewMouseWheel
        /// attached event reaches an element in its route that is derived from this class.
        /// Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The System.Windows.Input.MouseWheelEventArgs that contains the event data.</param>
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            //base.OnPreviewMouseWheel(e);

            if (this.IsMouseWheelScrollingEnabled)
            {
                this.ProcessMouseWheel(e);
            }
        }

        /// <summary>
        /// Invoked when the <see cref="E:System.Windows.UIElement.KeyDown"/> event is received.
        /// </summary>
        /// <param name="e">Information about the event.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.OnKeyUp(e);

            if (e.Handled)
            {
                return;
            }

            // Handle [Ctrl][Shift]Tab, Home and End cases
            // We have special handling here because if focus is inside the TabItem content we cannot
            // cycle through TabItem because the content is not part of the TabItem visual tree
            var direction = 0;
            var startIndex = -1;

            switch (e.Key)
            {
                case Key.Escape:
                    if (this.IsDropDownOpen)
                    {
                        this.IsDropDownOpen = false;
                    }

                    break;

                case Key.Tab:
                    if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    {
                        startIndex = this.ItemContainerGenerator.IndexFromContainer(this.ItemContainerGenerator.ContainerFromItem(this.SelectedItem));
                        if ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                        {
                            direction = -1;
                        }
                        else
                        {
                            direction = 1;
                        }
                    }

                    break;
                case Key.Home:
                    direction = 1;
                    startIndex = -1;
                    break;
                case Key.End:
                    direction = -1;
                    startIndex = this.Items.Count;
                    break;
            }

            var nextTabItem = this.FindNextTabItem(startIndex, direction);

            if (nextTabItem != null
                && ReferenceEquals(nextTabItem, this.SelectedItem) == false)
            {
                e.Handled = nextTabItem.SetFocus();
            }

            if (e.Handled == false)
            {
                base.OnKeyDown(e);
            }
        }

        #endregion

        #region Private methods

        private static bool IsRibbonAncestorOf(DependencyObject element)
        {
            while (element != null)
            {
                if (element is Ribbon)
                {
                    return true;
                }

                var parent = LogicalTreeHelper.GetParent(element) ?? VisualTreeHelper.GetParent(element);

                element = parent;
            }

            return false;
        }

        // Process mouse wheel event
        internal void ProcessMouseWheel(MouseWheelEventArgs e)
        {
            if (this.IsMinimized
                || this.SelectedItem == null)
            {
                return;
            }

            var focusedElement = Keyboard.FocusedElement as DependencyObject;

            if (focusedElement != null
                && IsRibbonAncestorOf(focusedElement))
            {
                return;
            }

            var visualItems = new List<RibbonTabItem>();
            var selectedIndex = -1;

#if NET45 || NET462
            var tabs = this.ItemContainerGenerator.Items.OfType<RibbonTabItem>()
                .Where(x => x.Visibility == Visibility.Visible && x.IsEnabled && (x.IsContextual == false || (x.IsContextual && x.Group.Visibility == Visibility.Visible)))
                .OrderBy(x => x.IsContextual)
                .ToList();
#else
            var tabs = this.Items.OfType<object>().Select(x => this.ItemContainerGenerator.ContainerFromItem(x)).OfType<RibbonTabItem>()
                .Where(x => x.Visibility == Visibility.Visible && x.IsEnabled && (x.IsContextual == false || (x.IsContextual && x.Group.Visibility == Visibility.Visible)))
                .OrderBy(x => x.IsContextual)
                .ToList();
#endif

            for (var i = 0; i < tabs.Count; i++)
            {
                var ribbonTabItem = tabs[i];
                visualItems.Add(ribbonTabItem);

                if (ribbonTabItem.IsSelected)
                {
                    selectedIndex = visualItems.Count - 1;
                }
            }

            // Try to ensure that we have a selection
            if (selectedIndex < 0)
            {
                if (visualItems.Count > 0)
                {
                    visualItems[0].IsSelected = true;
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    if (selectedIndex > 0)
                    {
                        visualItems[selectedIndex].IsSelected = false;
                        selectedIndex--;
                        visualItems[selectedIndex].IsSelected = true;
                    }
                }
                else if (e.Delta < 0)
                {
                    if (selectedIndex < visualItems.Count - 1)
                    {
                        visualItems[selectedIndex].IsSelected = false;
                        selectedIndex++;
                        visualItems[selectedIndex].IsSelected = true;
                    }
                }
            }

            e.Handled = true;
        }

        // Get selected ribbon tab item
        private RibbonTabItem GetSelectedTabItem()
        {
            var selectedItem = this.SelectedItem;
            if (selectedItem == null)
            {
                return null;
            }

            var item = selectedItem as RibbonTabItem
                ?? this.ItemContainerGenerator.ContainerFromIndex(this.SelectedIndex) as RibbonTabItem;

            return item;
        }

        // Find next tab item
        private RibbonTabItem FindNextTabItem(int startIndex, int direction)
        {
            if (direction != 0)
            {
                var index = startIndex;
                for (var i = 0; i < this.Items.Count; i++)
                {
                    index += direction;

                    if (index >= this.Items.Count)
                    {
                        index = 0;
                    }
                    else if (index < 0)
                    {
                        index = this.Items.Count - 1;
                    }

                    var nextItem = this.ItemContainerGenerator.ContainerFromIndex(index) as RibbonTabItem;
                    if ((nextItem != null) && nextItem.IsEnabled && (nextItem.Visibility == Visibility.Visible))
                    {
                        return nextItem;
                    }
                }
            }

            return null;
        }

        // Updates selected content
        private void UpdateSelectedContent()
        {
            if (this.SelectedIndex < 0)
            {
                this.SelectedContent = null;
                this.SelectedTabItem = null;
            }
            else
            {
                var selectedTabItem = this.GetSelectedTabItem();
                if (selectedTabItem != null)
                {
                    this.SelectedContent = selectedTabItem.GroupsContainer;
                    this.SelectedTabItem = selectedTabItem;
                }
            }
        }

        #endregion

        #region Event handling

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        // Handles GeneratorStatus changed
        private void OnGeneratorStatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.UpdateSelectedContent();
            }
        }

        // Handles IsMinimized changed
        private static void OnMinimizedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tab = (RibbonTabControl)d;

            if (!tab.IsMinimized)
            {
                tab.IsDropDownOpen = false;
            }

            if ((bool)e.NewValue == false
                && tab.SelectedIndex < 0)
            {
                var item = tab.FindNextTabItem(-1, 1);

                if (item != null)
                {
                    item.IsSelected = true;
                }
            }
        }

        // Handles ribbon popup closing
        private void OnRibbonTabPopupClosing()
        {
            var ribbonTabItem = this.SelectedItem as RibbonTabItem;

            if (ribbonTabItem != null)
            {
                ribbonTabItem.IsHitTestVisible = true;
            }

            if (ReferenceEquals(Mouse.Captured, this))
            {
                Mouse.Capture(null);
            }
        }

        // handles ribbon popup opening
        private void OnRibbonTabPopupOpening()
        {
            var ribbonTabItem = this.SelectedItem as RibbonTabItem;

            if (ribbonTabItem != null)
            {
                ribbonTabItem.IsHitTestVisible = false;
            }

            Mouse.Capture(this, CaptureMode.SubTree);
        }

        /// <summary>
        /// Implements custom placement for ribbon popup
        /// </summary>
        private CustomPopupPlacement[] CustomPopupPlacementMethod(Size popupsize, Size targetsize, Point offset)
        {
            if (this.DropDownPopup == null
                || this.SelectedTabItem == null)
            {
                return null;
            }

            // Get current workarea
            var tabItemPos = this.SelectedTabItem.PointToScreen(new Point(0, 0));
#pragma warning disable 618
            var tabItemRect = new RECT
            {
                Left = (int)tabItemPos.X,
                Top = (int)tabItemPos.Y,
                Right = (int)tabItemPos.X + (int)this.SelectedTabItem.ActualWidth,
                Bottom = (int)tabItemPos.Y + (int)this.SelectedTabItem.ActualHeight
            };
#pragma warning restore 618

#pragma warning disable 618
            var monitor = NativeMethods.MonitorFromRect(ref tabItemRect, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            if (monitor == IntPtr.Zero)
            {
                return null;
            }

            var monitorInfo = NativeMethods.GetMonitorInfo(monitor);
#pragma warning restore 618
            var startPoint = this.PointToScreen(new Point(0, 0));
            if (this.FlowDirection == FlowDirection.RightToLeft)
            {
                startPoint.X -= this.ActualWidth;
            }

            var inWindowRibbonWidth = monitorInfo.rcWork.Right - Math.Max(monitorInfo.rcWork.Left, startPoint.X);

            var actualWidth = this.ActualWidth;
            if (startPoint.X < monitorInfo.rcWork.Left)
            {
                actualWidth -= monitorInfo.rcWork.Left - startPoint.X;
                startPoint.X = monitorInfo.rcWork.Left;
            }

            // Set width and prevent negative values
            this.DropDownPopup.Width = Math.Max(0, Math.Min(actualWidth, inWindowRibbonWidth));

            return new[]
            {
                new CustomPopupPlacement(new Point(startPoint.X - tabItemPos.X + offset.X, targetsize.Height + offset.Y), PopupPrimaryAxis.Vertical),
                new CustomPopupPlacement(new Point(startPoint.X - tabItemPos.X + offset.X, -1 * (targetsize.Height + offset.Y + ((ScrollViewer)this.SelectedContent).ActualHeight)), PopupPrimaryAxis.Vertical)
            };
        }

        // Handles IsDropDownOpen property changed
        private static void OnIsDropDownOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ribbonTabControl = (RibbonTabControl)d;

            ribbonTabControl.RaiseRequestBackstageClose();

            if (ribbonTabControl.IsDropDownOpen)
            {
                ribbonTabControl.OnRibbonTabPopupOpening();
            }
            else
            {
                ribbonTabControl.OnRibbonTabPopupClosing();
            }
        }

        /// <summary>
        /// Raises an event causing the Backstage-View to be closed
        /// </summary>
        public void RaiseRequestBackstageClose()
        {
            this.RequestBackstageClose?.Invoke(this, null);
        }

        #endregion

        /// <summary>
        /// Gets the first visible item
        /// </summary>
        public object GetFirstVisibleAndEnabledItem()
        {
            foreach (var item in this.Items)
            {
                var ribbonTab = this.ItemContainerGenerator.ContainerFromItem(item) as RibbonTabItem;

                if (ribbonTab != null
                    && ribbonTab.Visibility == Visibility.Visible
                    && ribbonTab.IsEnabled)
                {
                    return ribbonTab;
                }
            }

            return null;
        }
    }
}