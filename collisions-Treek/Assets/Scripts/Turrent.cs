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

public class Turrent : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float range = 35f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject spitPrefab;
    private bool alreadyAttacked;
    public bool isOnline = true;
    [SerializeField] private Transform spitLocation;
    [SerializeField] private Animator turretAnim;

    private void Start()
    {
        turretAnim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        // Checks the distance to the player
        if (Vector3.Distance(transform.position, player.position) <= range && isOnline == true)
        {
            // make the turret face the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // raycast to check if the player is in line of sight
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, range, playerLayer))
            {
                if (hit.collider.transform == player)
                {
                    // if the raycast hits the player call the detected method
                    OnPlayerDetected();
                }
            }
        }
       else if(!isOnline)
        {

        }
    }

    private void OnPlayerDetected()
    {
        Vector3 target = (player.transform.position - spitLocation.position).normalized;

        Debug.Log("Player Detected!");
        if (!alreadyAttacked)
        {
            turretAnim.Play("Alien_attack");
            var acidSpit = Instantiate(spitPrefab, spitLocation.position, Quaternion.identity);
            acidSpit.GetComponent<Rigidbody>().velocity = target * 80f;
            Destroy(acidSpit, 3f);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 3f);
        }
        else
        {
            turretAnim.Play("Alien_idle");
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

