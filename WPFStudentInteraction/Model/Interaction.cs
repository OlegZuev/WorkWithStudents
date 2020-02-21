using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using Students;

namespace WPFStudentInteraction.Model {
    public static class Interaction {
        public static void AddStudent(IList<Student> students, out Student currentStudent) {
            Contract.Assert(students != null, "students != null");

            var newStudent = new Student();
            students.Add(newStudent);
            currentStudent = newStudent;
        }

        public static void RemoveStudent(IList<Student> students, ref Student currentStudent) {
            Contract.Assert(students != null, "students != null");

            students.Remove(currentStudent);
            currentStudent = students.Last();
        }

        public static void NextStudent(IList<Student> students, ref Student currentStudent) {
            Contract.Assert(students != null, "students != null");
            if (students.Last() == currentStudent) {
                throw new IndexOutOfRangeException(Properties.Resources.AboveRange);
            }

            int current = students.IndexOf(currentStudent);
            currentStudent = students[current + 1];
        }

        public static void PrevStudent(IList<Student> students, ref Student currentStudent) {
            Contract.Assert(students != null, "students != null");
            if (students.First() == currentStudent) {
                throw new IndexOutOfRangeException(Properties.Resources.BelowRange);
            }

            int current = students.IndexOf(currentStudent);
            currentStudent = students[current - 1];
        }

        public static void OpenStudentList<T>(ref T students) where T : IEnumerable<Student> {
            Contract.Assert(students != null, "students != null");

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true)
                throw new FileNotFoundException(Properties.Resources.XmlFileNotFound);

            var xmlSerializer = new XmlSerializer(students.GetType());
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
    }
}