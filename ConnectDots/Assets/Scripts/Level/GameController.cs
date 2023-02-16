using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int tappedDot = 0;
    // Coordinates between which to draw line
    private Vector2 previousDot;
    private Vector2 nextDot;
    private Vector2 firstDot;
    private bool lastSpawned = false;
    private AudioSource audioSource;

    public GameObject line;
    public GameObject nextLevel;
    public GameObject returnToMenu;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            Vector3 raycastOrigin = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            //Debug.DrawRay(raycastOrigin, Vector3.forward*50, Color.red, 1000f);

            if (Physics.Raycast(raycastOrigin, Vector3.forward, out hit, Mathf.Infinity));
            {
                if (hit.collider == null) { } // Line to prevent Null errors
                // If the ray hits a dot and the dot is next in line to be pressed
                else if (hit.transform.tag == "Dot" && hit.transform.gameObject.GetComponent<DotBehavior>().getDotNumber() == tappedDot + 1)
                {
                    audioSource.Play();
                    if (tappedDot == 0)    // For the first dot do not set nextDot coordinates
                    {
                        previousDot = new Vector2(hit.transform.position.x, hit.transform.position.y);
                        firstDot = previousDot;
                    }
                    else
                    {
                        nextDot = new Vector2(hit.transform.position.x, hit.transform.position.y);

                        // Instantiating Line Game Object to be drawn
                        Vector2 destination = nextDot - previousDot;
                        GameObject newLine = Instantiate(line, previousDot, Quaternion.identity, this.transform);
                        newLine.GetComponent<DrawLine>().setDestination(destination);

                        previousDot = nextDot;
                    }
                    // If the tapped dot is the last one
                    if (tappedDot == LevelInfo.dotAmount - 1)
                    {
                        // Draw line between current dot and the first dot
                        Vector2 destination = firstDot - previousDot;
                        GameObject newLine = Instantiate(line, previousDot, Quaternion.identity, this.transform);
                        newLine.GetComponent<DrawLine>().setDestination(destination);

                        // Making sure that last line is instantiated before enabling next level ui on line 68 so null exception errors wouldn't be thrown
                        lastSpawned = true;
                    }

                    tappedDot++;
                    hit.transform.GetComponent<DotBehavior>().dotClicked();
                }
            }           
        }
        // Enable Next Level/return to main menu buttons if finished drawing
        if (lastSpawned)
            if (this.gameObject.transform.GetChild(LevelInfo.dotAmount - 1).gameObject.GetComponent<DrawLine>().getFinishedDrawing())
            {
                if (LevelInfo.selectedLevel == LevelInfo.levelCount-1) returnToMenu.SetActive(true);
                else nextLevel.SetActive(true);
            }
    }

    public Vector2 getPreviousDot()
    {
        return previousDot;
    }
    public Vector2 getNextDot()
    {
        return nextDot;
    }
}
