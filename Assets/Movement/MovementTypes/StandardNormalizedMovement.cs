using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class StandardNormalizedMovement : EntityMovementStyle {
		public void Move(Vector2 input, Rigidbody2D body)
		{
			Vector2 normalizedInput = input;
			normalizedInput.Normalize();
			body.AddForce(normalizedInput * Singleton.globalValues.entitySpeed);
		}
	}
}