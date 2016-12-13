/*
 *	This script defines the behaviour of Players
 *	Players grab input based on the passed ID 
 *  (NOTE: Rewired input manager must already contain the player by that ID) 
 *	This script is to be passed into an entity 
 *
 *	V1.0 By Danny Reilman <reilman@umich.edu>	
 */
 
using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class PlayerControl : EntityController {
		//The ID that this Player will use in awake to grab the Rewired player
		public int ID;
		
		//The player that input comes from
		private Rewired.Player player;
		private bool active;

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
	}
}