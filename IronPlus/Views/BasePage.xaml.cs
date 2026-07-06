using IronPlus.Services;
using IronPlus.ViewModels;

namespace IronPlus.Views
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();
        }

        public IList<IView> BaseContent => BaseContentGrid.Children;

        protected override async void OnAppearing()
        {
            Analytics_TrackPageView();

            try
            {
                await (BindingContext as BaseViewModel).InitializeAsync();
            }
            catch (Exception ex)
            {
                AnalyticsService.Track_App_Exception(ex.Message);
            }
        }

        #region Analytics Methods

        protected string PageName => GetType().Name;

        /// <summary>
        /// Send a page view to analytics server using page class name
        /// </summary>
        protected virtual void Analytics_TrackPageView()
        {
            AnalyticsService.Track_App_Page(PageName);
        }

        #endregion
    }
}
