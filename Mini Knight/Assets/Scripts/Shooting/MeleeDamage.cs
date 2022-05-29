using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MeleeDamage : MonoBehaviour
{
    public Transform meleePoint;
    public float meleeRange = 0.5f;
    public LayerMask enemyMask;
    public PhotonView pview;
    private void Update()
    {
        MeleeCal();
    }

    public void MeleeCal()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyMask);

            foreach (Collider2D enemy in hitEnemies)
            {
                //if(!pview.IsMine)
                //{
                    pview.RPC("TakeDamage", RpcTarget.Others);
                //}
            }
        }
    }
    
    [PunRPC]
    public void TakeDamage()
    {
        if(HealthBarScript.instance_health.currentHealth>40)
        {
            HealthBarScript.instance_health.currentHealth -= 40;
            //HealthBarScript.instance_health.SetHealth();
        }
        else
        {
            HealthBarScript.instance_health.currentHealth = 100;
            //HealthBarScript.instance_health.SetHealth();
            HealthBarScript.instance_health.currentHearts -= 1;
            HealthBarScript.instance_health.UpdateHearts();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (meleePoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);
    }
}
