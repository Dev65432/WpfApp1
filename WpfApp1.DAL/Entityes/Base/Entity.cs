using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace WpfApp1.DAL.Entityes.Base
{
    public abstract class Entity : IEntity, INotifyPropertyChanged
    {
         // public int Id { get; set; }

        private int _id;

        public int Id
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
