/* 
 * Immediatly sets DontDestroyOnLoad for the attached object and makes it a Singleton
 * (Ensures there is only one of this script in the scene at any time)
 * By Danny Reilman <reilman@umich.edu>
 */

using UnityEngine;
using System.Collections;
using find_real;

namespace find_real
{
	public class Singleton : MonoBehaviour {
		public static Singleton inst = null;
		public static GlobalValues globalValues = null;

		public GlobalValues globalValuesInst;

		void Awake () {
			if(inst == null)
			{
				inst = this;
				globalValues = globalValuesInst;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}