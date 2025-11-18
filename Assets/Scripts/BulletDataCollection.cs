using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDataCollection : MonoBehaviour {

	public static BulletDataCollection instance;

	public BulletConf[] ConfArray;

	// Use this for initialization
	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}

	public static BulletConf GetConf(int id)
	{
		for (int i = 0; i < instance.ConfArray.Length; i++)
		{
			BulletConf conf = instance.ConfArray[i];
			if (conf.bulleId == id)
				return conf;
		}
		Debug.LogErrorFormat("id = {0} is not found in BulletDataCollection", id);
		return null;
	}
}
