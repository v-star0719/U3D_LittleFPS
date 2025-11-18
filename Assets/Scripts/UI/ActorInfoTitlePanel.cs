using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInfoTitlePanel : MonoBehaviour
{
	public ActorInfoTitleCtrl firstItem;
	public LinkedList<ActorInfoTitleCtrl> aliveList = new LinkedList<ActorInfoTitleCtrl>();
	public LinkedList<ActorInfoTitleCtrl> deadList = new LinkedList<ActorInfoTitleCtrl>();

	void Awake()
	{
		deadList.AddLast(firstItem);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public ActorInfoTitleCtrl GetTitleCtrl()
	{
		if (deadList.Count > 0)
		{
			LinkedListNode<ActorInfoTitleCtrl> node = deadList.First;
			deadList.RemoveFirst();
			aliveList.AddLast(node);
			node.Value.gameObject.SetActive(true);
			return node.Value;
		}

		ActorInfoTitleCtrl newItem = Instantiate(aliveList.First.Value);
		newItem.transform.parent = transform;
		newItem.transform.localScale = Vector3.one;
		newItem.transform.localRotation = Quaternion.identity;

		aliveList.AddLast(newItem);
		//Debug.LogFormat("create ActorInfoTitleCtrl, alive = {0}, dead = {1}", aliveList.Count,deadList.Count);
		return newItem;
	}

	public void Recycle(ActorInfoTitleCtrl item)
	{
		item.gameObject.SetActive(false);
		LinkedListNode<ActorInfoTitleCtrl> node = aliveList.Find(item);
		aliveList.Remove(node);
		deadList.AddLast(node);
		//Debug.LogFormat("ActorInfoTitleCtrl, alive = {0}, dead = {1}" ,aliveList.Count, deadList.Count);
	}

}
