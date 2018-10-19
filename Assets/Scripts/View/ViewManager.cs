using UnityEngine;
using View.Scene;

namespace View
{
    public class ViewManager : MonoBehaviour
    {

        private SceneManager _sceneManager;

        public void LoadComplete()
        {
            _sceneManager.StartScene();
        }

        private void Start()
        {
            var canvasObject = new GameObject("Canvas");
            var canvas = canvasObject.AddComponent<Canvas>();
            canvasObject.transform.SetParent(transform);
            var rectTransform = canvasObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(640,480);
            
            
            var obj = new GameObject("SceneManager");
            _sceneManager = obj.AddComponent<SceneManager>();
            _sceneManager.transform.SetParent(canvasObject.transform);
        }


    }

}