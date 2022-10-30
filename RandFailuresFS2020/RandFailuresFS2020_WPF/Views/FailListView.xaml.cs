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

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for FailListView.xaml
    /// </summary>
    public partial class FailListView : UserControl
    {
        public event EventHandler ShowFailuresClicked;

        public FailListView()
        {
            InitializeComponent();
        }

        private void ShowFailures_Click(object sender, RoutedEventArgs e)
        {
            ShowFailuresClicked?.Invoke(this, e);
        }
    }
}
