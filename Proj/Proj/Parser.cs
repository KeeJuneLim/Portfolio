using System;
using System.Collections.Generic;
using System.Xml;

namespace Proj {
    class Parser {

        private static readonly Dictionary<string, List<Dictionary<string, List<Dictionary<string, string>>>>> IdspaceList = new();
        private static readonly Dictionary<string, List<Dictionary<string, string>>> CategoryList = new();
        private static readonly List<Dictionary<string, string>> ClassList = new ();



        public static void LoadXmlData() {
            XmlDocument XmlDoc = new();

            var folderName = "../../../../../Data/";
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(folderName);
            foreach (System.IO.FileInfo file in directory.GetFiles()) {
                if (string.Compare(file.Extension.ToLower(), ".xml", StringComparison.Ordinal) == 0) {
                    var fileName = file.Name;

                    XmlDoc.Load(folderName + fileName);

                    if (!XmlDoc.HasChildNodes) {
                        Console.WriteLine("Invalid Data - XML Data has no ChileNodes");
                        return;
                    }

                    var idspace = XmlDoc.LastChild?.Attributes?[0].Value;

                    if (string.IsNullOrEmpty(idspace)) {
                        Console.WriteLine("Invalid Data - XML Data has no Idspace");
                        return;
                    }

                    var categoryData = new List<Dictionary<string, List<Dictionary<string, string>>>>();
                    foreach (var categories in XmlDoc.LastChild) {
                        var category = (XmlNode)categories;
                        var categoryName = category.Attributes?[0].Value;
                        if (string.IsNullOrEmpty(categoryName)) {
                            Console.WriteLine("Invalid Data - XML Data has no Category Name");
                            return;
                        }

                        var categoryClasses = new List<Dictionary<string, string>> ();

                        foreach (var classes in category) {
                            var c = (XmlNode)classes;
                            var classProps = new Dictionary<string, string>();

                            foreach (var prop in c.Attributes) {
                                var p = (XmlNode)prop;
                                var propName = p.Name;
                                var propValue = p.Value;

                                var properties = new KeyValuePair<string, string>(propName, propValue);

                                classProps.Add(propName, propValue);
                            }
                            ClassList.Add(classProps);
                            categoryClasses.Add(classProps);
                        }

                        CategoryList[categoryName] = categoryClasses;
                        categoryData.Add(CategoryList);
                    }
                    IdspaceList[idspace] = categoryData;
                }
            }
        }

        public static List<Dictionary<string, List<Dictionary<string, string>>>> GetIdspace(string idspace) {
            return IdspaceList[idspace];
        }

        public static List<Dictionary<string, string>> GetCategory(string idspace, string category) {
            return CategoryList[category];
        }

        public static Dictionary<string, string> GetClass(string className) {

            foreach (var @class in ClassList) {
                foreach (var prop in @class) {
                    if (string.Equals(prop.Key, "ClassName")) {
                        if (prop.Value == className) {
                            return @class;
                        }
                    }
                }
            }

            return null;
        }

        public static Dictionary<string, string> GetClass(int classId) {
            foreach (var @class in ClassList) {
                foreach (var prop in @class) {
                    if (string.Equals(prop.Key, "classId")) {
                        if (prop.Value == classId.ToString()) {
                            return @class;
                        }
                    }
                }
            }

            return null;
        }

        public static int GetInt(string className, string propName) {
            var @class = GetClass(className);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return int.Parse(prop.Value);
                }
            }

            return 0;
        }

        public static int GetInt(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return int.Parse(prop.Value);
                }
            }

            return 0;
        }

        public static bool GetBool(string className, string propName) {
            var @class = GetClass(className);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return bool.Parse(prop.Value);
                }
            }

            return false;
        }

        public static bool GetBool(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return bool.Parse(prop.Value);
                }
            }

            return false;
        }

        public static double GetDouble(string className, string propName) {
            var @class = GetClass(className);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return double.Parse(prop.Value);
                }
            }

            return 0.0;
        }

        public static double GetDouble(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (var prop in @class) {
                if (prop.Key == propName) {
                    return double.Parse(prop.Value);
                }
            }

            return 0.0;
        }
    }
}
