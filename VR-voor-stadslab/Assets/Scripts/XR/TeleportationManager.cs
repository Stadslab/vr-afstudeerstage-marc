using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRDirectInteractor directInteractor;
    [SerializeField] private TeleportationProvider TeleportationProvider;

    private InputAction _thumbstick;

    private bool _isActive;

    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        // var cancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        // cancel.Enable();
        // activate.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        _thumbstick.Enable();
    }

    void Update()
    {
        if (!_isActive)
            return;

        if (_thumbstick.triggered)
            return;
            
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        if (hit.transform.GetComponent<TeleportationArea>())
        {
            TeleportRequest request = new TeleportRequest()
            {
                destinationPosition = hit.point
            };

            TeleportationProvider.QueueTeleportRequest(request);
        }

        rayInteractor.enabled = false;
        _isActive = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!TouchingObject())
        {
            Debug.Log("Active");
            rayInteractor.enabled = true;
            _isActive = true;
        }
    }
    // private void OnTeleportCancel(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Inactive");
    //     rayInteractor.enabled = false;
    //     _isActive = false;
    // }

    private bool TouchingObject()
    {
        List<XRBaseInteractable> targets = new List<XRBaseInteractable>();
        directInteractor.GetValidTargets(targets);
        return (targets.Count > 0);
    }
}
