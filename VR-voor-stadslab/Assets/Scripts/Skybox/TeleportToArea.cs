using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToArea : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject xrPlayer;

    public void TeleportPlayer()
    {
        if (xrPlayer != null && teleportPoint != null)
        {
            xrPlayer.transform.position = teleportPoint.position;
        }
    }
}
