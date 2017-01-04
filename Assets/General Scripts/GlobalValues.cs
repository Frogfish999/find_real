using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class GlobalValues : MonoBehaviour {
		public int entitySpeed;
		public Entity.MovementStyle movementStyle;
		public Color[] playerColors;
		public float maxTime;
		[Range(0,1)]
		public float stopOdds;
		public float centerBias;

		public LayerMask[] damageMasks;

		public int invulnFrames;

	}
}
