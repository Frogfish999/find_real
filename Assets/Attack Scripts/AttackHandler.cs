using UnityEngine;
using System.Collections;

namespace find_real
{
	public class AttackHandler : MonoBehaviour {

		public Collider2D hitbox;

		//Checks if the attack hit a player
		public void Attack(Vector2 direction, int playerNum)
		{
			if(direction.magnitude > .9)
			{
				hitbox.enabled = true;
				hitbox.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90,Vector3.forward);

				LayerMask[] layers = new LayerMask[3];
				
				int c = 0;
				for(int i = 1; i <= 4; ++i)
				{
					if(i != playerNum)
					{
						layers[c] = LayerMask.NameToLayer("Player" + i.ToString());
						Debug.Log("Checking layer: " + i.ToString());
						++c;
					}
				}

				StartCoroutine(RemoveHitboxAfterOneFrame(playerNum, layers[0] | layers[1] | layers[2]));
			}
		}

		private IEnumerator RemoveHitboxAfterOneFrame(int playerNum, LayerMask layers)
		{
			yield return null;
			bool quiet = hitbox.IsTouchingLayers(layers);
				
			if(!quiet)
			{
				Debug.Log("MISS");
			}
			yield return null;
			hitbox.enabled = false;
		}
	}
}