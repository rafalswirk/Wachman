using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FocusForge.Activities.UI.Views
{
    /// <summary>
    /// Interaction logic for ActivitiesView.xaml
    /// </summary>
    public partial class ActivitiesView : UserControl
    {
        public ActivitiesView()
        {
            InitializeComponent();
        }

        private void FlatActivitiesGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
            {
                dataGrid.SelectedItem = null;
            }
        }

        private void ActivitiesTree_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender is TreeView treeView && e.NewValue != null)
            {
                var selectedContainer = FindTreeViewItem(treeView, e.NewValue);
                if (selectedContainer != null)
                {
                    selectedContainer.IsSelected = false;
                }
            }
        }

        private static TreeViewItem? FindTreeViewItem(ItemsControl? parent, object? item)
        {
            if (parent == null || item == null)
            {
                return null;
            }

            var directContainer = parent.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
            if (directContainer != null)
            {
                return directContainer;
            }

            foreach (var child in parent.Items)
            {
                var childContainer = parent.ItemContainerGenerator.ContainerFromItem(child) as TreeViewItem;
                if (childContainer == null)
                {
                    continue;
                }

                var nestedMatch = FindTreeViewItem(childContainer, item);
                if (nestedMatch != null)
                {
                    return nestedMatch;
                }
            }

            return null;
        }
    }
}
