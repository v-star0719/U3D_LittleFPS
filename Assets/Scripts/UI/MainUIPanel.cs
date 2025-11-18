using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIPanel : MonoBehaviour
{
	public HpCtrl hpCtr;
	public UILabel enemyLabel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayerHp(int n)
	{
		hpCtr.Set(n);
	}

	public void OnBagBtnClick()
	{
		UIManager.instance.bagPanel.OpenPanel();
	}

	public void SetEnemyCount(int total, int alive)
	{
		enemyLabel.text = String.Format("敌人数：{0}/{1}", alive, total);
	}
}
