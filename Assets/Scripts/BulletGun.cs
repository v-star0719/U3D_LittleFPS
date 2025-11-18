using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : BulletBase
{
	private Ray ray;
	private float dist = 0;

	private GameObject hitEffect;

	void Update()
	{
		if (!isAlive) return;

		if (bulletConf.isLightSpeed)
		{
			DoAttack();
			isAlive = false;
			BulletManager.RecycleBullet(this);
		}
		else
		{
			Vector3 delta = transform.forward * bulletConf.flySpeed.z * Time.deltaTime;
			transform.position += delta;
			dist += delta.magnitude;

			if (dist > 200)
			{
				isAlive = false;
				BulletManager.RecycleBullet(this);
			}
		}
	}

	public void Init(ActorBase attacker, BulletConf bullet, Ray ray)
	{
		this.ray = ray;
		isAlive = true;
		bulletConf = bullet;
		Attacker = attacker;
		transform.position = ray.origin;
		transform.forward = ray.direction.normalized;
		dist = 0;
	}

	void DoAttack()
	{
		RaycastHit hitInfo;
		string[] layers = new string[] { "Box", "Monster" };
		if (Physics.Raycast(ray, out hitInfo, 200, LayerMask.GetMask(layers)))
		{
			if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
			{
				ActorBase actorBase = hitInfo.transform.GetComponent<ActorBase>();
				actorBase.BeAttacked(bulletConf.damage);
			}
			Debug.Log("Hit " + hitInfo.transform.name);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			ActorBase actorBase = other.transform.GetComponent<ActorBase>();
			actorBase.BeAttacked(bulletConf.damage);
		}

		if (hitEffect == null && bulletConf.hitEffect != null)
		{
			hitEffect = GameObject.Instantiate(bulletConf.hitEffect);
		}
		hitEffect.SetActive(false);
		hitEffect.transform.position = other.transform.position + new Vector3(0, other.bounds.size.y*0.5f, 0);
		hitEffect.SetActive(true);

		//Debug.Log("Hit " + other.transform.name);
		isAlive = false;
		BulletManager.RecycleBullet(this);
	}


}
