using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float speed = 2.0f; // Speed of the laser movement
    private float distance = 2.0f; // Distance the laser moves left and right

    private Vector3 startingPosition;
    private float direction = 1.0f; // Direction of movement, 1 for right, -1 for left
    void Start()
    {
        // Save the starting position of the laser
        startingPosition = transform.position;
    }

    void Update()
    {
        // Calculate the movement direction and position
        float movement = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startingPosition + new Vector3(0, 0, movement * direction);
    }

    public void DisableLasers()
    {
        this.gameObject.SetActive(false);
    }
}
