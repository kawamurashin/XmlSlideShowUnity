using Model;
using UnityEngine;
using View;

namespace Controller
{
    public class ControllerManager : MonoBehaviour
    {
        private static ControllerManager _instance;
        public static ControllerManager getInstance()
        {
            if (_instance == null)
            {
                var obj = new GameObject("ControllerManager");
                _instance = obj.AddComponent<ControllerManager>();
            }

            return _instance;
        }

        private ViewManager _viewManager;

        private void Start()
        {
            var _modelManager = ModelManager.getInstance();
            _modelManager.AddLoadCompleteCallback(LoadCompleteCallbackHandler);
            GameObject obj = _modelManager.gameObject;
            obj.transform.parent = this.transform;
            
            obj = new GameObject("ViewManager");
            _viewManager = obj.AddComponent<ViewManager>();
            obj.transform.parent = transform;

            _modelManager.StartLoad();


        }

        private void LoadCompleteCallbackHandler()
        {
            //Debug.Log("controller LoadCompleteCallbackHandler");
            _viewManager.LoadComplete();
            
        }

        private void Update()
        {
            
        }
    }
}