using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportUser : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor = null;

    private RaycastHit hit;

    private Vector3 destinationPos;
    [SerializeField] private TeleportationProvider TeleportationProvider;


    private void Awake() 
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    public void Teleport()
    {
        if (rayInteractor.enabled)
        {
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                if (hit.transform.TryGetComponent<TeleportationArea>(out TeleportationArea teleportationArea))
                {
                    // TeleportationArea area = hit.transform.GetComponent<TeleportationArea>();
                    Vector3 destinationPosition = hit.point;
                    TeleportRequest request = new TeleportRequest()
                    {
                        destinationPosition = hit.point
                    };
                    TeleportationProvider.QueueTeleportRequest(request);
                }
            }
        }
    }
}
