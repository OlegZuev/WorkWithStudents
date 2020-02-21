using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Serialization;
using Students;
using WPFStudentInteraction.Model;

namespace WPFStudentInteraction.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        private ObservableCollection<Student> _students = new ObservableCollection<Student>();

        private Student _currentStudent;

        public Student CurrentStudent {
            get => _currentStudent;
            set {
                _currentStudent = value;
                OnPropertyChanged(nameof(CurrentStudent));
            }
        }

        public MainWindowViewModel() {
            AddStudentCommand = new DelegateCommand(o => {
                Interaction.AddStudent(_students, out Student result);
                CurrentStudent = result;
            });

            OpenStudentListCommand = new DelegateCommand(o => {
                Interaction.OpenStudentList(ref _students);
                CurrentStudent = _students.FirstOrDefault();
            });

            SaveStudentListCommand = new DelegateCommand(o => {
                Interaction.SaveStudentList(_students);
            });
        }

        public ICommand AddStudentCommand { get; }

        public ICommand RemoveStudentCommand { get; }

        public ICommand NextStudentCommand { get; }

        public ICommand PrevStudentCommand { get; }

        public ICommand OpenStudentListCommand { get; }

        public ICommand SaveStudentListCommand { get; }

        public ICommand CreateStudentListCommand { get; }
    }
}