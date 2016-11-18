/*
 * This interface defines a movement style for the entities
 * Intended to be implemented by standard and constant
 *
 * V1.0 By Danny Reilman <reilman@umich.edu>
 */

using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public interface EntityMovementStyle {
		void Move(Vector2 input, Rigidbody2D body);
	}
}