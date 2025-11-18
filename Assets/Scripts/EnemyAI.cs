using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//发呆后有一定概率进行巡视（乱走）
//如果发现敌人，则进行攻击

public class EnemyAI : MonoBehaviour
{
	enum EmMyStatus
	{
		Idle,
		RandomMoveDirection,
		MoveToTarget,
		RandomMoveDirectionAfterAttack,
		AttackPrepair,
		NormalAttack,
		BeAttacked,
	}

	public ActorBase actor;

	private EmMyStatus curStatus;
	private float statusDuration;
	private float timer;

	private Vector3 randomMoveDir;
	private Vector3 randomMovePos;

	private float lastAttackTime = 0;//上次攻击的时刻，限制连续两轮攻击的时间间隔

	private ActorBase attackTarget;
	private ActorBase moveToTarget;

	private float attackInterval = 1;
	private float moveMaxDuratoin = 5f;
	private float idlMaxDuration = 5f;
	private float patrolRadius = 30;
	private int idleRandom = 30;
	private int moveRandom = 70;
	private float attackPrepairDuration = 0.8f;

	public void StartAI(ActorBase actor)
	{
		this.actor = actor;
		ChangeStatus(EmMyStatus.Idle);
	}

	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if (actor.IsBeAttacked())
		{
			ChangeStatus(EmMyStatus.BeAttacked);
			return;
		}

		if (actor.IsDead() || actor.IsDying())
		{
			return;
		}

		switch (curStatus)
		{
			case EmMyStatus.Idle:
				if (timer >= statusDuration)
				{
					int r = Random.Range(0, 101);
					if (r < idleRandom)
					{ }
					else if (r < idleRandom + moveRandom)
						ChangeStatus(EmMyStatus.RandomMoveDirection);
				}

				if (Patrol()) MoveToTarget();

				break;
			case EmMyStatus.RandomMoveDirection:
			case EmMyStatus.RandomMoveDirectionAfterAttack:
				actor.WalkForward(0);
				if (timer >= statusDuration)
					ChangeStatus(EmMyStatus.Idle);

				if (curStatus == EmMyStatus.RandomMoveDirection)
					if (Patrol()) MoveToTarget();
				break;
			case EmMyStatus.MoveToTarget:
				actor.LookAt(attackTarget.transform.position);
				actor.WalkForward(0);
				if (Vector3.SqrMagnitude(actor.transform.position - moveToTarget.transform.position) < actor.actorConf.attackDistance * actor.actorConf.attackDistance)
				{
					ChangeStatus(EmMyStatus.AttackPrepair);
				}
				break;

			case EmMyStatus.AttackPrepair:
				if (timer >= attackPrepairDuration)
				{
					if (attackTarget != null && attackTarget.IsAlive() &&
					    (actor.transform.position - moveToTarget.transform.position).sqrMagnitude <= actor.actorConf.attackDistance * actor.actorConf.attackDistance)
						DoAttack();
					else
						ChangeStatus(EmMyStatus.Idle);
				}

				break;

			case EmMyStatus.NormalAttack:
				if (actor.IsIdle())
					ChangeStatus(EmMyStatus.Idle);
				break;

			case EmMyStatus.BeAttacked:
				if (actor.IsIdle())
					ChangeStatus(EmMyStatus.Idle);
				break;
		}
	}

	void ChangeStatus(EmMyStatus newStatus)
	{
		timer = 0f;
		curStatus = newStatus;
		switch (newStatus)
		{
			case EmMyStatus.Idle:
				actor.Idle();
				statusDuration = Random.Range(0, idlMaxDuration);
				break;

			case EmMyStatus.RandomMoveDirection:
			case EmMyStatus.RandomMoveDirectionAfterAttack:
				Vector3[] dirs = new Vector3[]
				{
					new Vector3(0, 0, 1),
					new Vector3(1, 0, 1),
					new Vector3(1, 0, 0),
					new Vector3(1, 0, -1),
					new Vector3(0, 0, -1),
					new Vector3(-1, 0, -1),
					new Vector3(-1, 0, 0),
					new Vector3(-1, 0, 1),
				};
				randomMoveDir = dirs[Random.Range(0, 8)];
				randomMovePos = actor.transform.position + randomMoveDir * 3;
				actor.LookAt(randomMovePos);
				statusDuration = moveMaxDuratoin;
				break;

			case EmMyStatus.MoveToTarget:
				actor.LookAt(attackTarget.transform.position);
				break;

			case EmMyStatus.AttackPrepair:
				actor.Idle();
				statusDuration = attackPrepairDuration;
				break;

			case EmMyStatus.NormalAttack:
				actor.Attack();
				break;

			case EmMyStatus.BeAttacked:
				break;
		}
	}

	void MoveToTarget()
	{
		//找最近的敌人，移动过去进行攻击
		ActorBase actor = GetNearestAttackTarget();
		if (actor != null)
		{
			attackTarget = actor;
			moveToTarget = actor;
			ChangeStatus(EmMyStatus.MoveToTarget);
		}
		else
			ChangeStatus(EmMyStatus.Idle);
	}

	void DoAttack()
	{
		ChangeStatus(EmMyStatus.NormalAttack);
	}


	//巡逻，发现敌人返回true，否则返回false
	bool Patrol()
	{
		if (Time.realtimeSinceStartup - lastAttackTime < attackInterval)
			return false;

		List<ActorBase> enemyList = GameMain.instance.GetEnemyList(actor);
		for (int i = 0; i < enemyList.Count; i++)
		{
			if (!enemyList[i].IsAlive()) continue;
			float d = Vector3.SqrMagnitude(actor.transform.position - enemyList[i].transform.position);
			if (d < patrolRadius * patrolRadius)
			{
				return true;
			}
		}
		return false;
	}

	ActorBase GetNearestAttackTarget()
	{
		List<ActorBase> enemyList = GameMain.instance.GetEnemyList(actor);
		if (enemyList == null)
			return null;

		float minSqrDist = float.MaxValue;
		ActorBase minDistActor = null;
		for (int i = 0; i < enemyList.Count; i++)
		{
			if (!enemyList[i].IsAlive()) continue;
			float d = Vector3.SqrMagnitude(actor.transform.position - enemyList[i].transform.position);
			if (d < minSqrDist)
			{
				minSqrDist = d;
				minDistActor = enemyList[i];
			}
		}
		return minDistActor;
	}
}