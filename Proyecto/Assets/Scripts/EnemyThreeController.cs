
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyThreeController : MonoBehaviour
{

    public static EnemyThreeController instance;
    public Animator animator;
    public Transform player;
    public Transform enemy;
    private int maxHealth = 100; 
    private int currentHealth=100;
    public float movementSpeed = 10f;
    public float movementRange = -3f;
    public float fireRate = 200f;
    public GameObject bulletPrefab;
    public float velocidad = 10f;  // Puedes ajustar la velocidad según tus necesidades
    private float invincCounter;
    public float invincibleLength;
    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();
        sliderStart();
        animator.SetBool("Active", true);
        activeRoutine();
    }

    void Update()
    {

    }
    public void activeRoutine()
    {
        StartCoroutine(SpawnProyectiles());

    }
    IEnumerator SpawnProyectiles()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab,new Vector3(transform.position.x+3, transform.position.y, transform.position.z), Quaternion.identity);
            Vector3 direccion = transform.forward;
            bullet.GetComponent<Rigidbody>().velocity = direccion * velocidad;
            yield return new WaitForSeconds(1f);
        }
    }

    public void sliderStart()
    {
        currentHealth = maxHealth;
        UIController.instance.sliderHealthEnemy.maxValue = maxHealth;
        UIController.instance.sliderHealthEnemy.value = currentHealth;
        UIController.instance.textHealthenemy.text = "Vida: " + currentHealth + "/" + maxHealth;
    }
    public void DamagePlayerThree(int damageAmount)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                DestruirObjetosPorTag();
                // Destruye el objeto después de que termine la animación
                SceneManager.LoadScene("Victory");
            }
            invincCounter = invincibleLength;
            UIController.instance.sliderHealthEnemy.value = currentHealth;
            UIController.instance.textHealthenemy.text = "Vida: " + currentHealth + "/" + maxHealth;
        }
    }

    void DestruirObjetosPorTag()
    {
        GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag("ProyectilEnemy3");

        foreach (GameObject objeto in objetosConTag)
        {
            Destroy(objeto);
        }
    }

}

