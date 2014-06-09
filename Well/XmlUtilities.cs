using System.IO;
using System.Xml.Serialization;

namespace Well
{
    public class XmlUtilities<T>
    {
        public static void Serialize(T data, string fileName)
        {
            var serializer = new XmlSerializer(typeof (T));
            TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, data);
            writer.Close();
        }

        public static T Deserialize(string fileName)
        {
            var serializer = new XmlSerializer(typeof (T));
            var reader = new StreamReader(fileName);
            return (T) serializer.Deserialize(reader);
        }
    }
}