using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;
    private float invincCounter;
    public float invincibleLength;
    private int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.sliderHealth.maxValue = maxHealth;
        UIController.instance.sliderHealth.value = currentHealth;
        UIController.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        } 
    }

    public void DamagePlayer(int damageAmount)
    {
        if(invincCounter <= 0) {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;
                GameManager.instance.PlayerDied();
            }

            invincCounter = invincibleLength;
            UIController.instance.sliderHealth.value = currentHealth;
            UIController.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
