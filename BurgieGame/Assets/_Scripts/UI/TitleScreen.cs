using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//public enum GameState { MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU };
//public enum MenuState { NONE, PAUSE_MENU, CHARACTER, GAME, TITLE_SCREEN };



public class TitleScreen : MonoBehaviour
{
    private GameState _gameState;
    private MenuState _menuState;
    public Text gameStateDisplay;
    public Canvas PauseMenuCanvas;
    private GameObject storeSelected;

    public EventSystem es;

    public GameState gameState
    {
        set
        {
            _gameState = value;
        }
        get
        {
            return _gameState;
        }
    }
    public MenuState menuState
    {
        set
        {
            _menuState = value;
            if (changeMenu != null)
            {
                changeMenu(menuState); //send notification to change the menu
                //print(_menuState);
            }
        }
        get
        {
            return _menuState;
        }
    }

    private void Awake()
    {
        if (es == null)
        {
            es = (EventSystem)FindObjectOfType(typeof(EventSystem));
        }
        storeSelected = es.firstSelectedGameObject;
    }

    private void OnEnable()
    {
        MenuHandler_Title.firstItemNotif += ChangeSelectedItemMenuChange;
    }

    private void OnDisable()
    {
        MenuHandler_Title.firstItemNotif -= ChangeSelectedItemMenuChange;
    }

    private void Update()
    {
        if (es.firstSelectedGameObject != storeSelected)
        {
            if (es.currentSelectedGameObject == null)
            {
                es.SetSelectedGameObject(storeSelected);
            }
            else
            {
                storeSelected = es.currentSelectedGameObject;
            }
        }
    }

    public static event Action<MenuState> changeMenu;

    public void switchToMainMenu()
    {
        menuState = MenuState.PAUSE_MENU;

    }

    public void switchToManageGameMenu()
    {
        menuState = MenuState.MANAGE;
        print("Settings pressed");

    }

    public void switchToMenu(MenuState newMenu)
    {
        menuState = newMenu;

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchToLevelSelctor()
    {
        SceneManager.LoadScene("Level Selector", LoadSceneMode.Single);
    }

    public void DeleteSaveInfo()
    {
        SaveData._instance.ClearSave();
    }


    public void ChangeSelectedItemMenuChange(GameObject newItem)
    {
        es.SetSelectedGameObject(newItem);
    }
}
