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

namespace SamanthaDJ.Service.UserControl {

    /// <summary>
    /// Lógica de interacción para ConfigurationUC.xaml
    /// </summary>
    public partial class ConfigurationUC : System.Windows.Controls.UserControl {

        public event EventHandler event_UCClose;

        public ConfigurationUC() {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e) {
            event_UCClose(sender, e);
        }
    }
}
