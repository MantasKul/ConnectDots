using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject levelSelectionCanvas;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();    
    }

    // Opening level selection screen
    public void buttonPlay(int level)
    {
        audioSource.Play();
        LevelInfo.selectedLevel = level;
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    // Exit application
    public void buttonExit()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void swapCanvas()
    {
        audioSource.Play();
        mainCanvas.SetActive(!mainCanvas.activeSelf);
        levelSelectionCanvas.SetActive(!levelSelectionCanvas.activeSelf);
    }
}
