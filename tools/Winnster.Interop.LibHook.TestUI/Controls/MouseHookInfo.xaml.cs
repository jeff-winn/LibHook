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
using Winnster.Interop.LibHook.TestUI.ViewModels;

namespace Winnster.Interop.LibHook.TestUI.Controls
{
    /// <summary>
    /// Interaction logic for MouseHookInfoPane.xaml
    /// </summary>
    public partial class MouseHookInfoPane : UserControl
    {
        private MouseHookInfoViewModel vm;

        public MouseHookInfoPane()
        {
            this.InitializeComponent();
            this.vm = (MouseHookInfoViewModel)this.Resources["vm"];
        }
    }
}
