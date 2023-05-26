using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClampToBoundaries : MonoBehaviour
{
    private float _minX;
    private float _maxX;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer backgroundSr = transform.parent.Find("Background").GetComponent<SpriteRenderer>();

        // Local position is affected by parent position
        _minX = transform.parent.position.x - backgroundSr.bounds.extents.x;
        _maxX = transform.parent.position.x + backgroundSr.bounds.extents.x;
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        float objectWidthHalf = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;  // bounds.extents.x is half of the object's width

        // Clamp the X position based on the screen boundaries and the object's width
        Vector2 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX + objectWidthHalf, _maxX - objectWidthHalf);
        transform.position = newPosition;
    }
}
