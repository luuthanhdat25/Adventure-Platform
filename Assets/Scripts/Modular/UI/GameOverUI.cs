using LoadScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform gameOverMenu;

        [SerializeField]
        private TMP_Text winGameText;

        [SerializeField]
        private TMP_Text loseText;

        [SerializeField]
        private Button backToMenuButton;

        private void Start()
        {
            gameOverMenu.gameObject.SetActive(false);
            backToMenuButton.onClick.AddListener(BackToMenuScene);
        }

        public void Show(bool isWin)
        {
            winGameText.gameObject.SetActive(isWin);
            loseText.gameObject.SetActive(!isWin);
            gameOverMenu.gameObject.SetActive(true);
        }

        private void BackToMenuScene()
        {
            Loader.Load(Loader.Scene.GameMenuScene);
        }
    }
}
