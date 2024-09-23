using UnityEngine;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using UnityEngine.InputSystem;

namespace Manager
{
    public class InputManager : Singleton<InputManager>
    {
        public Action OnSwitchGun;

        private PlayerInputAction inputSystemSetting;

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
            inputSystemSetting.Player.SwitchGun.performed += (InputAction.CallbackContext context) => OnSwitchGun?.Invoke();
            inputSystemSetting.UI.Escape.performed += (InputAction.CallbackContext context) => GameManager.Instance.TogglePauseGame();
        }

        public Vector2 GetRawInputNormalized()
        {
            Vector2 inputVector = inputSystemSetting.Player.Move.ReadValue<Vector2>();
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public bool GetDashInputTrigger()
        {
            
            return inputSystemSetting.Player.Dash.IsPressed();
        }

        public bool IsShootPressed() => inputSystemSetting.Player.Shoot.IsPressed();

        /// <summary>
        /// The current Pointer coordinates in window space
        /// </summary>
        /// <returns>Vector 2 of Position</returns>
        public Vector2 GetMousePositionInScreen() => Mouse.current.position.ReadValue();
    }
}