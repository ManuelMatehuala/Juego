using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Button forwardButton;
    public Button backButton;
    //public Button jumpButton;
    public Button bulletButton;
    public Button menuButton;
    public Button cpistolButton;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        //Desactivar los botones al inicio
        forwardButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
        //jumpButton.gameObject.SetActive(false);
        bulletButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        cpistolButton.gameObject.SetActive(false);

        // Activar solo los botones necesarios para Android
        if (Application.platform == RuntimePlatform.Android)
        {
            forwardButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            //jumpButton.gameObject.SetActive(true);
            bulletButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
            cpistolButton.gameObject.SetActive(true);

        }

        currentGun--;
        SwitchGun();

        gunStartPosition = gunHolder.localPosition;
        gunStartRotation = gunHolder.localRotation;
        firePointStartPosition = fpPoint.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        bool uiControl = true;
        if (currentSceneName == "Test")
        {
            uiControl = !UIController.instance.pauseScreen.activeInHierarchy;
        }
        else if (currentSceneName == "Level2")
        {
            uiControl = !UIControllerLevel2.instance.pauseScreen.activeInHierarchy;
        }

        if (uiControl)
        {

            // Verificar si estamos en Android
#if UNITY_ANDROID
            if (isMovingForward || isMovingBackward)
            {
                HandleMovement();
            }
#endif


            // Verificar si estamos en PC
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
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
#endif


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


            // Verificar si estamos en PC
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
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
#endif

        }
    }

    // Método para avanzar
    public void MoveForward()
    {
        isMovingForward = true;
        isMovingBackward = false;
    }

    // Método para retroceder
    public void MoveBackward()
    {
        isMovingForward = false;
        isMovingBackward = true;
    }

    // Método para detener el movimiento
    public void StopMovement()
    {
        isMovingForward = false;
        isMovingBackward = false;
    }

    // Método para saltar
    public void Jump()
    {
        //if (characterController.isGrounded)
        //{
        //moveInput.y = jumpPower;
        //}

        float yStore = moveInput.y;

        moveInput.y = yStore;

        //Gravedad
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        moveInput.y = jumpPower;
    }

    // Método común para el movimiento
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Guardar y velocity
        float yStore = moveInput.y;

        Vector3 vertMove = isMovingForward ? transform.forward : (isMovingBackward ? -transform.forward : Vector3.zero);
        Vector3 horiMove = transform.right * horizontalInput;

        moveInput = horiMove + vertMove;
        moveInput.Normalize();
        moveInput *= moveSpeed;

        moveInput.y = yStore;

        // Gravedad
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (characterController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        characterController.Move(moveInput * Time.deltaTime);
    }


    public void FireButtonPressed()
    {
        // Si la pistola está lista para disparar
        if (activeGun.fireCounter <= 0)
        {
            RaycastHit hit;
            // Realizar un raycast para detectar un objetivo
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
            {
                // Ajustar la dirección del punto de fuego hacia el objetivo
                if (Vector3.Distance(camTrans.position, hit.point) > 2f)
                {
                    firePoint.LookAt(hit.point);
                }
            }
            else
            {
                // Si no se detecta un objetivo, apuntar a una posición predeterminada
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }

            // Llamar al método de disparo
            FireShot();
        }
    }


    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "Test")
            {
                UIController.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
            }
            else if (currentSceneName == "Level2")
            {
                UIControllerLevel2.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
            }
            
        }
    }

    public void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);

        currentGun++;

        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Test")
        {
            UIController.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
        }
        else if (currentSceneName == "Level2")
        {
            UIControllerLevel2.instance.ammoText.text = "Balas: " + activeGun.currentAmmo;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
