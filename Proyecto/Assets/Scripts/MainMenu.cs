using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public string firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Form");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Rules()
    {
        SceneManager.LoadScene("Rules");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    public void Story()
    {
        SceneManager.LoadScene("Story");
    }

    public void Ranking()
    {
        SceneManager.LoadScene("Ranking");
    }
}