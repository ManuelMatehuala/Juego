using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    public static EnemyLifeController instance;

    public int maxHealth, currentHealth;
    private float invincCounter;
    public float invincibleLength;
    private int score = 0;
    public GameObject prefabEnemy;
    public float life = 100f;
    //public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.sliderHealthEnemy.maxValue = maxHealth;
        UIController.instance.sliderHealthEnemy.value = currentHealth;
        UIController.instance.textHealthenemy.text = "Vida: " + currentHealth + "/" + maxHealth;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                DestruirObjetosPorTag();
                Instantiate(prefabEnemy, new Vector3(-53.21f, 6.615827f, -9.53f), transform.rotation);
                Destroy(gameObject);
                //enemyThreeController.activeRoutine();
                //animator.SetFloat("Life", 100f);

                currentHealth = maxHealth;
            }
            invincCounter = invincibleLength;
            UIController.instance.sliderHealthEnemy.value = currentHealth;
            UIController.instance.textHealthenemy.text = "Vida: " + currentHealth + "/" + maxHealth;
        }
    }
    void DestruirObjetosPorTag()
    {
        GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag("ProyectilEnemy");

        foreach (GameObject objeto in objetosConTag)
        {
            Destroy(objeto);
        }
    }
    public int GetScore()
    {
        return score;
    }

}
