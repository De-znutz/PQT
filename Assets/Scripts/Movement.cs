using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed,playerGravity,jumpSpeed;
    private float InpZ, InpX,sprintMultiplier = 1.5f;
    private Vector3 direct;
    private Transform meshPos;
    private bool jumpQueued,isSprinting;



    // Start is called before the first frame update
    void Start()
    {
        //get transform of players mesh for rotation
        meshPos = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        //get movement input
        InpX = -1 * Input.GetAxis("Horizontal");
        InpZ = Input.GetAxis("Vertical");
        //Get space bar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpQueued = true;
        }
        //get sprint key input
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }


    }

    private void FixedUpdate()
    {
        //aircheck
        if (controller.isGrounded)
        {
            if (jumpQueued) {
                direct.y += jumpSpeed;
                jumpQueued = false;
            }
            else {
                direct.y = 0f;
            }
            
        }
        else {
            //gravity
            direct.y -= playerGravity * Time.deltaTime;
        }
        if (isSprinting)
        {
            direct = new Vector3(InpZ * playerSpeed * sprintMultiplier, direct.y, InpX * playerSpeed * sprintMultiplier);
        }
        else
        {
            direct = new Vector3(InpZ * playerSpeed, direct.y, InpX * playerSpeed);
        }
        


        //move
        controller.Move(direct);

        //mesh rotate
        if (InpX != 0 || InpZ != 0)
        {
            Vector3 lookDir = new Vector3(direct.x, 0, direct.z);
            meshPos.rotation = Quaternion.LookRotation(lookDir);
        }

    }
}
