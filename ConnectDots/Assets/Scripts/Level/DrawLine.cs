using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private const float DRAWSPEED = 3f;

    private LineRenderer lineRenderer;
    private float distance;
    private Vector2 destination;
    private bool finishedDrawing;
    private AudioSource audioSource;

    private bool previousLineDrawn;
    Vector2 currentPosition = new Vector2(0, 0);

    void Start()
    {
        finishedDrawing = false;
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();

        distance = Vector2.Distance(new Vector2(0, 0), destination);
        destination.Normalize();

        if (transform.GetSiblingIndex() != 0)   // If current dot isn't the first one so we wouldn't look for sibling at -1 index
            previousLineDrawn = transform.parent.gameObject.transform.GetChild(transform.GetSiblingIndex() - 1).gameObject.GetComponent<DrawLine>().getFinishedDrawing();
        else previousLineDrawn = true;
    }

    void Update()
    {
        if (transform.GetSiblingIndex() != 0)
            previousLineDrawn = transform.parent.gameObject.transform.GetChild(transform.GetSiblingIndex() - 1).gameObject.GetComponent<DrawLine>().getFinishedDrawing();
        else previousLineDrawn = true;   // for the first child there're no previous ones so automatically 'previous' line is drawn

        // Only drawing if previous line has finished being drawn
        if (previousLineDrawn)
        {
            float distanceA = Vector2.Distance(new Vector2(0, 0), currentPosition);
            float distanceB = Vector2.Distance(new Vector2(0, 0), distance*destination);

            if (distanceA < distanceB)
            {
                if (!audioSource.isPlaying) audioSource.Play();
                currentPosition += destination * Time.deltaTime * DRAWSPEED;

                lineRenderer.SetPosition(1, currentPosition);
            }
            if (distanceA >= distanceB)
            {
                audioSource.Stop();
                finishedDrawing = true;
            }
        }
    }

    public void setDestination(Vector2 destination)
    {
        this.destination = destination;
    }
    public bool getFinishedDrawing()
    {
        return finishedDrawing;
    }
}
