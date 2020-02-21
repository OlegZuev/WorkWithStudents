using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students {
    public class Student {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Faculty { get; set; }

        public Student(string firstname, string lastname, string faculty) {
            Firstname = firstname;
            Lastname = lastname;
            Faculty = faculty;
        }

        public Student() { }
    }
}