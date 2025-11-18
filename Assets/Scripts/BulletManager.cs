using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	public static BulletManager instance;

	public LinkedList<BulletBase> aliveBulletList = new LinkedList<BulletBase>();
	public LinkedList<BulletBase> deadBulletList = new LinkedList<BulletBase>();

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

	public static BulletBase GetBullet(BulletConf bulletConf)
	{
		LinkedListNode<BulletBase> node = instance.deadBulletList.First;
		while (node != null)
		{
			if (node.Value.bulletConf.bulletType == bulletConf.bulletType)
			{
				instance.deadBulletList.Remove(node);
				instance.aliveBulletList.AddLast(node);
				node.Value.gameObject.SetActive(true);
				//Debug.LogFormat("find bulllet, alive = {0}, dead = {1}", instance.aliveBulletList.Count, instance.deadBulletList.Count);
				return node.Value;
			}
			node = node.Next;
		}

		GameObject go = null;
		if (bulletConf.bulletGameObject == null)
		{
			go = new GameObject("Bullet");
		}
		else
		{
			go = GameObject.Instantiate(bulletConf.bulletGameObject);
		}
		go.transform.parent = instance.transform;
		go.transform.localScale = Vector3.one;
		go.transform.localRotation = Quaternion.identity;

		BulletBase bullet = null;
		if (bulletConf.bulletType == EmBulletType.GunBullet)
		{
			bullet = go.AddComponent<GunBullet>();
		}
		else if (bulletConf.bulletType == EmBulletType.GrenadeBulle)
		{
			bullet = go.AddComponent<BulletGrenade>();
		}
		else if (bulletConf.bulletType == EmBulletType.WorldPoint)
		{
			bullet = go.AddComponent<BulletWordPoint>();
		}
		instance.aliveBulletList.AddLast(bullet);
		//Debug.LogFormat("create bulllet, alive = {0}, dead = {1}", instance.aliveBulletList.Count, instance.deadBulletList.Count);
		return bullet;
	}

	public static void RecycleBullet(BulletBase bullet)
	{
		LinkedListNode<BulletBase> node = instance.aliveBulletList.Find(bullet);
		bullet.gameObject.SetActive(false);
		instance.aliveBulletList.Remove(node);
		instance.deadBulletList.AddLast(node);
		//Debug.LogFormat("recycle bulllet, alive = {0}, dead = {1}", instance.aliveBulletList.Count, instance.deadBulletList.Count);
	}
}
