using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampToBoundaries : MonoBehaviour
{
    public GameObject background;

    private Vector2 _screenBounds;
    private float _objectWidthHalf;

    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = background.GetComponent<SpriteRenderer>().bounds.extents;
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        _objectWidthHalf = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;  // bounds.extents.x is half of the object's width

        // Clamp the X position based on the screen boundaries and the object's width
        Vector2 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, -1 * _screenBounds.x + _objectWidthHalf, _screenBounds.x - _objectWidthHalf);
        transform.position = newPosition;
    }
}
