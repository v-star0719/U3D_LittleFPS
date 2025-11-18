using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataCollection : MonoBehaviour {

	public static WeaponDataCollection instance;

	public WeaponConf[] ConfArray;

	// Use this for initialization
	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}

	public static WeaponConf GetConf(int id)
	{
		for (int i = 0; i < instance.ConfArray.Length; i++)
		{
			WeaponConf conf = instance.ConfArray[i];
			if (conf.weaponId == id)
				return conf;
		}
		Debug.LogErrorFormat("id = {0} is not found in WeaponDataCollection", id);
		return null;
	}
}
