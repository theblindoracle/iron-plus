using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IronPlus.Interfaces;
using IronPlus.Models;
using Newtonsoft.Json;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.ViewModels
{
    public class BarbellSettingsViewModel : BaseViewModel
    {
        IDatabaseService databaseService;
        public BarbellSettingsViewModel(IDialogService dialogService, ISettingsService settingsService, IDatabaseService databaseService) : base(dialogService, settingsService)
        {
            this.databaseService = databaseService;

            Title = "Barbell Settings";
        }

        public override async Task InitializeAsync()
        {
            using (dialogService.Loading())
            {
                var barbells = await databaseService.GetBarbellsAsync();
                Barbells = new ObservableCollection<Barbell>(barbells);
            }
        }

        ObservableCollection<Barbell> barbells;
        public ObservableCollection<Barbell> Barbells
        {
            get => barbells;
            set => SetProperty(ref barbells, value);
        }

        Command<Barbell> removeBarbellCommand;
        public Command<Barbell> RemoveBarbellCommand => removeBarbellCommand ??= new Command<Barbell>(async (barbell) =>
        {
            await databaseService.DeleteBarbellAsync(barbell);
            barbells.Remove(barbell);
        });

        Command<Barbell> editBarbellCommand;
        public Command<Barbell> EditBarbellCommand => editBarbellCommand ??= new Command<Barbell>(async (barbell) =>
        {
            var json = JsonConvert.SerializeObject(barbell);
            await Shell.Current.GoToAsync($"addBarbellDetails?barbell={json}");
        });

        Command addBarbellCommand;
        public Command AddBarbellCommand => addBarbellCommand ??= new Command(async () => await Shell.Current.GoToAsync("addBarbellDetails"));
    }
}
