using Unity.VisualScripting;
using UnityEngine;

public class EnemyThreeController : MonoBehaviour
{

    public Animator animator;
    public Transform player;
    public Transform enemy;

    void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distancia = Vector3.Distance(enemy.position, player.position);
        //Debug.Log("Distancia entre objetos: " + distancia);
        animator.SetFloat("Distance", distancia);
    }

    public void isReceivingAttack()
    {
        animator.SetBool("ReceivingHit", true);
    }

}
