using System.ComponentModel;

namespace ch22yaml.Data
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender,e) => { };

        public void NotifyPropertyChanged(string propertyName)=>
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
