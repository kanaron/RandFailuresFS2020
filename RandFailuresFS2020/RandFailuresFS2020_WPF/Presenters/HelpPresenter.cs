using RandFailuresFS2020_WPF.Models;
using RandFailuresFS2020_WPF.Views;

namespace RandFailuresFS2020_WPF.Presenters
{
    public class HelpPresenter
    {
        public HelpView HelpView { private set; get; }
        public HelpModel HelpModel { private set; get; }

        public HelpPresenter()
        {
            HelpView = new HelpView();

            HelpModel = new HelpModel();

            HelpView.DataContext = HelpModel;
        }
    }
}
