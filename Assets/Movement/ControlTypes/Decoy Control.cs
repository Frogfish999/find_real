/*
 *  Currently this just moves right for testing
 *  
 *  This script will eventually define decoy behaviour
 *  Decoys will somehow evaluate their surroundings and move
 *  or perhaps move randomly (TBD)
 *
 *  V0.1 by Danny Reilman <reilman@umich.edu>
 *  V0.2 Changed to random so they didn't just fly off the screen
 */
using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class DecoyControl : EntityController {

		private float timePassed = 0;
		private float goalTime = 0;
		private Vector2 direction;

		private bool stopped;

		public void Awake() { }
		
		public Vector2 GetInput(float deltaT, Entity entity)
		{
			timePassed += deltaT;

			if(timePassed >= goalTime)
			{
				if(Random.Range(0f,1f) < Singleton.globalValues.stopOdds)
				{
					stopped = true;
				}
				else
				{
					stopped = false;
					goalTime = Random.Range(0f, Singleton.globalValues.maxTime);
					timePassed = 0;
					
					float phase = Random.Range(0f, Singleton.globalValues.centerBias);
					Vector2 centerVector = -1 * entity.transform.position;
					
					direction = (Random.insideUnitCircle.normalized + centerVector * phase).normalized;
				}
			}
			if(stopped)
			{
				return new Vector2(0,0);	
			}
			return direction;
		}
	}
}
