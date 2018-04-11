// ReSharper disable once CheckNamespace
namespace Fluent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Fluent.Internal;

    /// <summary>
    /// Represents contextual groups container
    /// </summary>
    public class RibbonContextualGroupsContainer : Panel
    {
        private readonly List<Size> sizes = new List<Size>();

        /// <summary>
        /// When overridden in a derived class, positions child elements and determines a size for
        /// a System.Windows.FrameworkElement derived class.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should
        /// use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var finalRect = new Rect(finalSize);
            var index = 0;

            foreach (UIElement item in this.InternalChildren)
            {
                finalRect.Width = this.sizes[index].Width; //item.DesiredSize.Width;
                finalRect.Height = Math.Max(finalSize.Height, this.sizes[index].Height); //Math.Max(finalSize.Height, item.DesiredSize.Height);
                item.Arrange(finalRect);
                finalRect.X += this.sizes[index].Width; // item.DesiredSize.Width;
                index++;
            }

            return finalSize;
        }

        /// <summary>
        /// When overridden in a derived class, measures the size in layout required for
        /// child elements and determines a size for the System.Windows.FrameworkElement-derived class.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements.
        /// Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
        /// <returns>The size that this element determines it needs during layout, based on its calculations of child element sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var allGroupsWidth = 0D;
            this.sizes.Clear();
            var infinity = new Size(double.PositiveInfinity, double.PositiveInfinity);

            var availableSizeHeight = availableSize.Height;

            if (double.IsPositiveInfinity(availableSizeHeight))
            {
                availableSizeHeight = 0;
            }

            foreach (RibbonContextualTabGroup contextualGroup in this.InternalChildren)
            {
                // Calculate width of tab items of the group
                var tabsWidth = 0D;

                // We have to look at visible and items which already got measured only
                var visibleItems = contextualGroup.Items.Where(item => item.Visibility == Visibility.Visible && DoubleUtil.AreClose(item.DesiredSize.Width, 0) == false).ToList();

                foreach (var item in visibleItems)
                {
                    tabsWidth += item.DesiredSize.Width;
                }

                contextualGroup.Measure(infinity);
                var groupWidth = contextualGroup.DesiredSize.Width;

                var tabWasChanged = false;

                if (groupWidth > tabsWidth)
                {
                    // If tab's width is less than group's width we have to stretch tabs
                    var delta = (groupWidth - tabsWidth) / visibleItems.Count;

                    foreach (var item in visibleItems)
                    {
                        var newDesiredWidth = item.DesiredSize.Width + delta;

                        // Update cached DesiredWidth
                        if (DoubleUtil.AreClose(newDesiredWidth, item.DesiredWidth) == false)
                        {
                            item.DesiredWidth = newDesiredWidth;
                            item.Measure(new Size(item.DesiredWidth, item.DesiredSize.Height));
                            tabWasChanged = true;
                        }
                    }
                }

                if (tabWasChanged)
                {
                    // If we have changed tabs layout we have
                    // to invalidate down to RibbonTabsContainer
                    var visual = visibleItems[0] as Visual;

                    while (visual != null)
                    {
                        var uiElement = visual as UIElement;
                        if (uiElement != null)
                        {
                            if (uiElement is RibbonTabsContainer)
                            {
                                uiElement.InvalidateMeasure();
                                break;
                            }

                            uiElement.InvalidateMeasure();
                        }

                        visual = VisualTreeHelper.GetParent(visual) as Visual;
                    }

                    tabsWidth = 0;

                    foreach (var item in visibleItems)
                    {
                        tabsWidth += item.DesiredSize.Width;
                    }
                }

                // Calc final width and measure the group using it
                var finalWidth = tabsWidth;
                allGroupsWidth += finalWidth;

                if (allGroupsWidth > availableSize.Width)
                {
                    finalWidth -= allGroupsWidth - availableSize.Width;
                    allGroupsWidth = availableSize.Width;
                }

                contextualGroup.Measure(new Size(Math.Max(0, finalWidth), availableSizeHeight));
                this.sizes.Add(new Size(Math.Max(0, finalWidth), availableSizeHeight));
            }

            return new Size(allGroupsWidth, availableSizeHeight);
        }
    }
}