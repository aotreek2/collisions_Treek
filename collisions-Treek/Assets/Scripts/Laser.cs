using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject[] lasers, repeatedLasers; // Array to hold the laser GameObjects
    public float speed = 2.0f; // Speed of the laser movement
    public float distance = 2.0f; // Distance the laser moves left and right
    private float timer;
    public bool buttonIsPressed;
    private Vector3[] startingPositions;
    private float direction = 1.0f; // Direction of movement, 1 for right, -1 for left

    void Start()
    {
        // Initialize starting positions array based on the number of lasers
        startingPositions = new Vector3[lasers.Length];

        // Save the starting position of each laser
        for (int i = 0; i < lasers.Length; i++)
        {
            if (lasers[i] != null)
            {
                startingPositions[i] = lasers[i].transform.position;
            }
        }
    }

    void Update()
    {
        HandleLaserTiming();

        for (int i = 0; i < lasers.Length; i++)
        {
            if (lasers[i] != null)
            {
                // Calculate the movement direction and position for each laser
                float movement = Mathf.Sin(Time.time * speed) * distance;
                lasers[i].transform.position = startingPositions[i] + new Vector3(0, 0, movement * direction);
            }
        }
    }

    private void HandleLaserTiming()
    {
        timer += Time.deltaTime;

        // Check if the timer has reached the toggle interval
        if (timer >= 2f && !buttonIsPressed)
        {
            // Reset the timer
            timer = 0f;

            // Toggle the active state of the laser
            for (int i = 0; i < repeatedLasers.Length; i++)
            {
                repeatedLasers[i].SetActive(!repeatedLasers[i].activeSelf);
            }
        }
        else if (buttonIsPressed == true)
        {
            for (int i = 0; i < repeatedLasers.Length; i++)
            {
                repeatedLasers[i].SetActive(false);
            }
        }
    }

    public void DisableLasers()
    {
        foreach (var laser in lasers)
        {
            if (laser != null)
            {
                laser.SetActive(false);
            }
        }
    }
}
