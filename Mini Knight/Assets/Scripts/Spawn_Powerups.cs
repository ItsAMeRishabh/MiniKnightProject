using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_Powerups : MonoBehaviour
{
    public static Spawn_Powerups instance;
    [SerializeField]
    private GameObject Swordpowerups;
    public List<Transform> spawnPoints;
    public int SowrdCount;

    void Start()
    {
        instance = this;
         
    }
    private void Update() {
         if (SowrdCount < 3)
          {
              StartCoroutine(PowerupSpawnRoutine());
          }
    }

    IEnumerator PowerupSpawnRoutine()
    {
 
                float spawnPoint = Random.Range(0, spawnPoints.Count);
                Instantiate(Swordpowerups, spawnPoints[(int)spawnPoint].position, Quaternion.identity);
                SowrdCount++;
                yield return new WaitForSeconds(5.0f);

    }
}
