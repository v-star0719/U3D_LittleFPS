using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
	public GameObject winGroup;

	public GameObject loseGroup;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open(bool win)
	{
		gameObject.SetActive(true);
		winGroup.SetActive(win);
		loseGroup.SetActive(!win);
	}

	public void RestartBtnClick()
	{
		gameObject.SetActive(false);
		GameMain.instance.Restart();
	}
}
