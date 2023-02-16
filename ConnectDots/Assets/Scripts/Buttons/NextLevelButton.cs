using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void nextLevel()
    {
        audioSource.Play();
        LevelInfo.selectedLevel += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void returnToMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
