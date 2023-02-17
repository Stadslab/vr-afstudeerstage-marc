using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkybox : MonoBehaviour
{
    public GameObject xrOrigin;

    // Update is called once per frame
    void Update()
    {
        if (xrOrigin != null)
        {
            this.gameObject.transform.position = new Vector3(xrOrigin.transform.position.x, this.transform.position.y, xrOrigin.transform.position.z);
        }
    }
}
