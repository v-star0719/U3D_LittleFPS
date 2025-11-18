using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWordPoint : BulletBase
{
	private GameObject hitEffect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isAlive)
		{
			DoAttack();
			isAlive = false;
			BulletManager.RecycleBullet(this);
		}
	}

	public void Init(ActorBase attacker, BulletConf bullet, Vector3 pos)
	{
		isAlive = true;
		bulletConf = bullet;
		transform.position = pos;
		Attacker = attacker;
	}

	void DoAttack()
	{
		Transform target = null;
		List<ActorBase> targetList = GameMain.instance.GetEnemyList(Attacker);
		for (int i = 0; i < targetList.Count; i++)
		{
			Vector3 dist = targetList[i].transform.position - transform.position;
			if (dist.sqrMagnitude < bulletConf.damageDistance * bulletConf.damageDistance)
			{
				targetList[i].BeAttacked(bulletConf.damage);
				target = targetList[i].transform;
				//Debug.Log("Hit " + targetList[i].transform.name);
			}
		}

		if (target == null) return;

		if (hitEffect == null && bulletConf.hitEffect != null)
		{
			hitEffect = GameObject.Instantiate(bulletConf.hitEffect);
		}

		hitEffect.SetActive(false);
		hitEffect.transform.position = target.position + target.GetComponent<BoxCollider>().bounds.size * 0.5f;
		hitEffect.SetActive(true);
	}
}
