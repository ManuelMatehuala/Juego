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
    private bool gameEnded = false;
    private int currentLevel = 1;

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
        if (!gameEnded)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void PauseUnpause()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Test")
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
        else if (currentSceneName == "Level2")
        {
            if (UIControllerLevel2.instance.pauseScreen.activeInHierarchy)
            {
                UIControllerLevel2.instance.pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            else
            {
                UIControllerLevel2.instance.pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }


    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Puntuación: " + score;
    }


    public void EndGame()
    {
        // Evitar que se llame a EndGame múltiples veces
        if (!gameEnded)
        {
            gameEnded = true;

            // Guardar el puntaje y el nivel en MongoDB al finalizar el tiempo
            int currentLevel = GetCurrentLevel();
            DBMongo.instance.SaveScore(PlayerPrefs.GetString("PlayerName", ""), score, currentLevel);

            SceneManager.LoadScene("Victory");
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCurrentLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Test")
        {
            return 1;
        }
        else if (currentSceneName == "Level2")
        {
            return 2;
        }

        return 1;
    }


    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
}

