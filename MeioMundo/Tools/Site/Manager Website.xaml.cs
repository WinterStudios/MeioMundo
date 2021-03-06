﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tools.Site;

namespace Tools
{
    /// <summary>
    /// Interaction logic for Manager_Website.xaml
    /// </summary>
    public partial class Manager_Website : UserControl
    {
        public static bool ShowMenu { get { return true; } }
        public static string Header { get { return "Stock do site"; } }

        public List<Dados.Site> _produtosSite = new List<Dados.Site>();
        public List<Dados.Site> ProdutosSite { get { return _produtosSite; } set { _produtosSite = value; } }

        public List<Dados.Site> _produtosSage = new List<Dados.Site>();
        public List<Dados.Site> ProdutosSage { get { return _produtosSage; } set { _produtosSage = value; } }

        public Manager_Website()
        {
            InitializeComponent();
            table_Site.ItemsSource = ProdutosSite;
            table_Sage.ItemsSource = ProdutosSage;
        }


        private void ManagerProducts(object sender, RoutedEventArgs e)
        {

        }

        private void btn_OpenSite_Click(object sender, RoutedEventArgs e)
        {
            string path = FileManager.Window.OpenFileWindowDialog(FileManager.Window.Extensions.CSV);
            _produtosSite = FileManager.GetDados(path);
            table_Site.ItemsSource = _produtosSite;
            table_Site.Items.Refresh();
        }
        private void btn_OpenSage_Click(object sender, RoutedEventArgs e)
        {
            string path = FileManager.Window.OpenFileWindowDialog(FileManager.Window.Extensions.XLSX);

            _produtosSage = FileManager.GetDados(path);
            table_Sage.ItemsSource = _produtosSage;
            table_Sage.Items.Refresh();
        }
    }
}
