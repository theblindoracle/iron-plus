using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IronPlus.ViewModels.Base
{
    public abstract class ExtendedBindableObject : INotifyPropertyChanged
    {
        bool _isBatching = false;
        readonly HashSet<string> _pendingNotifications = new();

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void BeginBatch() => _isBatching = true;

        protected void EndBatch()
        {
            _isBatching = false;
            foreach (var name in _pendingNotifications)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            _pendingNotifications.Clear();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (_isBatching)
            {
                _pendingNotifications.Add(propertyName);
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
