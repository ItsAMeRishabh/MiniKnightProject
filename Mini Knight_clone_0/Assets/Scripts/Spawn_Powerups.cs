using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_Powerups : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerups;
    public List<Transform> spawnPoints;

    void Start()
    {
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(true)
        {
            int randomPowerup = Random.Range(0,powerups.Length);
            float spawnPoint = Random.Range(0,spawnPoints.Count);
            Instantiate(powerups[randomPowerup] , spawnPoints[(int)spawnPoint].position,Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
