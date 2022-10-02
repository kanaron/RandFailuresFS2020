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
using System.Windows.Shapes;

namespace RandFailuresFS2020_WPF.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public event EventHandler OverviewClick;
        public event EventHandler SettingsClick;
        public event EventHandler PresetsClick;
        public event EventHandler FailListClick;
        public event EventHandler HelpClick;

        public ShellView()
        {
            InitializeComponent();
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            OverviewClick?.Invoke(this, e);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsClick?.Invoke(this, e);
        }

        private void Presets_Click(object sender, RoutedEventArgs e)
        {
            PresetsClick?.Invoke(this, e);
        }

        private void Fail_list_Click(object sender, RoutedEventArgs e)
        {
            FailListClick?.Invoke(this, e);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpClick?.Invoke(this, e);
        }
    }
}
