using RepeatUtils.DesignPattern.SingletonPattern;
using ScriptableObjects;
using Sound;
using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager 
{
    public class GameManager : Singleton<GameManager>
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event Action OnGamePaused;
        public event Action OnGameUnpaused;

        [SerializeField]
        private SoundSO winGameSoundSO;

        [SerializeField]
        private SoundSO loseGameSoundSO;

        [SerializeField]
        private SoundSO gameMusicBackgroundSO;

        [SerializeField] private SoundSO demosound;

        public class OnStateChangedEventArgs : EventArgs {
            public GameState NewGameState;
        }

        public enum GameState
        {
            GamePlaying,
            GameOver,
            GamePaused
        }

        private GameState state;

        protected override void Awake()
        {
            base.Awake();
            state = GameState.GamePlaying;
            Time.timeScale = 1;
        }

        private void Start()
        {
            SoundPooling.Instance.CreateSound(gameMusicBackgroundSO, 0, 0); 
        }

        public void TogglePauseGame()
        {
            if (state == GameState.GameOver) return;

            if (state != GameState.GamePaused) {
                ChangeState(GameState.GamePaused);
                Time.timeScale = 0;
                OnGamePaused?.Invoke();
            } else {
                ChangeState(GameState.GamePlaying);
                Time.timeScale = 1;
                OnGameUnpaused?.Invoke();
            }

        }

        private void FixedUpdate()
        {

            if (InputManager.Instance.IsEscapes())
            {
                TogglePauseGame();
            }
            switch (state)
            {
                case GameState.GamePlaying:
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        private void ChangeState(GameState newState)
        {
            state = newState;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                NewGameState = newState
            });
        }
     
        public bool IsGamePlaying() => state == GameState.GamePlaying;

        public void GameOver(bool isWin)
        {
            SoundPooling.Instance.StopAll();
            SoundSO playSound = isWin ? winGameSoundSO : loseGameSoundSO;
            //SoundPooling.Instance.CreateSound(playSound, PlayerPublicInfor.Instance.Position, 0, 0);

            UI.UIManager.Instance.GameOverUI.Show(isWin);
            ChangeState(GameState.GameOver);
            Time.timeScale = 0;
        }
    
        public void PlayAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}