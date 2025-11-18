using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCtrl : MonoBehaviour
{
	public GameObject[] spriteArray;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Set(int n)
	{
		for (int i = 0; i < spriteArray.Length; i++)
			spriteArray[i].SetActive(i < n);
	}
}
