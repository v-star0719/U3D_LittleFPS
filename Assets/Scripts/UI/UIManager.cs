using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	public MainUIPanel mainUIPanel;
	public ActorInfoTitlePanel actorInfoTitlePanel;
	public BagPanel bagPanel;
	public GameOverPanel gameOverPanel;

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
}
