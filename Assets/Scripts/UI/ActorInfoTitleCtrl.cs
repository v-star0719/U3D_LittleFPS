using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfoTitleCtrl : MonoBehaviour
{
	private const int Y_OFFSET = 15;//像素单位

	public UILabel nameLabel;
	public UISlider hpSlider;

	private ActorBase actor;
	private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (actor == null) return;

		if (actor.IsDead())
		{
			Dead();
			return;
		}

		hpSlider.value = actor.hp / actor.actorConf.hp;

		Vector3 worldPos = actor.transform.position;
		worldPos.y += boxCollider.size.y;

		Vector3 screenPos = GameMain.instance.PlayerCamera.WorldToScreenPoint(worldPos);
		if (screenPos.z < 0)
			screenPos.y += 10000;

		screenPos.z = 0;
		screenPos.y += Y_OFFSET;

		transform.position = GameMain.instance.uiCamera.ScreenToWorldPoint(screenPos);
	}
	
	public void Init(ActorBase _actor)
	{
		actor = _actor;
		nameLabel.text = actor.actorConf.actorName;
		boxCollider = actor.GetComponentInChildren<BoxCollider>();
	}

	public void Dead()
	{
		actor = null;
		boxCollider = null;
		UIManager.instance.actorInfoTitlePanel.Recycle(this);
	}
}
