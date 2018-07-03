﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    // Use this for initialization
    private Animator anim;
    private CharacterController charController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool canMove;
    private bool finished_Movement = true;

    private float player_ToPointDistance;
    private Vector3 target_Pos = Vector3.zero;
    private Vector3 player_Move = Vector3.zero;

    private float gravity = 9.8f;
    private float height;


	void Awake () {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveThePlayer();
        charController.Move(player_Move);
	}


    void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
             
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider is TerrainCollider)
                {
                    player_ToPointDistance = Vector3.Distance(transform.position, hit.point);
                    if (player_ToPointDistance >= 1.0f)
                    {
                        canMove = true;
                        target_Pos = hit.point;
                    }
                }
            }
        }
            if (canMove)
            {
                anim.SetFloat("Walk", 1.0f);
                Vector3 target_Temp = new Vector3(target_Pos.x, transform.position.y, target_Pos.z);


                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target_Temp - transform.position),15.0f*Time.deltaTime);
                player_Move = transform.forward * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, target_Pos) <= 0.5f)
                {
                    canMove = false;
                }
            }
            else
            {
                player_Move.Set(0.0f, 0.0f, 0.0f);
                anim.SetFloat("Walk", 0.0f);
            }

        

    }



}
