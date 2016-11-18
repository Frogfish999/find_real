using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class ConstantNormalizedMovement : EntityMovementStyle {
		const double EPSILON = 0.0001;
		private Vector2 lastInput = new Vector2(0,0);
		
		public void Move(Vector2 input, Rigidbody2D body)
		{
			Vector2 normalizedInput = input;
			normalizedInput.Normalize();
			
			//Update held direction if a direction is held
			if(Mathf.Abs(normalizedInput.magnitude - 1) < EPSILON)
			{
				lastInput = normalizedInput;
			}

			body.AddForce(lastInput * Singleton.globalValues.entitySpeed);
		}
	}
}