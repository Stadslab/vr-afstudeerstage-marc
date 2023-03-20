using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleMenu : MonoBehaviour
{
    public GameObject menu;
    public bool menuOpen = false;

    public Transform menuPosition;

    public void MenuToggle()
    {
        if (menu == null || menuPosition == null)
            return;

        if (menuOpen)
        {
            menuOpen = false;
            menu.SetActive(false);
        }
        else
        {
            menu.transform.position = menuPosition.position;
            menu.transform.rotation = new Quaternion(menuPosition.rotation.x, menuPosition.rotation.y, 0, menuPosition.rotation.w);
            menuOpen = true;
            menu.SetActive(true);
        }
    }
}
