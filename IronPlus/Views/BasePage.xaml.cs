using System;
using System.Collections.Generic;
using IronPlus.Services;
using IronPlus.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.Views
{
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();
        }

        public IList<View> BaseContent => BaseContentGrid.Children;

        protected override async void OnAppearing()
        {
            Analytics_TrackPageView();

            try
            {
                await (BindingContext as BaseViewModel).InitializeAsync();
            }
            catch (Exception ex)
            {
                AppCenterService.Track_App_Exception(ex.Message);
            }
        }

        #region Analytics Methods

        protected string PageName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Send a page view to analytics server using custom tag
        /// </summary>
        /// <param name="pageTag">Page tag.</param>
        protected void Analytics_TrackPageView(string pageTag)
        {
            AppCenterService.Track_App_Page(pageTag);
        }


        /// <summary>
        /// Send a page view to analytics server using page class name
        /// </summary>
        protected virtual void Analytics_TrackPageView()
        {
            AppCenterService.Track_App_Page(PageName);

        }

        #endregion
    }
}
