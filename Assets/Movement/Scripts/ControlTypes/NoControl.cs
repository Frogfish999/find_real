/*
 *  This script does nothing, it just sits in the EntityController slot
 *  I thought we might need this, it could be useless though 
 * 
 *  V1.0 By Danny Reilman <reilman@umich.edu>	
 */
using UnityEngine;
using System.Collections;

public class NoControl : EntityController {
	public void Awake() {}
	public Vector2 GetInput(float deltaT, Entity entity) {return new Vector2(0,0);}
}
