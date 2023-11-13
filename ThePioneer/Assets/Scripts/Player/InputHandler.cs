using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MV {
    public class InputHandler : MonoBehaviour {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

       //pause
        public event EventHandler OnPauseAction;
        public static InputHandler Instance { get; private set; }
        //

        //Roll
        public bool b_Input;

        //Attack
        public bool rb_Input;
        public bool rt_Input;
        public bool rd_Input;

        public bool rollFlag;
        public bool isInteracting;
        // 

        PlayerControls inputActions;
        CameraHandler cameraHandler;
        PlayerAttacker playerAttacker;

        Vector2 movementInput;
        Vector2 cameraInput;

        //Camara
        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
            playerAttacker = GetComponent<PlayerAttacker>();
        }
        
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if(cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }
        //

        public void OnEnable() {
          //Pause
            Instance = this;
            //
            if (inputActions == null) {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                //Pause
                 inputActions.PlayerMovement.Pause.performed += Pause_performed;
                //
            }

            inputActions.Enable();
        }

        //Pause
        private void Pause_performed(InputAction.CallbackContext context) {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }
        //
      
        private void OnDisable() 
        {
            inputActions.Disable();
        }
        

        public void TickInput(float delta) {
            MoveInput(delta);
            //HandleRollInput(delta);
            HandleAttackInput(delta);
        }

        private void MoveInput(float delta) {

            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        /*/Roll
        private void HandleRollInput(float delta)
        {

            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (b_Input)
            {
                rollFlag = true;
            }
        }
        /*/
      
        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            inputActions.PlayerActions.RD.performed += i => rd_Input = true;

            if(rb_Input)
            {
                playerAttacker.HandleLightAttack();
            }

            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack();
            }

            if (rd_Input)
            {
                playerAttacker.HandleDirectAttack();
            }
        }
    }
}
