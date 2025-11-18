using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrenade : BulletBase
{
	private float timer;
	private Vector3 speed;
	private GameObject hitEffect;

	void Update()
	{
		if (!isAlive) return;

		speed.y -= ActorBase.GRAVITATIONAL_ACCELERATION * Time.deltaTime;
		Vector3 newPos = transform.position + speed * Time.deltaTime;

		if (speed.y < 0 && transform.position.y < 0.03f)
		{
			newPos.y = 0.03f;
			speed = speed.normalized * speed.magnitude * 0.3f;
			speed.y = -speed.y;
		}
		transform.position = newPos;

		timer += Time.deltaTime;
		if (timer >= bulletConf.explodeTime)
		{
			DoAttack();
			isAlive = false;
			BulletManager.RecycleBullet(this);
		}
	}

	public void Init(ActorBase attacker, BulletConf bullet, Vector3 origin, Vector3 dir)
	{
		timer = 0f;
		isAlive = true;
		bulletConf = bullet;
		Attacker = attacker;
		transform.position = origin;
		dir = Vector3.RotateTowards(dir, Vector3.up, Mathf.Deg2Rad*20, Mathf.Deg2Rad * 20);
		speed = dir.normalized * bullet.flySpeed.z;
	}

	void DoAttack()
	{
		List<ActorBase> targetList = GameMain.instance.GetEnemyList(Attacker);
		for (int i = 0; i < targetList.Count; i++)
		{
			if(!targetList[i].IsAlive()) continue;

			Vector3 dist = targetList[i].transform.position - transform.position;
			if (dist.sqrMagnitude < bulletConf.damageDistance * bulletConf.damageDistance)
			{
				targetList[i].BeAttacked(bulletConf.damage);
				//Debug.Log("Hit " + targetList[i].transform.name);
			}
		}

		if (hitEffect == null && bulletConf.hitEffect != null)
		{
			hitEffect = GameObject.Instantiate(bulletConf.hitEffect);
		}

		hitEffect.SetActive(false);
		hitEffect.transform.position = transform.position;
		hitEffect.SetActive(true);
}
}
