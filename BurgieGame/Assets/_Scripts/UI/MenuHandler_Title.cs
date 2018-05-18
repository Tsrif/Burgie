using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler_Title : MonoBehaviour
{
    public MenuState menuOption;
    public GameObject menuItem;

   

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
        }
        else
        {
            menuItem.SetActive(false);
        }
    }

    

}
