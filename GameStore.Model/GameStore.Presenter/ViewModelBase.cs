using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameStore.Presenter
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, возникающее при изменении значения свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Уведомляет подписчиков об изменении свойства
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод для установки значения свойства (тот самый SetProperty)
        /// </summary>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Псевдоним для совместимости с твоим кодом GameDto
        /// </summary>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            return SetProperty(ref field, value, propertyName);
        }
    }
}