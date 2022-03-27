using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    Vector2 respawnPoint;
    
    void Start()
    {
        respawnPoint = transform.position;
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }

        else if(other.gameObject.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
        
    }
}
