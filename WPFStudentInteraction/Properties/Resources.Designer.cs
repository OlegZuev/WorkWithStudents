﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFStudentInteraction.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WPFStudentInteraction.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Следующего студента нет.
        /// </summary>
        public static string AboveRange {
            get {
                return ResourceManager.GetString("AboveRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Бакалавр.
        /// </summary>
        public static string BachelorFriendlyName {
            get {
                return ResourceManager.GetString("BachelorFriendlyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Предыдущего студента нет.
        /// </summary>
        public static string BelowRange {
            get {
                return ResourceManager.GetString("BelowRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Диплом.
        /// </summary>
        public static string Diploma {
            get {
                return ResourceManager.GetString("Diploma", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Факультет.
        /// </summary>
        public static string Faculty {
            get {
                return ResourceManager.GetString("Faculty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Имя.
        /// </summary>
        public static string Firstname {
            get {
                return ResourceManager.GetString("Firstname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Фамилия.
        /// </summary>
        public static string Lastname {
            get {
                return ResourceManager.GetString("Lastname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Магистр.
        /// </summary>
        public static string MasterFriendlyName {
            get {
                return ResourceManager.GetString("MasterFriendlyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вы хотите добавить бакалавра или магистра?.
        /// </summary>
        public static string StudentCreationQuestion {
            get {
                return ResourceManager.GetString("StudentCreationQuestion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неизвестный тип студента.
        /// </summary>
        public static string UnexpectedStudentType {
            get {
                return ResourceManager.GetString("UnexpectedStudentType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не удалось найти файл.
        /// </summary>
        public static string XmlFileNotFound {
            get {
                return ResourceManager.GetString("XmlFileNotFound", resourceCulture);
            }
        }
    }
}
