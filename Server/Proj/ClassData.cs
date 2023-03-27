using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Proj {
    class ClassData : IEnumerable {
        private readonly Dictionary<string, string> Data = new();

        public string Idspace;
        public string Category;

        public ClassData() {
        }

        public ClassData(ClassData data) {
            Data = new Dictionary<string, string>(data.Data);
            Idspace = data.Idspace;
            Category = data.Category;
        }

        public ClassData(string className) {
            var data = Parser.GetClass(className);

            Data = new Dictionary<string, string>(data.Data);
            Idspace = data.Idspace;
            Category = data.Category;
        }

        public IEnumerator GetEnumerator() {
            return Data.GetEnumerator();
        }

        public void Add(string idspace, string category, KeyValuePair<string, string> data) {
            Idspace = idspace;
            Category = category;
            var kv = new KeyValuePair<string, string>(data.Key, data.Value);
            Data.Add(kv.Key, kv.Value);
        }

        public void Add(string idspace, string category, string key, string value) {
            Idspace = idspace;
            Category = category;

            Data.Add(key, value);
        }

        public int GetInt(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    return int.Parse(prop.Value);
                }
            }

            return 0;
        }

        public bool GetBool(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    return bool.Parse(prop.Value);
                }
            }

            return false;
        }
        public double GetDouble(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    return double.Parse(prop.Value);
                }
            }

            return 0.0;
        }

        public string GetString(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    return prop.Value;
                }
            }

            return "";
        }

        public Vector2 GetVector2(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    var str = prop.Value;
                    string[] temp = str.Substring(1, str.Length - 2).Split(',');

                    return new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                }
            }

            return Vector2.Zero;
        }

        public string[] GetStringArray(string propName) {
            foreach (var prop in Data) {
                if (prop.Key == propName) {
                    var str = prop.Value;
                    return str.Substring(1, str.Length - 2).Split(',');
                }
            }

            return Array.Empty<string>();
        }
    }
}
