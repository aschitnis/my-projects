﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using wpf.websocket.client.classes;

// using WebSocketSharp;

namespace wpf.websocket.client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SocketViewModel SocketVM { get; set; } = new SocketViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = SocketVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SocketVM.SendToSocketServer();
           //string code = SocketVM.GetSelectedCurrencyCode();
        }
    }
}
