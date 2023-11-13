using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MV
{
    public class PlayerManager : MonoBehaviour
    {
        
        
        

        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isInAir;
        public bool isGrounded;
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            inputHandler.rollFlag = false;
            isInteracting = anim.GetBool("isInteracting");
            inputHandler.TickInput(delta);

            //playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        }

        

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if(cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag |= false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.rd_Input = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }

        }
    }
}
