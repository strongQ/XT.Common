using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XT.Common.Extensions
{
    public class XmlExtension
    {
        public static void SaveObjectToXml<T>(string saveFileName)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                TextWriter textWriter = new StreamWriter(saveFileName);
                xmlSerializer.Serialize(textWriter, typeof(T));
                textWriter.Close();
            }
            catch (Exception innerException)
            {
                throw new Exception("SaveObjectToXml() Exception", innerException);
            }
        }

        public static T ReadXmlToObject<T>(string readFileName, bool IsRead = false) where T : class
        {

            if (File.Exists(readFileName))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    FileStream fileStream = null;
                    fileStream = !IsRead ? new FileStream(readFileName, FileMode.Open) : new FileStream(readFileName, FileMode.Open, FileAccess.Read);
                    var data = Convert.ChangeType(xmlSerializer.Deserialize(fileStream), typeof(T));
                    fileStream.Close();
                    return (T)data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
    }
}
