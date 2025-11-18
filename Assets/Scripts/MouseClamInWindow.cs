using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClamInWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Vector3 mousePos = Input.mousePosition;
		//mousePos.x = Mathf.Clamp(mousePos.x, - Screen.width * 0.5f, Screen.width * 0.5f);
		//mousePos.y = Mathf.Clamp(mousePos.y, -Screen.height * 0.5f, Screen.height * 0.5f);
		//Application.set
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
