using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    private int playerMaxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = playerMaxHealth;
    }
    
    public void UpdatePlayerHealth(int amountOfHealth)
    {
        health += amountOfHealth;
        if(health <= 0)
        {
            health = 0;
            Debug.Log("Player Health less than 1 (YOU ARE DEAD)");

            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }
}
