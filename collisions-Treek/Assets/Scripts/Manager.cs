//////////////////////////////////////////////
//Assignment/Lab/Project: Collisions_Treek
//Name: Ahmed Treek
//Section: SGD.213.0021
//Instructor: Aurore Locklear
//Date: 3/7/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text resultTxt;
    [SerializeField] private GameObject helpPanel, mainPanel, resultsPanel;
    Scene scene;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            helpPanel.SetActive(false);
        }
        else if(scene.name == "MainGame")
        {
            resultsPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        
    }

    private void Update()
    {
        
    }
    public void Win()
    {
        resultsPanel.SetActive(true);
        resultTxt.text = "You Win!";

        if (resultsPanel.activeSelf == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Death()
    {

        resultsPanel.SetActive(true);
        resultTxt.text = "You Died!";

        if (resultsPanel.activeSelf == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnMenuButtonClicked()
    {
        resultsPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnHelpButtonClicked()
    {
        helpPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void OnCloseButtonClicked()
    {
        helpPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
