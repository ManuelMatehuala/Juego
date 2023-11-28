using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed, lifeTime;

    public Rigidbody theRB;

    public GameObject impactEffect;
    public GameObject miObjeto;

    // Start is called before the first frame update
    void Start()
    {
        // Obtén una referencia al GameObject por su nombre
        

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("enemy"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("enemyDos"))
        {
            //Destroy(other.gameObject);
            EnemyLifeController.instance.DamagePlayer(5);
        }
        else if (other.gameObject.tag.Equals("ProyectilEnemy")) {
            Destroy(other.gameObject);
            //Sumar punto
            GameManager.instance.AddScore(1);
        }
        else if (other.gameObject.tag.Equals("ProyectilEnemy3"))
        {
            Destroy(other.gameObject);
            //Sumar punto
            GameManager.instance.AddScore(1);
        }
        else if (other.gameObject.tag.Equals("EnemyThree"))
        {
            //Destroy(other.gameObject);
            if (miObjeto != null)
            {
                // Obtén el componente del controlador (script) adjunto al GameObject
                EnemyThreeController controlador = miObjeto.GetComponent<EnemyThreeController>();

                if (controlador != null)
                {
                    // Ahora puedes llamar a los métodos del controlador
                    controlador.DamagePlayerThree(5); 
                }
                else
                {
                    Debug.LogError("El GameObject no tiene el componente de controlador (script) MiControlador.");
                }
            }
            else
            {
                Debug.LogError("No se encontró el GameObject con el nombre MiObjeto.");
            }

        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
