using UnityEngine;
using UnityEngine.UI;
using Manager;
using LoadScene;

namespace UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform pauseMenu;
        public RectTransform PauseMenu => pauseMenu;

        [SerializeField]
        private Button continuteButton;
        public Button ContinuteButton => continuteButton;

        [SerializeField]
        private Button backToMenuButton;
        public Button BackToMenuButton => backToMenuButton;


        private void Start()
        {
            pauseMenu.gameObject.SetActive(false);
            continuteButton.onClick.AddListener(TogglePauseGame);
            backToMenuButton.onClick.AddListener(BackToMenuScene);
        }

        private void TogglePauseGame()
        {
            GameManager.Instance.TogglePauseGame();
        }

        private void BackToMenuScene()
        {
            //Loader.Load(Loader.Scene.GameMenuScene);
        }
    }
}
