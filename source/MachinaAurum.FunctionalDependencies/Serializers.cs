using System;
using System.IO;
using System.Xml.Serialization;

namespace MachinaAurum.FunctionalDependencies
{
    public class SerializerFunctions
    {
        public Func<object, string> Serialize { get; private set; }

        public SerializerFunctions(System.Xml.Serialization.XmlSerializer serializer)
        {
            Serialize = new Func<object, string>(x =>
            {
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, x);
                    return writer.GetStringBuilder().ToString();
                }
            });
        }
    }

    public static class SerializerExtensions
    {
        public static SerializerFunctions GetFunctions<T>(this System.Xml.Serialization.XmlSerializer serializer)
        {
            return new SerializerFunctions(serializer);
        }
    }

    public static class Serializers
    {
        public static Func<object, string> ToXml
        {
            get
            {
                return new Func<object, string>(x =>
                {
                    using (var writer = new StringWriter())
                    {
                        var serializer = new XmlSerializer(x.GetType());

                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");

                        serializer.Serialize(writer, x, ns);
                        return writer.GetStringBuilder().ToString();
                    }
                });
            }
        }

        public static Func<string, T> FromXml<T>()
        {
            return new Func<string, T>(x =>
            {
                using (var reader = new StringReader(x))
                {
                    var serializer = new XmlSerializer(typeof(T));

                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    return (T)serializer.Deserialize(reader);
                }
            });
        }
    }
}
