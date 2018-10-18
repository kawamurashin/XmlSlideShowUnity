using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Model.Data
{
    public class SceneData
    {
        private string _id;
        private string _text;
        private List<ImageData> _imageDataList;
        public void SetXmlNode(XmlNode xmlNode)
        {
            _id = xmlNode.Attributes["id"].Value;
            if (xmlNode["text"] != null)
            {
                _text = xmlNode["text"].InnerText;
            }
            
            _imageDataList = new List<ImageData>();
            if (xmlNode["images"] != null)
            {
                var list = xmlNode["images"].ChildNodes;
                var n = list.Count;
                for (var i = 0; i < n; i++)
                {
                    var child = list[i];
                    var imageData = new ImageData();
                    imageData.SetXmlNode(child);

                    _imageDataList.Add(imageData);
                }
            }
            
        }

        public string Id
        {
            get { return _id; }
        }

        public string Text
        {
            get { return _text; }
        }

        public List<ImageData> ImageDataList
        {
            get { return _imageDataList; }
        }
    }
}