using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace IronPlus.Interfaces
{
    public interface IDialogService
    {
        Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? cancelToken = null);
        Task AlertAsync(AlertConfig config, CancellationToken? cancelToken = null);
        IProgressDialog Loading(string title = null, Action onCancel = null, string cancelText = null, bool show = true, MaskType? maskType = null);
        IDisposable Toast(string title, TimeSpan? dismissTimer = null);
        IDisposable Toast(ToastConfig config);
    }
}