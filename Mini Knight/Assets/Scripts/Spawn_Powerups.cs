using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_Powerups : MonoBehaviour
{
    public static Spawn_Powerups Powerup_instance;
    public int powerup_count;
    [SerializeField]
    private GameObject[] powerups;
    public List<Transform> sowrdSpawnPoints;
   // public List<Transform> healthSpawnPoints;

    void Start()
    {
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(true)
        {
            if(powerup_count <=3)
            {
                int randomPowerup = Random.Range(0,powerups.Length);
                float spawnPoint = Random.Range(0,sowrdSpawnPoints.Count);
                Instantiate(powerups[randomPowerup] , sowrdSpawnPoints[(int)spawnPoint].position,Quaternion.identity);
                powerup_count++;
                yield return new WaitForSeconds(5.0f);   
            }

        }
    }
}
