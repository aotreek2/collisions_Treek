//////////////////////////////////////////////
//Assignment/Lab/Project: Collisions_Treek
//Name: Ahmed Treek
//Section: SGD.213.0021
//Instructor: Aurore Locklear
//Date: 3/7/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private Player createdPlayer;
    private GameObject player;
    [SerializeField] private GameObject[] repeatedLasers;
    private float timer;
    public bool buttonIsPressed;

    // Update is called once per frame
    void Update()
    {
        HandleLaserTiming();
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

    public void Win()
    {

    }
}
