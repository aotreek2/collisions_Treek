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

    void Start()
    {
        createdPlayer = new Player();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
