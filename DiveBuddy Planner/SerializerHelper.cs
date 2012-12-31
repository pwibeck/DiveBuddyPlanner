using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;

namespace DiveBuddy_Planner
{
    public static class SerializerHelper
    {
        public static T DeSeralize<T>(string fileName)
        {
            T result = default(T);
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStorage.FileExists(fileName))
                {
                    //isoStorage.DeleteFile(fileName);
                    using (IsolatedStorageFileStream file = isoStorage.OpenFile(fileName, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(file))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(T));
                            result = (T)serializer.Deserialize(reader);
                        }
                    }
                }
            }

            return result;
        }

        public static void Serialize<T>(string fileName, T obj)
        {
            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStorage.FileExists(fileName))
                {
                    isoStorage.DeleteFile(fileName);
                }

                using (IsolatedStorageFileStream file = isoStorage.OpenFile(fileName, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(writer, obj);                        
                    }
                }
            }
        }
    }
}
