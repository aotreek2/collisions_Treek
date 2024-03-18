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
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement & Camera")]
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    
    [Header("Pickup Related")]
    public Transform playerHand;
    private bool isHolding;
    public GameObject canvas;
    [SerializeField] private Material red;
    [SerializeField] private TMP_Text healthTxt, scoreTxt;

    [Header("Script Related")]
    CharacterController characterController;
    [SerializeField] private Laser createdLaser;
    [SerializeField] private Turrent createdTurret;
    [SerializeField] private Manager createdManager;
    private Player createdPlayer;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        createdPlayer = new Player();
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handles Pickups

        if (Input.GetKeyDown(KeyCode.E) && isHolding == false)
        {
            HandleRayCast();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }


        #endregion
    }

    void HandleRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            // Check if the hit object is interactive (tag or layer check, etc.)
             if (hit.collider.CompareTag("Box"))
            {
                // Pickup the object
                PickupObject(hit.collider.gameObject);
                isHolding = true;
            }

            else if (hit.collider.name == "Button 1")
            {
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                renderer.material = red;
                createdLaser.DisableLasers();
            }

            else if (hit.collider.name == "Button 2")
            {
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                renderer.material = red;
                createdTurret.isOnline = false;
            }

            else if (hit.collider.name == "Button 3")
            {
                Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                renderer.material = red;
                createdLaser.buttonIsPressed = true;
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(playerHand); // handTransform is the transform of the player's hand
        obj.transform.position = playerHand.position;

    }
    void Drop()
    {
        GameObject obj;
        obj = playerHand.transform.GetChild(0).gameObject;
        obj.GetComponent<Rigidbody>().isKinematic = false;
        playerHand.DetachChildren();
        isHolding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            Debug.Log("Hit PickUp");
            createdPlayer.GainScore(1);
            scoreTxt.text = ": " + createdPlayer.Score + "/10";
            Debug.Log(createdPlayer.Score);
            Destroy(other.gameObject);

            if(createdPlayer.Score == 10)
            {
                createdManager.Win();
                lookSpeed = 0;
            }
        }

        if (other.gameObject.tag == "Trap")
        {
            Debug.Log("Hit Trap");
            createdPlayer.TakeDamage(20);
            healthTxt.text = ": " + createdPlayer.Health;
            if (createdPlayer.Health <= 0)
            {
                createdManager.Death();
                lookSpeed = 0;
            }
            Debug.Log(createdPlayer.Health);
        }
    }

}

