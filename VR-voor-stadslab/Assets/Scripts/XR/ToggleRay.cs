using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class ToggleRay : MonoBehaviour
{
    [Tooltip("Switch even if an object is selected")]
    public bool forceToggle = false;
    [Tooltip("The interactor that is switched to")]
    public XRDirectInteractor directInteractor = null;
    private XRRayInteractor rayInteractor = null;
    private bool isSwitched = false;

    private void Awake() 
    {
        // Get ray interactor component
        rayInteractor = GetComponent<XRRayInteractor>();
        // switch of ray interactors from the start
        SwitchInteractors(false);
    }

    public void ActivateRay()
    {
        if (!TouchingObject() || forceToggle)
            SwitchInteractors(true);
    }

    public void DeactiveRay()
    {
        if (isSwitched)
            SwitchInteractors(false);
    }

    private bool TouchingObject()
    {
        List<XRBaseInteractable> targets = new List<XRBaseInteractable>();
        directInteractor.GetValidTargets(targets);
        return (targets.Count > 0);
    }

    // Switches innteractors on or off
    private void SwitchInteractors(bool value)
    {
        isSwitched = value;
        rayInteractor.enabled = value;
        directInteractor.enabled = !value;
    }
}
