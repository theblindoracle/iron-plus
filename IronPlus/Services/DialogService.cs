using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using IronPlus.Interfaces;

namespace IronPlus.Services
{
    public class DialogService : IDialogService
    {
        readonly IThemeService themeService;

        public DialogService(IThemeService themeService)
        {
            this.themeService = themeService;
        }

        static readonly IUserDialogs _instance = UserDialogs.Instance;

        public async Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? cancelToken = null)
            => await _instance.AlertAsync(message, title, okText, cancelToken);

        public async Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null)
            => await _instance.AlertAsync(config, cancelToken);

        public IDisposable Toast(string title, TimeSpan? dismissTimer = null)
            => _instance.Toast(title, dismissTimer);

        public IDisposable Toast(ToastConfig config)
        {
            // config.BackgroundColor = themeService.GetThemedResourceColor("DialogBackground", true);
            // config.MessageTextColor = themeService.GetThemedResourceColor("OnDialogBackground", true);
            return _instance.Toast(config);
        }

        public IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null)
            => _instance.Loading(title, onCancel, cancelText, show, maskType);
    }
}
