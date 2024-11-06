using UnityEngine;
using UnityEngine.UI;
using Manager;
using LoadScene;
using UnityEngine.Audio;

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


        [SerializeField]
        private Slider audioSetting;
        [SerializeField]
        private AudioMixer audioMixer;
        private Button[] buttons;
        private void Start()
        {
            float backgroundVolume;
            pauseMenu.gameObject.SetActive(false);
            


            continuteButton.onClick.AddListener(TogglePauseGame);
            backToMenuButton.onClick.AddListener(BackToMenuScene);

            audioMixer.GetFloat("MasterVolume", out backgroundVolume);
            audioSetting.value = Mathf.Pow(10, backgroundVolume / 20);

            audioSetting.onValueChanged.AddListener(UpdateVolume);
        }

        private void TogglePauseGame()
        {
            GameManager.Instance.TogglePauseGame();
        }

        private void BackToMenuScene()
        {
            //Loader.Load(Loader.Scene.GameMenuScene);
        }
        private void UpdateVolume(float value)
        {
            float volumeInDb = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("MasterVolume", volumeInDb);

        }
    }
}
