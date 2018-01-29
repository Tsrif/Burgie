using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour {
	public MenuState menuOption;
	public GameObject menuItem;


    private void OnEnable()
    {
        GameController.changeMenu += menuChanged;

    }

    private void OnDisable()
    {
        GameController.changeMenu -= menuChanged;

    }

    void menuChanged(MenuState newMenu) {
		if(newMenu == menuOption) {
			menuItem.SetActive(true);
		}
		else {
			menuItem.SetActive(false);
		}
	}

}
