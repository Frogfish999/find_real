using UnityEngine;
using System.Collections;

namespace find_real
{
	public class HurtboxHandler : MonoBehaviour {
		public Collider2D hurtbox;
		public int CheckForHit (int playerNum) {
			if (hurtbox.IsTouchingLayers(Singleton.globalValues.damageMasks[0]))
			{
				return 0;
			}

			for(int i = 1; i <= 4; ++i)
			{
				if(playerNum != i && hurtbox.IsTouchingLayers(Singleton.globalValues.damageMasks[i]))
				{
					return i;
				}
			}
			
			return -1;
		}
	}
}
