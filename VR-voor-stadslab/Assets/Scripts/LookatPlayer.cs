using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatPlayer : MonoBehaviour
{
    public GameObject xrPlayer;

    void Update()
    {
        if (xrPlayer != null)
        {
            this.transform.LookAt(xrPlayer.transform);
        }
    }
}
