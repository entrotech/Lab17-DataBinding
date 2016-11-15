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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ElementBindingButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ElementBindingWindow();
            win.Show();
        }

        private void BruteForceButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new BruteForceWindow();
            win.Owner = this;
            string msg = win.ShowDialog() == true
                ? "Changes Saved" : "Changes Cancelled";
            MessageBox.Show(msg, "Result");
        }

        private void SimpleBindingButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new SimpleBindingWindow();
            win.Owner = this;
            string msg = win.ShowDialog() == true
                ? "Changes Saved" : "Changes Cancelled";
            MessageBox.Show(msg, "Result");
        }

        private void ComplexBindingButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ComplexBindingWindow();
            win.Owner = this;
            win.Show();
        }
    }
}