using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menuObject;
    [SerializeField] private Button settinngButton;
    [SerializeField] private Button ExitButton;


    void Start()
    {
        GameManager.Instance.OnGamePaused += HideMenu;
        GameManager.Instance.OnGameUnpaused += ShowMenu;

        settinngButton.onClick.AddListener(HideMenu);
    }

    private void ShowMenu()
    {
        menuObject.gameObject.SetActive(true);
    }

    private void HideMenu()
    {
        menuObject.gameObject.SetActive(false);
    }
}
