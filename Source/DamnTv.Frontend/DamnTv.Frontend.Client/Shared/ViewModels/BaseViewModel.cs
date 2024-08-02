using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DamnTv.Frontend.Client.Shared.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChangedEventArgs args = new(propertyName);
            PropertyChanged?.Invoke(this, args);
        }

        protected void SetProperty<T>(
            ref T filed, in T value,
            [CallerMemberName] string? propertyName = null
        )
        {
            if (EqualityComparer<T>.Default.Equals(filed, value))
                return;

            filed = value;
            OnPropertyChanged(propertyName);
        }
    }
}
