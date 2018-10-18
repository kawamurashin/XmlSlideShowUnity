using System.Xml;
using UnityEngine;

namespace Model
{
    public class ImageData
    {
        private string _id;
        private string _path;
        private Texture2D _texture;

        public void SetXmlNode(XmlNode xmlNode)
        {
            _id = xmlNode.Attributes["id"].Value;
            _path = xmlNode.InnerText;
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public string Id
        {
            get { return _id; }
        }

        public string Path
        {
            get { return _path; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }
    }
}