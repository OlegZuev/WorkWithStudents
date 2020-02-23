using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using Students;
using WPFStudentInteraction.View;

namespace WPFStudentInteraction.Model {
    public static class Interaction {
        public static Student GetFirstStudent(IList<Student> students, PropertyInfo selectedSearchAttribute,
                                              string queryText) {
            Contract.Assert(students != null, "students != null");

            if (selectedSearchAttribute == null || string.IsNullOrEmpty(queryText)) {
                return students.FirstOrDefault();
            }

            return students.ToList().Find(s => FilterByPropertyValue(s, selectedSearchAttribute, queryText));
        }

        // ReSharper disable once RedundantAssignment
        public static void AddStudent(IList<Student> students, out Student currentStudent, ref CreationStudentWindow window) {
            Contract.Assert(students != null, "students != null");

            window = new CreationStudentWindow {Owner = Application.Current.MainWindow};
            if (window.ShowDialog() == true) {
                var newStudent = Activator.CreateInstance(window.Result) as Student;
                students.Add(newStudent);
                currentStudent = newStudent;
            } else {
                currentStudent = null;
            }

        }

        public static void RemoveStudent(IList<Student> students, Student currentStudent,
                                         PropertyInfo selectedSearchAttribute, string queryTex, out Student result) {
            Contract.Assert(students != null, "students != null");

            int current = students.IndexOf(currentStudent);
            int anotherStudentIndex;
            if (GetPrevStudentIndex(students, current, selectedSearchAttribute, queryTex) is int resultA &&
                resultA != -1) {
                anotherStudentIndex = resultA;
            } else if (GetNextStudentIndex(students, current, selectedSearchAttribute, queryTex) is int resultB &&
                       resultB != -1 && resultB < students.Count) {
                anotherStudentIndex = resultB - 1;
            } else {
                anotherStudentIndex = -1;
            }

            students.Remove(currentStudent);
            result = anotherStudentIndex == -1 ? null : students[anotherStudentIndex];
        }

        public static void NextStudent(IList<Student> students, Student currentStudent,
                                       PropertyInfo selectedSearchAttribute, string queryText, out Student result) {
            Contract.Assert(students != null, "students != null");

            int current = students.IndexOf(currentStudent);
            int nextStudentIndex = GetNextStudentIndex(students, current, selectedSearchAttribute, queryText);

            if (nextStudentIndex == students.Count || nextStudentIndex == -1) {
                throw new IndexOutOfRangeException(Properties.Resources.AboveRange);
            }

            result = students[nextStudentIndex];
        }

        public static int GetNextStudentIndex(IList<Student> students, int current, PropertyInfo attribute,
                                              string text) {
            if (students == null || current == -1 || current == students.Count + 1) {
                return -1;
            }

            if (attribute == null || string.IsNullOrEmpty(text)) {
                return current + 1;
            }

            return students.ToList().FindIndex(current + 1, s => FilterByPropertyValue(s, attribute, text));
        }

        public static void PrevStudent(IList<Student> students, Student currentStudent,
                                       PropertyInfo selectedSearchAttribute, string queryText, out Student result) {
            Contract.Assert(students != null, "students != null");

            int current = students.IndexOf(currentStudent);
            int prevStudentIndex = GetPrevStudentIndex(students, current, selectedSearchAttribute, queryText);

            if (prevStudentIndex == -1) {
                throw new IndexOutOfRangeException(Properties.Resources.BelowRange);
            }

            result = students[prevStudentIndex];
        }

        public static int GetPrevStudentIndex(IList<Student> students, int current, PropertyInfo attribute,
                                              string text) {
            if (students == null || current < 1) {
                return -1;
            }

            if (attribute == null || string.IsNullOrEmpty(text)) {
                return current - 1;
            }

            return students.ToList().FindLastIndex(current - 1, s => FilterByPropertyValue(s, attribute, text));
        }

        private static bool FilterByPropertyValue<T>(T obj, PropertyInfo property, string value) {
            string propertyValue = (string) obj.GetType().GetProperties().ToList()
                                               .Find(p => p.Name == property.Name)
                                               ?.GetValue(obj) ?? "";
            return Regex.IsMatch(propertyValue, $"^{value}.*$");
        }

        public static void OpenStudentList<T>(out T students) where T : IEnumerable<Student> {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) {
                students = default;
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using XmlReader reader = XmlReader.Create(new FileStream(openFileDialog.FileName, FileMode.Open));
            students = (T) xmlSerializer.Deserialize(reader);
        }

        public static void SaveStudentList(IEnumerable<Student> students) {
            Contract.Assert(students != null, "students != null");

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != true) {
                return;
            }
            
            var xmlSerializer = new XmlSerializer(students.GetType());
            using var writer = new FileStream(openFileDialog.FileName, FileMode.Create);
            xmlSerializer.Serialize(writer, students);
        }

        public static void CreateStudentList<T>(out T students) where T : IEnumerable<Student>, new() {
            students = new T();
        }

        public static bool IsSameTypeStudentExists(IEnumerable<Student> students, Student student) {
            Contract.Assert(students != null, "students != null");
            return student != null && students.Any(s => s.GetType() == student.GetType());
        }

        public static void RemoveRedundantProperties(IList<Student> students, Student student, IList<PropertyInfo> attributes) {
            if (students is null || attributes is null || IsSameTypeStudentExists(students, student)) {
                return;
            }

            student?.GetType().GetProperties().ToList().ForEach(property => {
                if (students.All(p => p.GetType().GetProperties().All(s => s.Name != property.Name))) {
                    attributes.Remove(property);
                }
            });
        }

        public static void AddMissingProperties(Student student, IList<PropertyInfo> attributes) {
            student?.GetType().GetProperties().ToList().ForEach(elem => {
                if (attributes.All(p => p.Name != elem.Name)) {
                    attributes.Add(elem);
                }
            });
        }

        public static void PromoteToMasterStudent(Student student, out Student promotedStudent) {
            Contract.Assert(student != null, "student != null");

            promotedStudent = new Master {
                Firstname = student.Firstname, Lastname = student.Lastname, Faculty = student.Faculty
            };
        }
    }
}