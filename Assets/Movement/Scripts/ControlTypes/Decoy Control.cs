/*
 *  Currently this just moves right for testing
 *  
 *  This script will eventually define decoy behaviour
 *  Decoys will somehow evaluate their surroundings and move
 *  or perhaps move randomly (TBD)
 *
 *  V0.1 by Danny Reilman <reilman@umich.edu>
 */
using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class DecoyControl : EntityController {

		public void Awake() { }
		
		public Vector2 GetInput(float deltaT, Entity entity)
		{
			return Vector2.right;
		}
	}
}
