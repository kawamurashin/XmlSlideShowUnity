using System.Collections.Generic;
using Model;
using Model.Data;
using UnityEngine;

namespace View.Scene
{
    public class SceneManager : MonoBehaviour
    {
        private int _sceneCount = 0;
        private List<SceneData> _sceneDataList;
        //
        private SceneContainer _sceneContainer;
        private SceneContainer _removeSceneContainer;

        private void Start()
        {
            _sceneCount = 0;
        }

        public void StartScene()
        {
            var modelManager = ModelManager.getInstance();
            _sceneDataList = modelManager.SceneDataList;
            //
            _sceneCount = 0;
            addScene();

        }

        private void addScene()
        {
            var sceneData = _sceneDataList[_sceneCount];
            
            var obj = new GameObject("SceneContainer");
            _sceneContainer = obj.AddComponent<SceneContainer>();
            _sceneContainer.setSceneData(sceneData);
            obj.transform.SetParent(transform);
            _sceneContainer.AddTweenCompleteCallback(sceneContainerCompleteCallbackHandler);


            obj.transform.localPosition = new Vector3(0, 480, 0);

            setTween();
        }

        private void setTween()
        {
            if(_removeSceneContainer)
            {

                iTween.MoveTo(_removeSceneContainer.gameObject, iTween.Hash(
                    "y", 480, 
                    "time", 1.2,
                    "easeType", iTween.EaseType.easeInCubic
                ));
                iTween.MoveTo(_sceneContainer.gameObject, iTween.Hash(
                    "y", 0, 
                    "time", 1.2,
                    "delay", 0.8,
                    "easeType", iTween.EaseType.easeOutCubic,
                    "oncompletetarget", gameObject, 
                    "oncomplete" ,"tweenCompleteCallbackHandler"
                ));
            }
            else
            {
                iTween.MoveTo(_sceneContainer.gameObject, iTween.Hash(
                    "y", 0, 
                    "time", 1.2,
                    "easeType", iTween.EaseType.easeOutCubic,
                    "oncompletetarget", gameObject, 
                    "oncomplete" ,"tweenCompleteCallbackHandler"
                ));
            }
        }

        private void nextScene()
        {
            _sceneCount++;
            if (_sceneCount >= _sceneDataList.Count) {
                _sceneCount = 0;
            }
            addScene();
        }

        private void tweenCompleteCallbackHandler()
        {
            _sceneContainer.startTween();
            
            if (_removeSceneContainer ){
                _removeSceneContainer.gameObject.transform.SetParent(null);
                Destroy(_removeSceneContainer.gameObject);
            }
            _removeSceneContainer = _sceneContainer;
            
        }

        private void sceneContainerCompleteCallbackHandler()
        {
            //Debug.Log("sceneContainerCompleteCallbackHandler");
            nextScene();
        }
    }
}