using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneLoader : MonoBehaviour
{
    public int sceneNumber = 0;

    public void LoadScene() {
        SceneManager.LoadScene(sceneNumber);
    }
}
