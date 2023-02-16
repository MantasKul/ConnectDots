using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DotBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    //    private BoxCollider2D collider;

    public GameObject numberTMP;
    public Sprite sprite;   // Sprite to switch to when pressed
    public int dotNumber;   // Keeping track of which dot number it is

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
//        collider = GetComponent<BoxCollider2D>();

        textMeshPro = GetComponentInChildren(typeof(TextMeshPro)) as TextMeshPro;
        textMeshPro.text = dotNumber.ToString();
    }

    private void Update()
    {

        // Tested different way of touching the dots, requires BoxCollider2D
        /*        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector2 touchOrigin = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                    if (collider.OverlapPoint(touchOrigin)) // Could also use Physics2D.OverlapPoint with layer Mask (int layerMask = 1 << 6) to detect only dots
                    {
                        Debug.Log(this.transform.name);
                        changeSprite();
                    }
                }*/
    }

    public void dotClicked()
    {
        // Swapping sprite
        spriteRenderer.sprite = sprite;

        // Fading number
        StartCoroutine(fadeNumber());
    }

    IEnumerator fadeNumber()
    {
        for(float i = 1; i >= 0; i -= Time.deltaTime)
        {
            textMeshPro.alpha = i;
            yield return null;
        }
    }

    public void setDotNumber(int dotNumber)
    {
        this.dotNumber = dotNumber;
    }
    public int getDotNumber()
    {
        return this.dotNumber;
    }
}
