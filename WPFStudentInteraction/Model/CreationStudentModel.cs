using System;
using System.Diagnostics.Contracts;
using Students;
using WPFStudentInteraction.View;

namespace WPFStudentInteraction.Model {
    public static class CreationStudentModel {
        public static void GetNewStudentType(string content, CreationStudentWindow window) {
            Contract.Assert(window != null, "window != null");

            if (content == Properties.Resources.BachelorFriendlyName) {
                window.Result = typeof(Bachelor);
            } else if (content == Properties.Resources.MasterFriendlyName) {
                window.Result = typeof(Master);
            } else {
                throw new ArgumentException(Properties.Resources.UnexpectedStudentType);
            }

            window.DialogResult = true;
            window.Close();
        }
    }
}