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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.Test.my.weather.Popups
{
    /// <summary>
    /// Interaktionslogik für InfoPopup.xaml
    /// </summary>
    public partial class InfoPopup : UserControl
    {
        public InfoPopup()
        {
            InitializeComponent();
        }

        private void bCloseInfoPopup_Click(object sender, RoutedEventArgs e)
        {
            infoPopup.IsOpen = false;
        }

        public void ShowInfoMessage(string message)
        {
            infoPopup.IsOpen = true;
        }
    }
}