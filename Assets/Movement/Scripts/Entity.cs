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
using find_real;

namespace find_real
{
	public class Entity : MonoBehaviour {
		public enum ControlTypeEnum
		{
			None,
			Human,
			Decoy
		}
		
		public enum MovementStyle
		{
			Standard,
			Constant
		}

		public ControlTypeEnum initControlType;
		public int initPlayerNum = 0;

		//Internal values to hold control style, color/player color, and movementStyle
		private EntityController control;
		private int playerNum;

		private EntityMovementStyle moveStyle;

		//Intentionally Start instead of awake in order to be called after singleton awake
		void Start () {
			SetController(initControlType, playerNum);
			SetMovementStyle(Singleton.globalValues.movementStyle);
		}
		
		void FixedUpdate () {
			Vector2 input = control.GetInput(Time.deltaTime, this);
			moveStyle.Move(input, GetComponent<Rigidbody2D>());
		}

		//Sort of a factory function to create and set a controlType with a given value
		public void SetController(ControlTypeEnum controlType, int playerNum_in)
		{
			playerNum = playerNum_in;
			switch(controlType)
			{
				case ControlTypeEnum.None:
					control = new NoControl();
					break;
				case ControlTypeEnum.Human:
					PlayerControl newControl = new PlayerControl();
					newControl.ID = playerNum;
					control = newControl;
					break;
				case ControlTypeEnum.Decoy:
					control = new DecoyControl();
					break;
			}
			control.Awake();

		}

		//Similar to above, sets the movementStyle
		public void SetMovementStyle(MovementStyle movementType)
		{
			switch(movementType)
			{
				case MovementStyle.Standard:
					moveStyle = new StandardNormalizedMovement();
					break;
				case MovementStyle.Constant:
					moveStyle = new ConstantNormalizedMovement();
					break;
			}
		}
	}
}