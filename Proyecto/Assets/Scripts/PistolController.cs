using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public GameObject bullet;

    public bool canAutoFire;

    public float fireRate;

    [HideInInspector]
    public float fireCounter;

    public int currentAmmo, pickupAmount;

    public Transform firePoint;

    public float zoomAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmo()
    {
        currentAmmo += pickupAmount;

        UIController.instance.ammoText.text = "Balas: " + currentAmmo;
    }
}
