
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyThreeController : MonoBehaviour
{

    public static EnemyThreeController instance;
    public Animator animator;
    public Transform player;
    public Transform enemy;
    private int maxHealthThree = 100; 
    private int currentHealthThree=100;
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
        maxHealthThree = 100;
        currentHealthThree = 100;
        animator = GetComponent<Animator>();
        sliderStart();
        animator.SetBool("Active", true);
        activeRoutine();
        Debug.Log(currentHealthThree);
        Debug.Log(maxHealthThree);
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
        currentHealthThree = maxHealthThree;
        UIControllerLevel2.instance.sliderHealthEnemy.maxValue = maxHealthThree;
        UIControllerLevel2.instance.sliderHealthEnemy.value = currentHealthThree;
        UIControllerLevel2.instance.textHealthenemy.text = "Vida: " + currentHealthThree + "/" + maxHealthThree;
    }
    public void DamagePlayerThree(int damageAmount)
    {
        Debug.Log(damageAmount);
        Debug.Log(currentHealthThree);
        if (invincCounter <= 0)
        {
            currentHealthThree -= damageAmount;

            if (currentHealthThree <= 0)
            {
                DestruirObjetosPorTag();
                // Destruye el objeto después de que termine la animación
                GameManager.instance.EndGame();
            }
            invincCounter = invincibleLength;
            UIControllerLevel2.instance.sliderHealthEnemy.value = currentHealthThree;
            UIControllerLevel2.instance.textHealthenemy.text = "Vida: " + currentHealthThree + "/" + maxHealthThree;
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

