using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using Proj.Enum;

namespace Proj {
    class Parser {

        private static readonly Dictionary<string, List<Dictionary<string, List<ClassData>>>> IdspaceList = new();
        private static readonly Dictionary<string, List<ClassData>> CategoryList = new();
        private static readonly List<ClassData> ClassList = new();



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

                    var categoryData = new List<Dictionary<string, List<ClassData>>>();
                    foreach (var categories in XmlDoc.LastChild) {
                        var category = (XmlNode)categories;
                        var categoryName = category.Attributes?[0].Value;
                        if (string.IsNullOrEmpty(categoryName)) {
                            Console.WriteLine("Invalid Data - XML Data has no Category Name");
                            return;
                        }

                        var categoryClasses = new List<ClassData>();

                        foreach (var classes in category) {
                            var c = (XmlNode)classes;
                            var classProps = new ClassData();

                            foreach (var prop in c.Attributes) {
                                var p = (XmlNode)prop;
                                var propName = p.Name;
                                var propValue = p.Value;

                                classProps.Add(idspace, categoryName, propName, propValue);
                            }
                            ClassList.Add(classProps);
                            categoryClasses.Add(new ClassData(classProps));
                        }

                        CategoryList[categoryName] = new List<ClassData>(categoryClasses);
                        categoryData.Add(CategoryList);
                    }
                    IdspaceList[idspace] = new List<Dictionary<string, List<ClassData>>>(categoryData);
                }
            }
        }

        public static List<Dictionary<string, List<ClassData>>> GetIdspace(string idspace) {
            return new List<Dictionary<string, List<ClassData>>>(IdspaceList[idspace]);
        }

        public static List<ClassData> GetCategory(string idspace, string category) {
            return new List<ClassData>(CategoryList[category]);
        }

        public static ClassData GetClass(string className) {
            foreach (var @class in ClassList) {
                if (string.Equals(@class.GetString(PropName.ClassName), className)) {
                    return @class;
                }
            }

            return null;
        }

        public static ClassData GetClass(int classId) {
            foreach (var @class in ClassList) {
                if (Equals(@class.GetInt(PropName.ClassId), classId)) {
                    return @class;
                }
            }

            return null;
        }

        public static int GetInt(string className, string propName) {
            var @class = GetClass(className);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return int.Parse(prop.Value);
                }
            }

            return 0;
        }

        public static int GetInt(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return int.Parse(prop.Value);
                }
            }

            return 0;
        }

        public static bool GetBool(string className, string propName) {
            var @class = GetClass(className);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return bool.Parse(prop.Value);
                }
            }

            return false;
        }

        public static bool GetBool(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return bool.Parse(prop.Value);
                }
            }

            return false;
        }

        public static double GetDouble(string className, string propName) {
            var @class = GetClass(className);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return double.Parse(prop.Value);
                }
            }

            return 0.0;
        }

        public static double GetDouble(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return double.Parse(prop.Value);
                }
            }

            return 0.0;
        }

        public static string GetString(string className, string propName) {
            var @class = GetClass(className);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return prop.Value;
                }
            }

            return "";
        }

        public static string GetString(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    return prop.Value;
                }
            }

            return "";
        }

        public static Vector2 GetVector2(string className, string propName) {
            var @class = GetClass(className);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    var str = prop.Value;
                    string[] temp = str.Substring(1, str.Length - 2).Split(',');

                    return new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                }
            }

            return Vector2.Zero;
        }

        public static Vector2 GetVector2(int classId, string propName) {
            var @class = GetClass(classId);

            foreach (KeyValuePair<string, string> prop in @class) {
                if (prop.Key == propName) {
                    var str = prop.Value;
                    string[] temp = str.Substring(1, str.Length - 2).Split(',');

                    return new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                }
            }

            return Vector2.Zero;
        }

    }
}
