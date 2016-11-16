/*
 *  Script that dictates behaviour of entity movement
 *  This script is used for both Decoys and Humans, powerup givers could
 *  also possibly use this.
 *  Since all input, AI and human, goes through this, we could somehow add pausing
 *  by editing the update method
 * 
 *  V1.0 By Danny Reilman <reilman@umich.edu>	
 */

using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	public enum ControlTypeEnum
	{
		None,
		Human,
		Decoy
	}
	public ControlTypeEnum initControlType;
	public int initPlayerNum = 0;

	public int speed;

	//Internal values to hold control style and color/player color
	private EntityController control;
	private int playerNum;


	void Awake () {
		SetController(initControlType, playerNum);
	}
	
	void Update () {
		Vector2 input = control.GetInput(Time.deltaTime, this);

		//Super basic linear movement
		transform.position = new Vector3(transform.position.x + input.x,
										transform.position.y + input.y,
										transform.position.z);
	}

	public void SetController(ControlTypeEnum controlType, int playerNum_in)
	{
		playerNum = playerNum_in;
		switch(controlType)
		{
			case ControlTypeEnum.None:
				control = new NoControl();
				control.Awake();
				break;
			case ControlTypeEnum.Human:
				PlayerControl newControl = new PlayerControl();
				newControl.ID = playerNum;
				control = newControl;
				control.Awake();
				break;
			case ControlTypeEnum.Decoy:
				Debug.Log("NOT IMPLEMENTED YET");
				break;
		}

	}
}
