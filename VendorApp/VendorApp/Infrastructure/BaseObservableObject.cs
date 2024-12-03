using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VendorApp.Infrastructure
{
    public class BaseObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal void OnPropertyChanged([CallerMemberName] in string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BaseObservableObject RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
            return this;
        }

        protected virtual bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateValue = null)
        {
            //if value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            //if value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            onChanged?.Invoke();
            return true;
        }

        protected BaseObservableObject SetPropertyWith<T>(ref T storage, T value, Action onChanged = null, [CallerMemberName] string propertyName = null)
        {
            SetProperty(ref storage, value, propertyName, onChanged);
            return this;
        }

        public Guid ID { get; } = Guid.NewGuid();

        protected void UnSubscribeEvents()
        {
            if (PropertyChanged is not null)
            {
                Delegate[] clientList = PropertyChanged.GetInvocationList();
                foreach (var d in clientList)
                    PropertyChanged -= (d as PropertyChangedEventHandler);
            }
        }
    }
}

