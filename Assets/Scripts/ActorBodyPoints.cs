using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBodyPoints : MonoBehaviour
{
	public Transform gunPos;
	public Transform bulletRoot;
	public Transform bulletOriginPos;
	public Transform bulletDirPos;
	public Transform grenadeStartPos;
	public Transform spinePos;

	private int mark = 0;

	[HideInInspector]
	public Transform runtimeSpinePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (mark < 10)
		{
			mark++;
			return;
		}

		if (runtimeSpinePos == null)
		{
			GameObject go = new GameObject(spinePos.name + " new");
			runtimeSpinePos = go.transform;
			runtimeSpinePos.transform.parent = spinePos.parent;
			runtimeSpinePos.localRotation = Quaternion.identity;
			runtimeSpinePos.localScale = Vector3.one;
			runtimeSpinePos.localPosition = Vector3.zero;
			spinePos.parent = runtimeSpinePos;
		}
	}
}
