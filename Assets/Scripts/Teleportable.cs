using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    private bool isTouchingPortal = false, currentSide;
    private GameObject portal;
    private Portal portalComponent;

    void LateUpdate()
    {
        if (isTouchingPortal && currentSide != portalComponent.GetSide(gameObject.transform.position))
            portalComponent.TeleportObject(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Teleporters"))
            SetPortal(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Teleporters") && other.gameObject == portal)
        {
            portalComponent.SetRender(true);
            UnSetPortal();
        }
    }

    void SetPortal(GameObject portalObject)
    {
        isTouchingPortal = true;
        portal = portalObject;
        portalComponent = portal.GetComponent<Portal>();
        currentSide = portalComponent.GetSide(gameObject.transform.position);
    }

    public void UnSetPortal()
    {
        isTouchingPortal = false;
        portal = null;
        portalComponent = null;
    }
}
