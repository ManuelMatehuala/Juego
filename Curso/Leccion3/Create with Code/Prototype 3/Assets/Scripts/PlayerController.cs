using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody palyerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool ground = true;
    // Start is called before the first frame update
    void Start()
    {
        palyerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ground)
        {
            palyerRb.AddForce(Vector3.up, ForceMode.Impulse);
            ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }


}
