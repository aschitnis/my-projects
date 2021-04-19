using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für WindowGridSplitter.xaml
    /// </summary>
    public partial class WindowGridSplitter : Window
    {
        public WindowGridSplitterVM windowGridSplitterVM = new WindowGridSplitterVM();
        public WindowGridSplitter()
        {
            InitializeComponent();

            DataContext = windowGridSplitterVM;
        }

        private void ChangeForegroundforItemInComboBox(object obj)
        {
            DependencyObject dObject = obj as DependencyObject;
            if (dObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dObject); i++)
                {
                    if  ( dObject.GetType() == typeof(Border) )
                    {
                        break;
                    }
                    ChangeForegroundforItemInComboBox(dObject);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Loaded, (ThreadStart)delegate ()
            {
                DependencyObject dataTemplateMainGrid = FindVisualChild<Grid>(cbCustomers, "gdMain");
                if (dataTemplateMainGrid != null)
                {
                    DependencyObject tbIdsText = FindVisualChild<TextBlock>(dataTemplateMainGrid, "tbDisplayedtext");
                    if (tbIdsText != null)
                        (tbIdsText as TextBlock).Foreground = Brushes.Blue;
                }
            });
        }

        private DependencyObject FindVisualChild<T>(DependencyObject obj, string name)
        {
           //  Console.WriteLine(((FrameworkElement)obj).Name);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                FrameworkElement fe = child as FrameworkElement;
                //not a framework element or is null
                if (fe == null) return null;
                if (!string.IsNullOrEmpty(fe.Name))
                { 
                    // Console.WriteLine(fe.Name);
                }
                if (child is T && fe.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return child;
                else
                {
                    //Not found it - search children
                    DependencyObject nextLevel = FindVisualChild<T>(child, name);
                    if (nextLevel != null)
                        return nextLevel;
                }
            }
            return null;
        }
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // If the child is not of the request child type child
                T childType = child as T;

                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DependencyObject dataTemplateMainGrid = FindVisualChild<Grid>(cbCustomers, "gdMain");
            if (dataTemplateMainGrid != null)
            {
                DependencyObject tbIdsText = FindVisualChild<TextBlock>(dataTemplateMainGrid, "tbDisplayedtext");
                if (tbIdsText != null)
                    (tbIdsText as TextBlock).Foreground = Brushes.Blue;
            }

            //Dispatcher.Invoke(DispatcherPriority.Loaded, (ThreadStart)delegate ()
            //{
            //    DependencyObject dataTemplateMainGrid = FindVisualChild<Grid>(cbCustomers, "gdMain");
            //    if (dataTemplateMainGrid != null)
            //    {
            //        DependencyObject tbIdsText = FindVisualChild<TextBlock>(dataTemplateMainGrid, "tbDisplayedtext");
            //        if (tbIdsText != null)
            //            (tbIdsText as TextBlock).Foreground = Brushes.Blue;
            //    }
            //});
        }
    }
}
