﻿using MeioMundo.Editor.API.Plugin;
using MeioMundo.Editor.Internal.Plugin;
using System;
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

namespace MeioMundo.Editor.UsersControls
{
    /// <summary>
    /// Interaction logic for PluginUControl.xaml
    /// </summary>
    public partial class PluginUControl : UserControl
    {
        public List<PluginInfo> infos = new List<PluginInfo>();
        public PluginUControl()
        {
            InitializeComponent();
            infos = PluginManager.Plugins;
            infos.Add(new PluginInfo() { AssemblyName = "MeioMundo.Editor.Test", Version = "0.0.0.0" });
            dataPlguin.ItemsSource = infos;
        }

        private void dataPlguin_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            PluginManager.SaveSetting();
        }

        private void dataPlguin_Error(object sender, ValidationErrorEventArgs e)
        {

        }
    }
}