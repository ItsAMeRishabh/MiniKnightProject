using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    Vector2 respawnPoint;

    private float respawnCountdown;
    public float respawnCooldown;

    private bool isDead;

    void Start()
    {
        respawnPoint = transform.position;
        respawnCooldown = 3f;
        respawnCountdown = respawnCooldown;
        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "FallDetector")
        {
            isDead = true;
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

        
    }

    private void Update()
    {
        DeathTicker();
        if(isDead)
        {
            Invoke("SetMaterial", 0.2f);
            Invoke("ResetMaterial", 0.4f);
        }

    }

    public void DeathTicker()
    {
        if(isDead)
        {
            if(respawnCountdown <=0)
            {

                isDead = false;
                respawnCountdown = respawnCooldown;
                CharacterContorller.instanceController.playerDisabled = false;

                //CharacterContorller.instanceController.sp.material = CharacterContorller.instanceController.matDefault;
                ResetMaterial();
            }
            else
            {
                //CharacterContorller.instanceController.sp.material = CharacterContorller.instanceController.whiteMat;
                
                CharacterContorller.instanceController.playerDisabled = true;
                respawnCountdown -= Time.deltaTime;

            }
        }
        
    }
    void SetMaterial()
    {
        CharacterContorller.instanceController.sp.material = CharacterContorller.instanceController.whiteMat;
    }
    void ResetMaterial()
    {
        CharacterContorller.instanceController.sp.material = CharacterContorller.instanceController.matDefault;
    }
}
