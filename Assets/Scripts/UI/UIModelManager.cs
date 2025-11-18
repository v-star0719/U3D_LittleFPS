using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModelManager : MonoBehaviour
{
	public static UIModelManager instance;
	public ActorBase actor;

	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open()
	{
		actor.Init(1, EmActorType.Player, GameMain.instance.CurWeaponId);
		actor.gameObject.SetActive(true);
	}

	public void Close()
	{
		actor.gameObject.SetActive(false);
	}
}
