using System.Threading.Tasks;
using IronPlus.Interfaces;
using IronPlus.ViewModels.Base;

namespace IronPlus.ViewModels
{
    public class BaseViewModel : ExtendedBindableObject
    {
        public readonly IDialogService dialogService;
        public readonly ISettingsService settingsService;

        public BaseViewModel(IDialogService dialogService, ISettingsService settingsService)
        {
            this.dialogService = dialogService;
            this.settingsService = settingsService;
        }

        public string Title { get; set; }

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }


        public virtual Task InitializeAsync()
        {
            return Task.FromResult(false);
        }
    }
}
