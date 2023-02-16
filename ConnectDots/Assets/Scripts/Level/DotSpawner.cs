using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE
// CHANGE TUPLE TO VECTOR2
public class DotSpawner : MonoBehaviour
{
    // maximumx x/y coordinate that can come from JSON file (according to task's rules maximum value can be 1000)
    private const int MAXCOORDINATE = 1000;

    // List of tuples to hold x and y coordinates of each dot. Item1 - x, Item2 - y coordinates
    private List<Vector2> dotPosition = new List<Vector2>();
    private int selectedLevel = 0;

    public TextAsset jsonFile;  // Json file which contains dot positions
    public GameObject dotObject;   // Prefabs to sapwn

    void Start()
    {
        selectedLevel = LevelInfo.selectedLevel;
        // Setting the gameObject's position to top-left of the screen
//        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
//        transform.position = new Vector3(topLeft.x, topLeft.y, 0);

        // Reading the json file
        Levels levelsInJson = JsonUtility.FromJson<Levels>(jsonFile.text);

        // Puting coordinates from json file to list of tuples
        int i = 0;
        while (i < levelsInJson.levels[selectedLevel].level_data.Length - 1)
        {
            dotPosition.Add(new Vector2((float)levelsInJson.levels[selectedLevel].level_data[i], (float)levelsInJson.levels[selectedLevel].level_data[i + 1]));
            i += 2;
        }

        LevelInfo.dotAmount = dotPosition.Count;
        LevelInfo.levelCount = levelsInJson.levels.Length;

        // Instantiate dotObject prefab on the coordinates using list of tuples
        i = 0;
        foreach (Vector2 v in dotPosition)
        {
            // Offsets prevent dots from bein instantiated in areas outside the safe area rectangle
            float offsetX = Screen.width - Screen.safeArea.width;
            float offsetY = Screen.height - Screen.safeArea.height;
            i++;
            // Linear mapping to screen width/height
            float x = offsetX + (v.x / MAXCOORDINATE) * Screen.safeArea.width; // Screen.safeArea.x
            float y = offsetY + ((MAXCOORDINATE - v.y) / MAXCOORDINATE) * Screen.safeArea.height;    // Screen.safeArea.y  // MAXCOORDINATE - v.y flips y, so 0,0 will be top-left corner isntead of bottom-left

            Vector2 dotPosition = Camera.main.ScreenToWorldPoint(new Vector2(x, y));

            GameObject newDot = Instantiate(dotObject, dotPosition, Quaternion.identity);
            newDot.transform.SetParent(transform);
            newDot.GetComponent<DotBehavior>().setDotNumber(i);
        }
    }
}
