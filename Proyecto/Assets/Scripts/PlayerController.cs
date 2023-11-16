using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    
    public CharacterController characterController;

    private Vector3 moveInput;

    public Transform camTrans;

    [Header("Gravedad")]
    public float gravityModifier;

    [Header("Jugador control")]
    public float moveSpeed;
    public float jumpPower;
    private bool canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    [Header("Cámara control")]
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;

    public GameObject bullet;
    public Transform firePoint;

    public PistolController activeGun;
    public static PlayerController instance;

    public List<PistolController> allGuns = new List<PistolController>();
    public int currentGun;

    public Transform adsPoint, gunHolder, sdwPoint, afPoint, fpPoint;
    private Vector3 gunStartPosition;
    private Quaternion gunStartRotation;
    private Vector3 firePointStartPosition;
    public float adsSpeed = 2f;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //activeGun = allGuns[currentGun];
        //activeGun.gameObject.SetActive(true);

        //UIController.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;

        currentGun--;
        SwitchGun();

        gunStartPosition = gunHolder.localPosition;
        gunStartRotation = gunHolder.localRotation;
        firePointStartPosition = fpPoint.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy)
        {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //Guardar y velocity
        float yStore = moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        moveInput = moveInput * moveSpeed;

        moveInput.y = yStore;

        //Gravedad
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //Salto del Jugador
        if (Input.GetButtonDown("Jump"))
        {
            moveInput.y = jumpPower;
        }

        characterController.Move(moveInput * Time.deltaTime);

        //Control Rotación Cámara
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

        // Shooting
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            FireShot();
        }

        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <= 0)
            {
                FireShot();
            }
        }

        if (Input.GetButtonDown("Switch Gun"))
        {
            SwitchGun();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(activeGun.zoomAmount);
        }

        if (Input.GetMouseButton(1))
        {
            gunHolder.position = Vector3.MoveTowards(gunHolder.position, adsPoint.position, adsSpeed * Time.deltaTime);
        }
        else
        {
            gunHolder.localPosition = Vector3.MoveTowards(gunHolder.localPosition, gunStartPosition, adsSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            fpPoint.position = Vector3.MoveTowards(fpPoint.position, afPoint.position, adsSpeed * Time.deltaTime);
        }
        else
        {
            fpPoint.localPosition = Vector3.MoveTowards(fpPoint.localPosition, firePointStartPosition, adsSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(1) && gameObject.CompareTag("sword"))
        {
            gunHolder.rotation = Quaternion.RotateTowards(gunHolder.rotation, sdwPoint.rotation, adsSpeed * Time.deltaTime);
        }
        else
        {
            sdwPoint.localRotation = Quaternion.RotateTowards(sdwPoint.localRotation, gunStartRotation, adsSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();
        }
    }
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            UIController.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
        }
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;

        if(currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
    }
}
