using UnityEngine;


/**
 * A system to proceduraly spawn in decoys for each player in an square area
 * Walls and entity must be on the layer "BlockSpawning"
 * 
 * V1.0 By Danny Reilman <reilman@umich.edu>
 */
namespace find_real
{
	public class Spawner : MonoBehaviour {

		public Rect testRect;
		//How many loops to go through before giving up on spawning a character
		private static int INFINITE_LOOP_GUARD = 100;

		//The entity to be spawned in
		public GameObject entity;
		
		//The distance (center of spawned object to edge of existing object) that must be clear
		//	for the entity to spawn
		public float paddingDistance;

		//Characters are temporarily stored so that the final result is always ballanced
		private GameObject[] spawnedCharacters;

		public LayerMask blockSpawningLayer;

		public int spawnNumber;

		/**
		* Spawn numEach entities for each of numPlayers in the spawnArea
		* Entities can push up to their padding circle being tangent with the spawn area edge, but no further
		* Returns true if all entities are spawned, false if not
		*/
		public bool Spawn(int numPlayers, int numEach, Rect spawnArea)
		{
			bool failed = false;

			spawnedCharacters = new GameObject[numPlayers];

			Rect correctedSpawnArea = new Rect(spawnArea);

			//If the rect would be inverted 
			if(correctedSpawnArea.xMin + paddingDistance > correctedSpawnArea.xMax - paddingDistance)
			{
				failed = true;
			}
			else
			{
				correctedSpawnArea.xMin += paddingDistance;
				correctedSpawnArea.xMax -= paddingDistance;
			}

			//If the rect would be inverted 
			if(correctedSpawnArea.yMin + paddingDistance > correctedSpawnArea.yMax - paddingDistance)
			{
				failed = true;
			}
			else
			{
				correctedSpawnArea.yMin += paddingDistance;
				correctedSpawnArea.yMax -= paddingDistance;
			}

			for(int i = 0; i < numEach && !failed; ++i)
			{
				for(int j = 1; j <= numPlayers && !failed; ++j)
				{
					if(!SpawnEntity(j, correctedSpawnArea))
					{
						//Delete gameObjects to ensure ballance
						for(int k = 0; k < 4 && spawnedCharacters[k] != null; ++k)
						{
							GameObject.Destroy(spawnedCharacters[k]);
						}
						
						failed = true;
					}
				}
				System.Array.Clear(spawnedCharacters, 0, spawnedCharacters.Length);
			}

			return !failed;
		}

		//Spawn a single entity in the spawn area
		//Returns true if an entity was spawned, false if too much time has passed
		private bool SpawnEntity(int playerNum, Rect correctedSpawnArea)
		{
			int iterations = 0;

			while(iterations < INFINITE_LOOP_GUARD)
			{
				Vector2 location = new Vector2(Random.Range(correctedSpawnArea.xMin, correctedSpawnArea.xMax), Random.Range(correctedSpawnArea.yMin, correctedSpawnArea.yMax));
				//Check for intersecting players
				Collider2D intersectingObject = Physics2D.OverlapCircle(location, paddingDistance, blockSpawningLayer);
				
				if(intersectingObject == null)
				{
					GameObject instance = (GameObject)GameObject.Instantiate(entity, location, Quaternion.identity);
					Entity entityScript = instance.GetComponentInChildren<Entity>();
					entityScript.initControlType = Entity.ControlTypeEnum.Decoy;
					entityScript.initPlayerNum = playerNum;
					entityScript.playerNum = playerNum;
					
					spawnedCharacters[playerNum-1] = instance;

					return true;
				}
				
				++iterations;
			}

			return false;
		}

		
		//Testing
		void Start()
		{
			Spawn(4,spawnNumber,testRect);
		}
	}
}
