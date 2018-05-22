using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;
using UnityEngine.EventSystems;

public enum GameState {MENU, MENU_TO_PLAYING, PLAYING, PLAYING_TO_MENU, WINNER};
public enum MenuState {NONE, PAUSE_MENU, CHARACTER, MANAGE, TITLE_SCREEN, END_LEVEL};



public class GameController : MonoBehaviour {
    private GameState _gameState;
    private MenuState _menuState;
    public Text gameStateDisplay;
    public Canvas PauseMenuCanvas;
    public static event Action<GameState> currentGameState;

    public Camera2DFollow mainCamera;
    public BurgerController player;

    private GameObject storeSelected;

    public EventSystem es;


    public GameState gameState {
        set {
            _gameState = value;
            if (currentGameState != null) {
                currentGameState(_gameState); //send notification to telll everybody the current state of the game 
            }
        }
        get {
            return _gameState;
        }
    }
    public MenuState menuState {
        set {
            _menuState = value;
            if (changeMenu != null)
            {
                changeMenu(menuState); //send notification to change the menu
                //print(_menuState);
            }
        }
        get {
            return _menuState;
        }
    }

    public static event Action<MenuState> changeMenu;

    private void Awake()
    {
        if (es == null)
        {
            es = (EventSystem)FindObjectOfType(typeof(EventSystem));
        }
        //DontDestroyOnLoad(PauseMenuCanvas); 
    }

    private void OnEnable()
    {
        EndOfLevel.winner += Winner; //subscribe to event
        MenuHandler.firstItemNotif += ChangeSelectedItemMenuChange;
    }
    private void OnDisable()
    {
        EndOfLevel.winner -= Winner;
        MenuHandler.firstItemNotif -= ChangeSelectedItemMenuChange;
    }

    // Use this for initialization
    void Start() {
        switchToPlaying();

    }

    // Update is called once per frame
    void Update() {
       
        switch (gameState) {
            case GameState.MENU:
                if (Input.GetButtonDown("Pause")) {
                    gameState = GameState.MENU_TO_PLAYING;
                }
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
                break;
            case GameState.MENU_TO_PLAYING:
                switchToPlaying();
                break;
            case GameState.PLAYING:
                if (Input.GetButtonDown("Pause")) {
                    gameState = GameState.PLAYING_TO_MENU;
                }
                if (Input.GetButtonDown("Restart"))
                {
                    Restart();
                }
                //if (Input.GetKeyDown(KeyCode.R)) {
                //    ResetPlayerAndCamera();
                //}
                if (Input.GetKeyDown(KeyCode.T))
                {
                    ReturnToTitle();
                }

                break;
            case GameState.PLAYING_TO_MENU:
                switchToMenu();
                break;
            default:
                break;

        }

    }

    public void switchToPlaying() {
        gameState = GameState.PLAYING;
        gameStateDisplay.text = "Playing";
        menuState = MenuState.NONE;
        UnpauseGame();
    }

    public void switchToMenu() {
        gameState = GameState.MENU;
        menuState = MenuState.PAUSE_MENU;
        gameStateDisplay.text = "Menu";
        PauseGame();
    }

    public void switchToCharacterMenu() {
        menuState = MenuState.CHARACTER;

    }

    public void switchToMainMenu() {
        menuState = MenuState.PAUSE_MENU;

    }

    public void switchToManageGameMenu() {
        menuState = MenuState.MANAGE;

    }

    public void switchToEndLevelMenu() {
        menuState = MenuState.END_LEVEL;
    }

    public void switchToMenu(MenuState newMenu) {
        menuState = newMenu;
    }

    public void ResetPlayerAndCamera() {
        player.transform.position = player.originalPosition;
        mainCamera.transform.position = mainCamera.originalPosition;

    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ReturnToTitle() {
        //SceneManager.UnloadSceneAsync("Scene1");
        SceneManager.LoadScene("Title_Screen", LoadSceneMode.Single);
    }

    public void PauseGame() {
        PauseMenuCanvas.enabled = true;
    }

    private void Winner() {
        gameState = GameState.WINNER; //GameState.WINNER just makes it so you can't open up the pause menu after you've finished a level
        switchToEndLevelMenu();
    }

    public void UnpauseGame() {
        PauseMenuCanvas.enabled = false;
    }

    public void ChangeSelectedItemMenuChange(GameObject newItem)
    {
        es.SetSelectedGameObject(newItem);
    }
}
