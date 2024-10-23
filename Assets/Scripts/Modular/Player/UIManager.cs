using Manager;
using RepeatUtils.DesignPattern.SingletonPattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private InPlayingUI inPlayingUI;
    public InPlayingUI InPlayingUI => inPlayingUI;

    [SerializeField]
    private PauseUI pauseUI;

    [SerializeField]
    private GameOverUI gameOverUI;
    public GameOverUI GameOverUI => gameOverUI;

    [SerializeField]
    private RectTransform blackBackground;

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

}
