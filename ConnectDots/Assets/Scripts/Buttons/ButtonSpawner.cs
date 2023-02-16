using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject button;
    public int buttonCount;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for(int i = 0; i < buttonCount; i++)
        {
            GameObject newButton = Instantiate(button);
            newButton.transform.SetParent(transform);
            newButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(i.ToString());
            newButton.transform.position = transform.position + new Vector3(i*200,1,1);
            newButton.transform.localScale = transform.localScale;
            newButton.transform.GetComponent<Button>().onClick.AddListener(() => loadLevel(i));
        }
    }

    void loadLevel(int level)
    {
        Debug.Log(level);
        audioSource.Play();
        LevelInfo.selectedLevel = level;
        //SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
