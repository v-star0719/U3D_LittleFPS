using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameMain : MonoBehaviour
{
	public const int MAX_ENEMY_COUNT = 20;
	public const int MIN_ENEMY_COUNT = 10;
	public const int MAP_WIDTH = 100;
	public const int MAP_LENGTH = 100;

	public static GameMain instance;

	public GameObject EnemyPrefab;
	public ActorBase PlayerActor;
	public ActorContoller ActorController;
	public Camera PlayerCamera;

	public List<ActorBase> enemyList = new List<ActorBase>();

	public Camera uiCamera;
	public int CurWeaponId = 0;
	public int AliveEnemyCount = 0;

	void Awake()
	{
		instance = this;
	}

	void OnDestroy()
	{
		instance = null;
	}

	// Use this for initialization
	void Start ()
	{
		CurWeaponId = 1;
		Restart();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		PlayerActor.transform.position = Vector3.zero;
		PlayerActor.Init(1, EmActorType.Player, CurWeaponId);
		PlayerActor.StartWork();
		ActorController.Init(PlayerActor, PlayerCamera);
		ActorController.StartWork();
		CreateEnemy();
	}

	void CreateEnemy()
	{
		int count = Random.Range(MIN_ENEMY_COUNT, MAX_ENEMY_COUNT);

		for (int i = 0; i < count; i++)
		{
			Vector3 pos = Vector3.zero;
			pos.x = Random.Range(0, MAP_WIDTH) - MAP_WIDTH * 0.5f;
			pos.z = Random.Range(0, MAP_LENGTH) - MAP_LENGTH * 0.5f;

			ActorBase actorBase = null;
			EnemyAI ai = null;
			if (i >= enemyList.Count)
			{
				GameObject go = GameObject.Instantiate(EnemyPrefab);
				go.transform.position = pos;
				actorBase = go.AddComponent<ActorBase>();
				ai = go.AddComponent<EnemyAI>();
				enemyList.Add(actorBase);
			}
			else
			{
				actorBase = enemyList[i];
				ai = enemyList[i].GetComponent<EnemyAI>();
			}

			actorBase.gameObject.SetActive(true);
			actorBase.Init(2, EmActorType.Monster, -1);
			actorBase.StartWork();
			ai.StartAI(actorBase);
		}

		while (enemyList.Count > count)
		{
			Destroy(enemyList[enemyList.Count - 1].gameObject);
			enemyList.RemoveAt(enemyList.Count - 1);
		}

		AliveEnemyCount = count;
		UIManager.instance.mainUIPanel.SetEnemyCount(count, count);
	}

	public List<ActorBase> GetEnemyList(ActorBase attacker)
	{
		List<ActorBase> list = new List<ActorBase>();
		if (attacker.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			list.AddRange(enemyList);
		}
		else if(attacker.gameObject.layer == LayerMask.NameToLayer("Monster"))
		{
			list.Add(PlayerActor);
		}

		return list;
	}

	public void OnEnemyDead()
	{
		AliveEnemyCount--;
		UIManager.instance.mainUIPanel.SetEnemyCount(enemyList.Count, AliveEnemyCount);

		if(AliveEnemyCount == 0)
			UIManager.instance.gameOverPanel.Open(true);
	}

	public void OnPlayerDead()
	{
		UIManager.instance.gameOverPanel.Open(false);
	}

	public static bool IsInMap(Vector3 pos)
	{
		return Mathf.Abs(pos.x) > MAP_WIDTH * 0.5f || Mathf.Abs(pos.z) > MAP_LENGTH * 0.5f;
	}
}
