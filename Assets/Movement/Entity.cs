/*
 *  Script that dictates behaviour of entity movement
 *  This script is used for both Decoys and Humans, powerup givers could
 *  also possibly use this.
 *  Since all input, AI and human, goes through this, we could somehow add pausing
 *  by editing the update method
 * 
 *  V1.0 By Danny Reilman <reilman@umich.edu>
 *  V2.0 Added public variables for testing	
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
		
		public SpriteRenderer sprite;
		public AttackHandler attacker;
		public HurtboxHandler hurtbox;

		public ControlTypeEnum initControlType;
		public int initPlayerNum = 0;
		
		public int playerNum;

		//values to hold control style, color/player color, and movementStyle
		private EntityController control;
		private EntityMovementStyle moveStyle;

		private bool invunerable = false;

		//Intentionally Start instead of awake in order to be called after singleton awake
		void Start () {
			SetController(initControlType, initPlayerNum);
			SetMovementStyle(Singleton.globalValues.movementStyle);
		}
		
		void FixedUpdate () {
			Vector2 input = control.GetInput(Time.deltaTime, this);
			moveStyle.Move(input, GetComponentInChildren<Rigidbody2D>());
		}

		void Update()
		{
			if(control is PlayerControl)
			{
				var attackReturn = ((PlayerControl) control).GetAttackInput();

				if(attackReturn.attack)
				{
					Attack(attackReturn.direction);
				}
			}

			if(!invunerable)
			{
				int hitBy = hurtbox.CheckForHit(playerNum);

				if(hitBy != -1)
				{
					Debug.Log("Hit by damageType " + hitBy);
					StartCoroutine(InvunerableFrames());
				}
			}
		}

		private IEnumerator InvunerableFrames()
		{
			invunerable = true;
			for(int i = 0; i < Singleton.globalValues.invulnFrames; ++i)
			{
				yield return 0;
			}
			invunerable = false;
		}

		//Sort of a factory function to create and set a controlType with a given value
		public void SetController(ControlTypeEnum controlType, int playerNum_in)
		{
			playerNum = playerNum_in;
			switch(controlType)
			{
				case ControlTypeEnum.None:
					control = new NoControl();
					transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Decoy");
					break;
				case ControlTypeEnum.Human:
					PlayerControl newControl = new PlayerControl();
					newControl.ID = playerNum - 1;
					control = newControl;
					transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Player"+ (playerNum_in.ToString()));
					transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Player" + (playerNum_in.ToString())+ "Damage");
					break;
				case ControlTypeEnum.Decoy:
					control = new DecoyControl();
					transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Decoy"+ (playerNum_in.ToString()));
					transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Player" + (playerNum_in.ToString())+ "Damage");

					break;
			}
			control.Awake();
			
			sprite.color = Singleton.globalValues.playerColors[playerNum];
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

		//Attack given a specified direction
		private void Attack(Vector2 direction)
		{
			Debug.Log("Attacked at " + direction.x + ", " + direction.y);
			attacker.Attack(direction, playerNum);
		}
	}
}