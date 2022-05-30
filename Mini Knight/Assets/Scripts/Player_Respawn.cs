using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    Vector2 respawnPoint;

    private float respawnCountdown;
    public float respawnCooldown=5;

    void Start()
    {
        respawnPoint = transform.position;
        respawnCountdown = respawnCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "FallDetector")
        {
            //respawnCountdown = respawnCooldown;
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

    public void DeathTicker()
    {
        if (respawnCountdown <= 0)
        {
            CharacterContorller.instanceController.playerDisabled = false;
        }
        else
        {
            CharacterContorller.instanceController.playerDisabled = true;
            respawnCountdown -= Time.deltaTime;
        }
    }
}
