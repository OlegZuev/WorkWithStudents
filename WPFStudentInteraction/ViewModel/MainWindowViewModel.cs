using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Students;
using WPFStudentInteraction.Model;
using WPFStudentInteraction.View;

namespace WPFStudentInteraction.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        private CreationStudentWindow _creationStudentWindow;
        private ObservableCollection<Student> _students;

        private Student _currentStudent;

        private int _currentStudentIndex;

        private ObservableCollection<PropertyInfo> _searchAttributes;

        public ObservableCollection<PropertyInfo> SearchAttributes => _searchAttributes ??= new ObservableCollection<PropertyInfo>();

        private PropertyInfo _selectedSearchAttribute;

        public PropertyInfo SelectedSearchAttribute {
            get => _selectedSearchAttribute;
            set {
                _selectedSearchAttribute = value;
                QueryText = string.Empty; // ???
                OnPropertyChanged(nameof(SelectedSearchAttribute));
            }
        }

        private string _queryText = string.Empty;

        public string QueryText {
            get => _queryText;
            set {
                if (_queryText == value) {
                    return;
                }

                _queryText = value;
                CurrentStudent = Interaction.GetFirstStudent(_students, SelectedSearchAttribute, QueryText);
                OnPropertyChanged(nameof(QueryText));
            }
        }

        public Student CurrentStudent {
            get => _currentStudent;
            set {
                if (_currentStudent == value) {
                    return;
                }

                _currentStudent = value;
                _currentStudentIndex = _students.IndexOf(_currentStudent);
                Interaction.AddMissingProperties(_currentStudent, SearchAttributes);

                OnPropertyChanged(nameof(Diploma));
                OnPropertyChanged(nameof(CurrentStudent));
            }
        }

        private bool _hasNextStudent;

        public bool HasNextStudent {
            get => _hasNextStudent;
            set {
                _hasNextStudent = value;
                OnPropertyChanged(nameof(HasNextStudent));
            }
        }

        private bool _hasPrevStudent;

        public bool HasPrevStudent {
            get => _hasPrevStudent;
            set {
                _hasPrevStudent = value;
                OnPropertyChanged(nameof(HasPrevStudent));
            }
        }

        public string Diploma {
            get {
                PropertyInfo property = _currentStudent?.GetType().GetProperties().FirstOrDefault(p => p.Name == nameof(Diploma));
                return property?.GetValue(_currentStudent) as string;
            }
            set {
                PropertyInfo property = _currentStudent?.GetType().GetProperties().FirstOrDefault(p => p.Name == nameof(Diploma));
                property?.SetValue(_currentStudent, value);
                OnPropertyChanged(nameof(Diploma));
            }
        }

        public MainWindowViewModel() {
            AddStudentCommand = new DelegateCommand(o => {
                Interaction.AddStudent(_students, out Student result, ref _creationStudentWindow);
                if (result == null) return;

                CurrentStudent = result;
                _queryText = string.Empty;
                OnPropertyChanged(QueryText);
            }, o => _students != null);

            RemoveStudentCommand = new DelegateCommand(o => {
                Interaction.RemoveStudent(_students, _currentStudent, SelectedSearchAttribute, QueryText,
                                          out Student result);
                Interaction.RemoveRedundantProperties(_students, _currentStudent, SearchAttributes);
                CurrentStudent = result;
            }, o => _students?.Count > 0 && _currentStudent != null);

            NextStudentCommand = new DelegateCommand(o => {
                Interaction.NextStudent(_students, _currentStudent, SelectedSearchAttribute, QueryText,
                                        out Student result);
                CurrentStudent = result;
            }, o => {
                int result =
                    Interaction.GetNextStudentIndex(_students, _currentStudentIndex, SelectedSearchAttribute,
                                                    QueryText);
                HasNextStudent = result != -1 && result < _students.Count;
                return HasNextStudent;
            });

            PrevStudentCommand = new DelegateCommand(o => {
                Interaction.PrevStudent(_students, _currentStudent, SelectedSearchAttribute, QueryText,
                                        out Student result);
                CurrentStudent = result;
            }, o => {
                HasPrevStudent =
                    Interaction.GetPrevStudentIndex(_students, _currentStudentIndex, SelectedSearchAttribute,
                                                    QueryText) != -1;
                return HasPrevStudent;
            });

            OpenStudentListCommand = new DelegateCommand(o => {
                Interaction.OpenStudentList(out _students);
                if (_students == null) return;

                SearchAttributes.Clear();
                _students?.ToList().ForEach(student => Interaction.AddMissingProperties(student, SearchAttributes));
                CurrentStudent = _students?.FirstOrDefault();
            });

            SaveStudentListCommand = new DelegateCommand(o => {
                Interaction.SaveStudentList(_students);
            }, o => _students != null && _students.Any());

            CreateStudentListCommand = new DelegateCommand(o => {
                Interaction.CreateStudentList(out _students);
                CurrentStudent = null;
                SearchAttributes.Clear();
            });

            GetNewStudentTypeCommand = new DelegateCommand(content => {
                CreationStudentModel.GetNewStudentType(content as string, _creationStudentWindow);
            });

            PromoteToMasterStudentCommand = new DelegateCommand(o => {
                Interaction.PromoteToMasterStudent(_currentStudent, out Student promotedStudent);
                _students[_currentStudentIndex] = promotedStudent;
                CurrentStudent = promotedStudent;
            }, o => _currentStudent != null);
        }

        public ICommand AddStudentCommand { get; }

        public ICommand RemoveStudentCommand { get; }

        public ICommand NextStudentCommand { get; }

        public ICommand PrevStudentCommand { get; }

        public ICommand OpenStudentListCommand { get; }

        public ICommand SaveStudentListCommand { get; }

        public ICommand CreateStudentListCommand { get; }

        public ICommand GetNewStudentTypeCommand { get; }

        public ICommand PromoteToMasterStudentCommand { get; }
    }
}