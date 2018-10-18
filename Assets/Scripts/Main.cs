using Controller;
using UnityEngine;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		var controllerManager = ControllerManager.getInstance();
		var obj = controllerManager.gameObject;
		obj.transform.parent = transform;

	}
}
