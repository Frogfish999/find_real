/*
 *  This interface defines objects that can control an Entity
 *  Intended to be implemented by Player and Decoy
 *
 *  V1.0 By Danny Reilman <reilman@umich.edu>	
 */

using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public interface EntityController
	{
		//Called for initialization when the entity is created
		void Awake();

		//Returns the Vector2 input
		//deltaT should not be used to calculate speed,
		//I put deltaT in so that decoys could implement behaviour as a function
		//of passed time
		Vector2 GetInput(float deltaT, Entity entity);
	}
}
