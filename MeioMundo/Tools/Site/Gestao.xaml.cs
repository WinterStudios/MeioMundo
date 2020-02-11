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

namespace Tools.Site
{
    /// <summary>
    /// Interaction logic for Gestao.xaml
    /// </summary>
    public partial class Gestao : UserControl
    {
        public static bool ShowMenu { get { return true; } }
        public static string Header { get { return "Gestor"; } }
        public Gestao()
        {
            InitializeComponent();
        }
    }
}
