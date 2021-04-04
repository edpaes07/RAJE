using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Raje.Infra.Util
{
    public static class SerializationHelper
    {
        #region XML Serialization Methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static TObject DeserializeFromXml<TObject>(String xmlText)
        {
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object item = null;

            if (String.IsNullOrEmpty(xmlText))
                return default(TObject);

            using (StringReader stringReader = new StringReader(xmlText))
            {
                xmlReader = new XmlTextReader(stringReader);
                serializer = new XmlSerializer(typeof(TObject));
                item = serializer.Deserialize(xmlReader);

                if (xmlReader != null)
                    xmlReader.Close();
            }
            return (TObject)item;
        }

        public static String SerializeToXml<TObject>(TObject @object)
        {
            XmlSerializer serializer = null;
            StringBuilder output = null;
            String key = typeof(TObject).FullName;

            if (@object == null)
                return null;

            output = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(output, new XmlWriterSettings() { OmitXmlDeclaration = true }))
            {
                serializer = new XmlSerializer(typeof(TObject));
                serializer.Serialize(writer, @object);
                return output.ToString();
            }
        }

        #endregion XML Serialization Methods
    }
}