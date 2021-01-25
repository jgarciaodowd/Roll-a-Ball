﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
    private Rigidbody rb;
    private int count;
    private int points;
    private float movementX;
    private float movementY;
    private Vector3 OriginalPos;
    public TextMeshProUGUI CountText;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        points = 0;
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        setCountText();
        OriginalPos = transform.position;
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get < Vector2 >();
        movementX = movementVector.x;
        movementY = movementVector.y;
        //movementX = Input.acceleration.x;
        //movementY = -Input.acceleration.y;
    }

    void setCountText(){
        CountText.text = "Points: " + points.ToString();
        if(count >= 8 && points == 8){
            WinTextObject.SetActive(true);
        }else{
            if(count >= 8 && points < 8){
                LoseTextObject.SetActive(true);
            }
        }
    }

    void FixedUpdate(){
        Vector3 movement = new Vector3(movementX,0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count++;
            points++;
            setCountText();
        }
        if(other.gameObject.CompareTag("Enemy")){
            transform.position = OriginalPos;
            if(points > 0){
                points--;
            }
            setCountText();
        }
    }
}
