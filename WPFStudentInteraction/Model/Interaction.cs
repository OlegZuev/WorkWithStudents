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

        public static void AddStudent(IList<Student> students, out Student currentStudent) {
            Contract.Assert(students != null, "students != null");

            var newStudent = new Bachelor();
            //MessageBox.Show("Вы хотите добавить бакалавра или магистра?", "Добавление студента")
            // Написать свой MessageBox
            students.Add(newStudent);
            currentStudent = newStudent;
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
                result = null;
                return;
            }

            students.Remove(currentStudent);
            result = students[anotherStudentIndex];
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
                                               .GetValue(obj);
            return Regex.IsMatch(propertyValue, $"^{value}.*$");
        }

        public static void OpenStudentList<T>(out T students) where T : IEnumerable<Student> {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) {
                students = default;
                return;
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = XmlReader.Create(new FileStream(openFileDialog.FileName, FileMode.Open))) {
                students = (T) xmlSerializer.Deserialize(reader);
            }
        }

        public static void SaveStudentList(IEnumerable<Student> students) {
            Contract.Assert(students != null, "students != null");

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() != true)
                throw new FileNotFoundException(Properties.Resources.XmlFileNotFound);

            var xmlSerializer = new XmlSerializer(students.GetType());
            using (var writer = new FileStream(openFileDialog.FileName, FileMode.Create)) {
                xmlSerializer.Serialize(writer, students);
            }
        }

        public static void CreateStudentList<T>(out T students) where T : IEnumerable<Student>, new() {
            students = new T();
        }
    }
}