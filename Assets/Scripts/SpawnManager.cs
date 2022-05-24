using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const int xSpawnLocation = 10;
    private const int ySpawnLocation = 6;

    public float spawnTimer = 2f;
    public GameObject[] asteroidTypes;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnAsteroid()
    {
        while (true) {
            int[] spawnLines = { xSpawnLocation, -xSpawnLocation, ySpawnLocation, -ySpawnLocation };

            Vector2 spawnPoint;
            int spawnLine = spawnLines[Random.Range(0, spawnLines.Length)];

            /* 
             * Probably not the best way to select a spawn point.
             * This method depends on xSpawnLocationa and ySpawnLocation having different values
             * Hence it only works for rectangular spawn boxes. Won't work if the spawn box is a square.
             * 
             * NOTE: Code can also be cleaned up if/when Unity supports switch expressions with
             *       relational case statements (a or b => expression).
             */

            switch (spawnLine) {
            case xSpawnLocation:
            case -xSpawnLocation:
                spawnPoint = new Vector2(spawnLine, Random.Range(-ySpawnLocation, (float)ySpawnLocation));
                break;

            case ySpawnLocation:
            case -ySpawnLocation:
                spawnPoint = new Vector2(Random.Range(-xSpawnLocation, (float)xSpawnLocation), spawnLine);
                break;

            default:
                spawnPoint = new Vector2();
                break;
            }

            Instantiate(asteroidTypes[Random.Range(0, asteroidTypes.Length)], 
                spawnPoint, 
                Quaternion.identity);
            
            yield return new WaitForSeconds(spawnTimer);
        }
    }

}
