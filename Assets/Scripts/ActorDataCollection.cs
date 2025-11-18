using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDataCollection : MonoBehaviour
{
	public static ActorDataCollection instance;

	public ActorConf[] ConfArray;

	// Use this for initialization
	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}

	public static ActorConf GetConf(int id)
	{
		for (int i = 0; i < instance.ConfArray.Length; i++)
		{
			ActorConf conf = instance.ConfArray[i];
			if (conf.actorId == id)
				return conf;
		}
		Debug.LogErrorFormat("id = {0} is not found in ActorDataCollection", id);
		return null;
	}
}
