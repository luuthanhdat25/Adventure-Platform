using Manager;
using RepeatUtils.DesignPattern.SingletonPattern;
using ScriptableObjects;
using Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private InPlayingUI inPlayingUI;
    public InPlayingUI InPlayingUI => inPlayingUI;

    [SerializeField]
    private PauseUI pauseUI;

    [SerializeField] SoundSO clickSound;
    [SerializeField]
    private GameOverUI gameOverUI;
    public GameOverUI GameOverUI => gameOverUI;

    [SerializeField]
    private RectTransform blackBackground;
    private Button[] buttons;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadComponentInChild(ref inPlayingUI, gameObject);
        LoadComponentInChild(ref pauseUI, gameObject);
        LoadComponentInChild(ref gameOverUI, gameObject);
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        buttons = FindObjectsOfType<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    private void GameManager_OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
    {
        blackBackground.gameObject.SetActive(e.NewGameState != GameManager.GameState.GamePlaying);
        switch (e.NewGameState)
        {
            case GameManager.GameState.GamePlaying:
                inPlayingUI.ShowUI(true);
                pauseUI.PauseMenu.gameObject.SetActive(false);
                break;

            case GameManager.GameState.GameOver:
                inPlayingUI.ShowUI(false);
                break;

            case GameManager.GameState.GamePaused:
                pauseUI.PauseMenu.gameObject.SetActive(true);
                inPlayingUI.ShowUI(false);
                break;
        }
    }

    private void PlayClickSound()
    {
        SoundPooling.Instance.CreateSound(clickSound, 0, 0);
    }

}
