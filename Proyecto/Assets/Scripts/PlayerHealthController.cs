using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;
    private float invincCounter;
    public float invincibleLength;
    private int score = 0;
    public bool escudo = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test")
        {
            UIController.instance.sliderHealth.maxValue = maxHealth;
            UIController.instance.sliderHealth.value = currentHealth;
            UIController.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
        }
        else if (currentSceneName == "Level2")
        {
            UIControllerLevel2.instance.sliderHealth.maxValue = maxHealth;
            UIControllerLevel2.instance.sliderHealth.value = currentHealth;
            UIControllerLevel2.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
        }

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
        if (!escudo)
        {
            if (invincCounter <= 0)
            {
                currentHealth -= damageAmount;

                if (currentHealth > 100) {
                    currentHealth = 100;
                }

                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                    currentHealth = 0;
                    GameManager.instance.PlayerDied();
                }

                invincCounter = invincibleLength;
                string currentSceneName = SceneManager.GetActiveScene().name;
                if (currentSceneName == "Test")
                {
                    UIController.instance.sliderHealth.value = currentHealth;
                    UIController.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
                }
                else if (currentSceneName == "Level2")
                {
                    UIControllerLevel2.instance.sliderHealth.value = currentHealth;
                    UIControllerLevel2.instance.textHealth.text = "Vida: " + currentHealth + "/" + maxHealth;
                }

            }
        }
    }

    public void rutinaEscudo() {
        escudo = true;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test")
        {
            UIController.instance.EscudoText.text = "ESCUDO";
        }
        else if (currentSceneName == "Level2")
        {
            UIControllerLevel2.instance.EscudoText.text = "ESCUDO";
        }

        StartCoroutine(getEscudo());
    }

    public IEnumerator getEscudo()
    {
        yield return new WaitForSeconds(10);
        escudo = false;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test")
        {
            UIController.instance.EscudoText.text = "";
        }
        else if (currentSceneName == "Level2")
        {
            UIControllerLevel2.instance.EscudoText.text = "";
        }
    }
    public int GetScore()
    {
        return score;
    }
}
