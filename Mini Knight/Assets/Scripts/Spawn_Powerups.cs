using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn_Powerups : MonoBehaviour
{
    public static Spawn_Powerups instance;
    [SerializeField]
    private GameObject Swordpowerups;
    [SerializeField]
    private GameObject Health;
    public List<Transform> HealthSpawnPoint;
    public List<Transform> spawnPoints;
    public int SowrdCount;
    public int HealthCount;

    void Start()
    {
        instance = this;

    }
    private void Update()
    {
        if (SowrdCount < 3)
        {
            StartCoroutine(PowerupSpawnRoutine());
        }

        if(HealthCount<2)
        {
            StartCoroutine(HeartSpawn());
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {

        float spawnPoint = Random.Range(0, spawnPoints.Count);
        Instantiate(Swordpowerups, spawnPoints[(int)spawnPoint].position, Quaternion.identity);
        SowrdCount++;
        yield return new WaitForSeconds(5.0f);

    }

    IEnumerator HeartSpawn()
    {
        float spawnPoint= Random.Range(0,HealthSpawnPoint.Count);
        PhotonNetwork.Instantiate("HealthPowerup",HealthSpawnPoint[(int)spawnPoint].position,Quaternion.identity);
        HealthCount++;
        yield return new WaitForSeconds(2.0f);
    }
}
