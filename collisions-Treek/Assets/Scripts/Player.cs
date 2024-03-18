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

public class Player
{
    public int Health { get; set; }
    public int Score { get; set; }

    public Player()
    {
        Health = Mathf.Clamp(100, 0, 100); //clamps health so it wont go below 0 or above 100
        Score = Mathf.Clamp(0, 0, 10); //clamps score so it wont go below 0 or above 10
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage; //method to take damage when it is called with a pass by value
    }

    public void GainScore(int value)
    {
        Score += value; //method to gain score  when it is called with a pass by value
    }
}
