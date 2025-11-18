using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmBulletType
{
	GunBullet,
	GrenadeBulle,
	WorldPoint,
}

//public enum EmBulletTargeType
//{
//	HitOneEnemy,
//	AllEnemyInRound,
//}


[System.Serializable]
public class BulletConf
{
	public int bulleId;
	public EmBulletType bulletType;
	public float damage;
	public float attackDistance;
	public float damageDistance;
	public bool isLightSpeed;
	public Vector3 flySpeed;
	public float explodeTime;//爆炸时间
	public GameObject bulletGameObject;
	public GameObject hitEffect;
}
