using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{   
    public GameObject[] menuScreens;
    public Transform objectSpawnLocation;

    public void ShowScreen(int index)
    {
        foreach (var screen in menuScreens)
        {
            screen.SetActive(false);
        }

        menuScreens[index].SetActive(true);
    }

    public void SpawnObject(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.position = objectSpawnLocation.position;
    }

    private void OnDisable() 
    {
        ShowScreen(0);
    }
}
