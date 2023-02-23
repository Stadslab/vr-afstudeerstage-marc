using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleMenu : MonoBehaviour
{
    public GameObject menu;
    public bool menuOpen = false;

    public void MenuToggle()
    {
        if (menu == null)
            return;

        if (menuOpen)
        {
            menuOpen = false;
            menu.SetActive(false);
        }
        else
        {
            menuOpen = true;
            menu.SetActive(true);
        }
    }
}
