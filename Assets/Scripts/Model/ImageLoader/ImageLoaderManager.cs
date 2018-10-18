using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Model.ImageLoader
{
    
    public class ImageLoaderManager : MonoBehaviour
    {
        public delegate void Callback();

        private Callback loadCompleteCallback;

        public void AddLoadCompleteCallback(Callback callback)
        {
            loadCompleteCallback += callback;
        }
        private int _imageCount = 0;
        private int _sceneCount = 0;
        private List<SceneData> _sceneDataList;
        public void LoadStart()
        {
            var modelManager = ModelManager.getInstance();
            _sceneDataList = modelManager.SceneDataList;
            initSceneLoad();
        }
        
        private void initSceneLoad() {
            _sceneCount = 0;
            setSceneLoad();
        }
        private void setSceneLoad() {
            //_modelManager.sceneDataList[_sceneCount];
            initImageLoad();
        }
        private void initImageLoad() {
            _imageCount = 0;
            setImageLoad();
        }
        private void  setImageLoad(){

           

            StartCoroutine(ImageLoadCoroutine());
        }
        private IEnumerator ImageLoadCoroutine() {
            var path = Application.streamingAssetsPath +"/" +_sceneDataList[_sceneCount].ImageDataList[_imageCount].Path;
            var unityWebRequest = UnityWebRequestTexture.GetTexture(path);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.isNetworkError)
            {
                //Debug.Log("Error");
            }
            else
            {

                if (unityWebRequest.responseCode == 200) ;
                //var texture = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                var texture = ((DownloadHandlerTexture)unityWebRequest.downloadHandler).texture;
                _sceneDataList[_sceneCount].ImageDataList[_imageCount].SetTexture(texture);
                //
                _imageCount++;
                if (_imageCount >= _sceneDataList[_sceneCount].ImageDataList.Count()) {
                    sceneLoadComplete();
                }
                else {
                    this.setImageLoad();
                }
            }
        }
        private void sceneLoadComplete() {
            _sceneCount++;
            if (_sceneCount >= _sceneDataList.Count) {
                loadComplete();
            }
            else {
                setSceneLoad();
            }
        }

        private void loadComplete()
        {
            
            loadCompleteCallback();
        }
    }
}
