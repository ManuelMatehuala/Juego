using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{

    public float timeBetweenShowing = 1f;
    public GameObject textBox, returnButton, levelButton;

    //public Image blackScreen;
    public float blackScreenFade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowObjectsCo");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        //blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFade * Time.deltaTime));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LevelDos()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1f;
    }
    public IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(timeBetweenShowing);
        textBox.SetActive(true);
        yield return new WaitForSeconds(timeBetweenShowing);
        returnButton.SetActive(true);
        yield return new WaitForSeconds(timeBetweenShowing);
        levelButton.SetActive(true);
    }
}
