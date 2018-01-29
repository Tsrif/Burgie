using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public enum GameState { MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU };
//public enum MenuState { NONE, PAUSE_MENU, CHARACTER, GAME, TITLE_SCREEN };



public class TitleScreen : MonoBehaviour
{
    private GameState _gameState;
    private MenuState _menuState;
    public Text gameStateDisplay;
    public Canvas PauseMenuCanvas;

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
    public void QuitGame() {
        Application.Quit();
    }

    public void SwitchToLevelSelctor() {
        SceneManager.LoadScene("Level Selector", LoadSceneMode.Single);
    }
}
