using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDurabilityPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            AimCharacter.aim_instance.GunDurability = 3;
        }   
    }
}
