using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitAfterDying = 2f;
    public TextMeshProUGUI scoreText;
    public GameObject powerupPrefab;
    public float powerupSpawnInterval = 10f;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerupSpawnInterval);
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }

    public void PlayerDied()
    {
        StartCoroutine("PlayerDiedCo");
    }

    public IEnumerator PlayerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene("GameOver");
    }

    public void PauseUnpause()
    {
        if (UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Puntuación: " + score;
    }
}

