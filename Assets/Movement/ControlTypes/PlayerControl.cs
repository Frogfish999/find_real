/*
 *	This script defines the behaviour of Players
 *	Players grab input based on the passed ID 
 *  (NOTE: Rewired input manager must already contain the player by that ID) 
 *	This script is to be passed into an entity 
 *
 *	V1.0 By Danny Reilman <reilman@umich.edu>	
 */
 
using UnityEngine;

namespace find_real
{
	public class PlayerControl : EntityController {
		public struct AttackOutput
		{
			public AttackOutput(bool attack_in, Vector2 direction_in)
			{
				attack = attack_in;
				direction = direction_in;
			}

			public bool attack;
			public Vector2 direction;
		};

		//The ID that this Player will use in awake to grab the Rewired player
		public int ID;
		
		//The player that input comes from
		private Rewired.Player player;
		private bool active = true;

		public void Awake () {
			player = Rewired.ReInput.players.GetPlayer(ID);
			if(player == null)
			{
				active = false;
			}
		}
		
		public Vector2 GetInput(float deltaT, Entity entity)
		{
			if(active)
			{
				return new Vector2(player.GetAxis("Move Horizontal"),
							player.GetAxis("Move Vertical"));
			}
			
			return new Vector2(0,0);
		}

		public AttackOutput GetAttackInput()
		{
			if(active)
			{
				if(player.GetButtonDown("Attack"))
				{
					return new AttackOutput(true, new Vector2(player.GetAxis("Aim Horizontal"), player.GetAxis("Aim Vertical")).normalized);
				}
			}
			return new AttackOutput(false, new Vector2(0,0));
		}
	}
}