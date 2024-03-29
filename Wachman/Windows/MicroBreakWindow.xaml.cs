﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wachman.Utils;

namespace Wachman.Windows
{
    /// <summary>
    /// Interaction logic for MicroBreakWindow.xaml
    /// </summary>
    public partial class MicroBreakWindow : Window
    {
        public MicroBreakWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AeroGlassHelper.EnableBlur(this, true);
        }
    }
}
