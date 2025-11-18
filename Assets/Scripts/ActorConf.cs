using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActorConf
{
	public int actorId;
	public string actorName;
	public float hp;
	public float walkForwardSpeed;
	public float walkBackwardSpeed;
	public float walkLeftSpeed;
	public float walkRightSpeed;
	public float runSpeed;
	public Vector3 jumpForce;
	public float attackDistance;
	public float attackTimePoint;
	public float attackTimePoint2;
	public int bulletId1;
	public int bulletId2;
}
