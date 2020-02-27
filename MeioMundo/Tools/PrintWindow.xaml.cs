﻿using System.Windows;
using System.Windows.Documents;

namespace Tools
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        private FixedDocumentSequence _document;
        public PrintWindow(FixedDocumentSequence document)
        {
            _document = document;
            InitializeComponent();
            PreviewD.Document = document;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //print directly from the Xps file 
        }
    }
}
