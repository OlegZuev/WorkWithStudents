using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Students;
using WPFStudentInteraction.Model;

namespace WPFStudentInteraction.ViewModel {
    public class MainWindowViewModel : BaseViewModel {
        private ObservableCollection<Student> _students;

        private Student _currentStudent;

        private int _currentStudentIndex;

        private List<PropertyInfo> _searchAttributes;

        public List<PropertyInfo> SearchAttributes {
            get => _searchAttributes;
            set {
                _searchAttributes = value;
                OnPropertyChanged(nameof(SearchAttributes));
            }
        }

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
                SearchAttributes = _currentStudent != null
                    ? new List<PropertyInfo>(_currentStudent.GetType().GetProperties())
                    : null;

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

        public MainWindowViewModel() {
            AddStudentCommand = new DelegateCommand(o => {
                Interaction.AddStudent(_students, out Student result);
                CurrentStudent = result;
                _queryText = string.Empty;
                OnPropertyChanged(QueryText);
            }, o => _students != null);

            RemoveStudentCommand = new DelegateCommand(o => {
                Interaction.RemoveStudent(_students, _currentStudent, SelectedSearchAttribute, QueryText,
                                          out Student result);
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
                CurrentStudent = _students?.FirstOrDefault();
            });

            SaveStudentListCommand = new DelegateCommand(o => { Interaction.SaveStudentList(_students); });

            CreateStudentListCommand = new DelegateCommand(o => {
                Interaction.CreateStudentList(out _students);
                CurrentStudent = null;
                SearchAttributes = null;
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