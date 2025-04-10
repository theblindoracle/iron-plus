using System;
using System.Collections.Generic;
using IronPlus.Models;
using IronPlus.ViewModels;
using Newtonsoft.Json;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.Views
{
    [QueryProperty(nameof(Barbell), "barbell")]
    public partial class AddBarbellDetailsPage : BasePage
    {
        public AddBarbellDetailsPage()
        {
            InitializeComponent();
        }

        public string Barbell
        {
            set
            {
                var vm = (AddBarbellDetailsViewModel)BindingContext;
                if (vm != null)
                {
                    vm.Barbell = JsonConvert.DeserializeObject<Barbell>(Uri.UnescapeDataString(value));
                    vm.Title = "Edit Barbell";
                }
            }
        }
    }
}