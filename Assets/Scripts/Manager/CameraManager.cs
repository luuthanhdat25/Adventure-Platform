using Cinemachine;
using RepeatUtils.DesignPattern.SingletonPattern;
using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(CinemachineBrain))]
    public class CameraManager : Singleton<CameraManager>
    {
        private CinemachineBrain cinemachineBrain;

        private Vector2 screenSize;

        public Camera CurrentCinemachineCameraActive() => cinemachineBrain.OutputCamera;

        protected override void Awake()
        {
            base.Awake();
            cinemachineBrain = GetComponent<CinemachineBrain>();
            screenSize = new Vector2(Screen.width, Screen.height);
        }

        public Vector2 GetNormalizedMouseDirectionToScreenCenter()
        {
            Vector2 mousePosition = InputManager.Instance.GetMousePositionInScreen();
            Vector2 mousePositionRelativeToCenter = mousePosition - screenSize / 2.0f;
            Vector2 normalizedMousePosition = mousePositionRelativeToCenter / (screenSize / 2.0f);
            return normalizedMousePosition;
        }
    }
}
