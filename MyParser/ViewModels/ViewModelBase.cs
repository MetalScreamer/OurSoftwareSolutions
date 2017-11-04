using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Oss.Windows.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(T value, ref T storage, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(value, storage))
            {
                storage = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
