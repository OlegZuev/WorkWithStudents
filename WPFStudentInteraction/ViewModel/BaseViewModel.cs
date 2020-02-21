using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFStudentInteraction.Annotations;

namespace WPFStudentInteraction.ViewModel {
    public class BaseViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}