using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace FocusForge.Activities.UI.Services
{
    public class RawActivitiesExportService : IRawActivitiesExportService
    {
        public void Export(string? content)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Save activities log",
                Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = ".txt",
                FileName = $"activities-{DateTime.Now:yyyyMMdd-HHmmss}.txt"
            };

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            try
            {
                File.WriteAllText(dialog.FileName, content ?? string.Empty, Encoding.UTF8);
                MessageBox.Show("Activities were saved successfully.", "Activities", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save activities file. {ex.Message}", "Activities", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
