using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeioMundo.Components
{
    public partial class TabSystem : UserControl
    {
        public Form1 FormParent { get; set; }
        public TabSystem()
        {
            InitializeComponent();
            
            

        }
        public void SetComponets()
        {
            TabsPages.Width = ClientSize.Width;
            
            Pages.Width = Size.Width;
            Pages.Height = Size.Height - TabsPages.Height;
            
            Console.WriteLine(FormParent.ClientSize.Height);
        }
    }
}
