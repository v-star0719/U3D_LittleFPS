using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItemCtrl : MonoBehaviour
{
	public UILabel nameLabel;

	public UITexture textureCtrl;

	private WeaponConf weaponConf;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetInfo(WeaponConf _conf)
	{
		weaponConf = _conf;
		nameLabel.text = _conf.name;
		textureCtrl.mainTexture = _conf.texture;
	}

	public void OnClick()
	{
		UIManager.instance.bagPanel.OnBagItemClick(weaponConf);
	}
}
