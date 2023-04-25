using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampToBoundaries : MonoBehaviour
{

    private Vector2 screenBounds;
    private float objectWidthHalf;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidthHalf = transform.GetComponent<SpriteRenderer>().bounds.extents.x;  // bounds.extents.x is half of the obect's width
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        // Clamp the X position based on screen boundaries and the object's width
        Vector2 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -1 * screenBounds.x + objectWidthHalf, screenBounds.x - objectWidthHalf);
        transform.position = newPosition;
    }
}
