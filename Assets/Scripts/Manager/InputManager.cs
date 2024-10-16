using UnityEngine;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public Action OnSwitchGun;

        private PlayerInputAction inputSystemSetting;

        private Dictionary<string, bool> inputStates = new Dictionary<string, bool>();
        protected override void Awake()
        {
            base.Awake();
            this.EnableInputAction();
        }

        private void EnableInputAction()
        {
            inputSystemSetting = new PlayerInputAction();
            inputSystemSetting.Enable();
        }

        private void OnDisable()
        {
            inputSystemSetting.Disable();
        }

        private void Start()
        {
            inputSystemSetting.Player.SwitchGun.performed += (InputAction.CallbackContext context) =>  OnSwitchGun?.Invoke();
            inputSystemSetting.UI.Escape.performed += (InputAction.CallbackContext context) => GameManager.Instance.TogglePauseGame();
        }

        public Vector2 GetRawInputNormalized()
        {
            Vector2 inputVector = inputSystemSetting.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public bool IsDashInputTrigger() =>inputSystemSetting.Player.Dash.IsPressed();

       

        public bool IsJumpInputTrigger() => IsInputTriggered("Jump", inputSystemSetting.Player.Jump.IsPressed());
        
        public bool IsTabIsOpenedPressed() => IsInputTriggered("OpenTabMenu", inputSystemSetting.Player.OpenTabMenu.IsPressed());
        
        public bool IsAttackPressed() => 
            // IsInputTriggered("Attack",
                inputSystemSetting.Player.Attack.IsPressed()
                // )
                ;

        public bool IsAttackHold() => inputSystemSetting.Player.Attack.IsInProgress();

        public bool IsShootPressed() => inputSystemSetting.Player.Shoot.IsPressed();

        public bool IsPerformSkillPressed()=> inputSystemSetting.Player.PerformSkill.IsPressed(); 
        
        private bool IsInputTriggered(string actionName, bool isPressed)
        {
            if (!inputStates.ContainsKey(actionName))
            {
                inputStates[actionName] = false;
            }

            bool wasPressed = inputStates[actionName];

            if (isPressed && !wasPressed)
            {
                inputStates[actionName] = true;
                return true;
            }
            inputStates[actionName] = isPressed;
            return false;
        }
       
        

        /// <summary>
        /// The current Pointer coordinates in window space
        /// </summary>
        /// <returns>Vector 2 of Position</returns>
        public Vector2 GetMousePositionInScreen() => Mouse.current.position.ReadValue();
    }
}