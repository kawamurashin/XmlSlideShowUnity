using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Model.Data;
using Model.ImageLoader;
using UnityEngine;
using UnityEngine.Networking;

namespace Model
{
    public class ModelManager:MonoBehaviour
    {
        private List<SceneData> _sceneDataList;
        public delegate void Callback();

        private Callback loadCompleteCallback;

        public void AddLoadCompleteCallback(Callback callback)
        {
            loadCompleteCallback += callback;
        }
        private static ModelManager _instance;
        

        public static ModelManager getInstance()
        {
            if (_instance == null)
            {
                var obj = new GameObject("ModelManager");
                _instance = obj.AddComponent<ModelManager>();
            }
            return _instance;
        }

        public void StartLoad()
        {

            StartCoroutine(LoadingCoroutine());
        }

        private IEnumerator LoadingCoroutine()
        {
            var path = Application.streamingAssetsPath + "/data.xml";
            var unityWebRequest = UnityWebRequest.Get(path);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.isNetworkError)
            {
                //Debug.Log("Error");
            }
            else
            {

                if (unityWebRequest.responseCode == 200)
                {
                    _sceneDataList = new List<SceneData>();
                    var text = unityWebRequest.downloadHandler.text;
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(new StringReader(text));
                    
                    XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("scenes");
                    XmlNode xmlNode = xmlNodeList[0];
                    XmlNodeList list = xmlNode.ChildNodes;
                    var n = list.Count;
                    for (var i = 0; i < n; i++)
                    {
                        var node = list[i];
                        var sceneData = new SceneData();
                        sceneData.SetXmlNode(node);

                        _sceneDataList.Add(sceneData);
                    }
                    
                    var obj = new GameObject("ImageLoader");
                    var imageLoaderManager = obj.AddComponent<ImageLoaderManager>();
                    imageLoaderManager.AddLoadCompleteCallback(ImageLoadCompleteCallbackHandler);
                    imageLoaderManager.LoadStart();
                }


            }
        }

        private void ImageLoadCompleteCallbackHandler()
        {
            loadCompleteCallback();
        }

        public List<SceneData> SceneDataList
        {
            get { return _sceneDataList; }
        }
    }
}