using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MenuHandler_Title : MonoBehaviour
{
    public MenuState menuOption;
    public GameObject menuItem;
    public GameObject firstItemInMenu;
    public static event Action<GameObject> firstItemNotif; 

  
    private void OnEnable()
    {
        TitleScreen.changeMenu += menuChanged;
    }

    private void OnDisable()
    {
        TitleScreen.changeMenu -= menuChanged;
    }

    void menuChanged(MenuState newMenu)
    {
        if (newMenu == menuOption)
        {
            menuItem.SetActive(true);
            //send first item in the menu to the title controller to change the currently selected button
            if (firstItemNotif != null) {
                firstItemNotif(firstItemInMenu);
            }
        }
        else
        {
            menuItem.SetActive(false);
        }
    }

    

}
