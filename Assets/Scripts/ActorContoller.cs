using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorContoller : MonoBehaviour
{
	private const float MOUSE_SENSITIVITY = 50f;
	private const float FIRE_INVERVAL = 0.3f;

	public bool enableLookAround = false;

	private ActorBase playerActor;
	private Camera playerCamera;

	private float preFireTime;

	public void Init(ActorBase playerActor, Camera playerCamera)
	{
		this.playerActor = playerActor;
		this.playerCamera = playerCamera;
	}
	public void StartWork()
	{
	}


	// Update is called once per frame
	void Update ()
	{
		if (!playerActor.IsAlive()) return;
		if (playerActor.IsBeAttacked()) return;
		if (playerActor.IsJumping()) return;
		if (playerActor.IsTossGrenade()) return;

		//视角偏转
		float rotaionDeltaY = Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY * Time.deltaTime;
		float rotaionDeltaX = Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY * Time.deltaTime;

		if (enableLookAround)
			playerCamera.transform.localEulerAngles += new Vector3(rotaionDeltaX, 0, 0);
		playerActor.Pitch(-rotaionDeltaX);

		//射击
		if (Input.GetAxis("Fire1") > 0 && Time.timeSinceLevelLoad - preFireTime >= FIRE_INVERVAL)
		{
			preFireTime = Time.timeSinceLevelLoad;
			playerActor.Attack();
			return;
		}

		//扔手榴弹
		if (Input.GetAxis("Fire2") > 0 && !playerActor.IsTossGrenade())
		{
			preFireTime = Time.timeSinceLevelLoad;
			playerActor.TossGrenade();
			return;
		}

		float y = Input.GetAxis("Vertical");
		float x = Input.GetAxis("Horizontal");
		//跳
		if (Input.GetAxis("Jump") > 0 && !playerActor.IsJumping())//
		{
			playerActor.Jump(x, 1, y);
			return;
		}

		//前后
		if (y > 0)
		{
			playerActor.WalkForward(rotaionDeltaY);
			return;
		}
		else if (y < 0)
		{
			playerActor.WalkBackward(rotaionDeltaY);
			return;
		}
		
		//左后
		//如果没有前后移动，再去处理水平移动
		if (y != 0) x = 0;

		if (x > 0)
		{
			playerActor.WalkRight(rotaionDeltaY);
			return;
		}
		else if (x < 0)
		{
			playerActor.WalkLeft(rotaionDeltaY);
			return;
		}

		//转向
		if (!playerActor.IsWalking() && !playerActor.IsJumping())
		{
			if (rotaionDeltaY > 0)
			{
				playerActor.TrunRight(rotaionDeltaY);
			}
			else if(rotaionDeltaY < 0)
			{
				playerActor.TrunLeft(rotaionDeltaY);
			}
			return;
		}

		//如果上面一个都没有，就待机
		if (!playerActor.IsJumping() && !playerActor.IsAttacking())
		{
			playerActor.Idle();
		}
	}
}
