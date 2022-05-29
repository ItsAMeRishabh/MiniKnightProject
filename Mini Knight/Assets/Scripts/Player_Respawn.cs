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
            if (HealthBarScript.instance_health.currentHealth > 75)
            {
                HealthBarScript.instance_health.currentHealth -= 75;
            }
            else if (HealthBarScript.instance_health.currentHealth <= 75)
            {
                HealthBarScript.instance_health.currentHealth = 100;
                HealthBarScript.instance_health.currentHearts -= 1;
                HealthBarScript.instance_health.UpdateHearts();
            }
        }

        else if(other.gameObject.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }

        if (other.gameObject.tag == "Spikes")
        {
            CharacterContorller.instanceController.anim.SetBool("TakeDamage",true);
            if (HealthBarScript.instance_health.currentHealth > 10)
            {
                HealthBarScript.instance_health.currentHealth -= 10;
            }
            else if (HealthBarScript.instance_health.currentHealth <= 10)
            {
                HealthBarScript.instance_health.currentHealth = 100;
                HealthBarScript.instance_health.currentHearts -= 1;
                HealthBarScript.instance_health.UpdateHearts();
            }
        }
        else
        {
            CharacterContorller.instanceController.anim.SetBool("TakeDamage",false);
        }
    }

}
