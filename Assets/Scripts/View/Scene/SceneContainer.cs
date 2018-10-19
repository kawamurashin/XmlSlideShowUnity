using System;
using System.Runtime.InteropServices;
using Model.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace View.Scene
{
    public class SceneContainer : MonoBehaviour
    {
        private int _imageCount;
        private SceneData _sceneData;

        private GameObject _backgroundContainer;

        //
        private Image _imageContainer;
        private Image _removeImageContainer;

        public delegate void Callback();

        private Callback tweenCompleteCallback;

        public void AddTweenCompleteCallback(Callback callback)
        {
            tweenCompleteCallback += callback;
        }

        private void Awake()
        {
            _backgroundContainer = new GameObject("ImageContainer");
            _backgroundContainer.transform.SetParent(transform);
        }

        private void Start()
        {
        }

        public void setSceneData(SceneData sceneData)
        {
            _sceneData = sceneData;
            _imageCount = 0;

            var font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Fonts/Futura PT Book.ttf");

            var obj = new GameObject("text");
            var text = obj.AddComponent<Text>();
            obj.transform.SetParent(transform);
            text.text = _sceneData.Text;
            text.font = font;
            text.fontSize = 64;

            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(640, 480);

            //addImage();
        }
        public void startTween()
        {
            addImage();
        }
        //private
        private void addImage()
        {
            var texture = _sceneData.ImageDataList[_imageCount].Texture;

            var obj = new GameObject("Image");
            _imageContainer = obj.AddComponent<Image>();
            obj.transform.SetParent(_backgroundContainer.transform);
            _imageContainer.sprite =
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            var rectTransform = _imageContainer.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(texture.width, texture.height);
            rectTransform.localPosition = new Vector3(640, 0, 0);


            setTween();
        }

        private void setTween()
        {
            if (_removeImageContainer)
            {
                iTween.MoveTo(_removeImageContainer.gameObject, iTween.Hash(
                    "x", -640, 
                    "time", 1.2,
                    "easeType", iTween.EaseType.easeInCubic
                ));
                iTween.MoveTo(_imageContainer.gameObject, iTween.Hash(
                    "x", 0, 
                    "time", 1.2,
                    "delay", 0.8,
                    "easeType", iTween.EaseType.easeOutCubic,
                    "oncompletetarget", gameObject, 
                    "oncomplete" ,"tweenCompleteHandler"
                ));
            }
            else
            {
                iTween.MoveTo(_imageContainer.gameObject, iTween.Hash(
                    "x", 0, 
                    "time", 1.2,
                    "easeType", iTween.EaseType.easeOutCubic,
                    "oncompletetarget", gameObject, 
                    "oncomplete" ,"tweenCompleteHandler"
                ));
            }
        }
        //
        public void tweenCompleteHandler()
        {
            if(_removeImageContainer)
            {
                _removeImageContainer.gameObject.transform.SetParent(null);
                Destroy(_removeImageContainer.gameObject);
            }
            _removeImageContainer = _imageContainer;

            _imageCount++;
            if (_imageCount >= _sceneData.ImageDataList.Count)
            {
                //_imageCount = 0;
                //addImage();
                tweenCompleteCallback();

            }
            else
            {
                addImage();
            }
        }
    }
}