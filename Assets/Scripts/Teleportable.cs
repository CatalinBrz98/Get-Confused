using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    private bool isTouchingPortal = false, currentSide;
    private GameObject portal;
    private Portal portalComponent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
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
        currentSide = portalObject.GetComponent<Portal>().GetSide(gameObject.transform.position);
        isTouchingPortal = true;
        portal = portalObject;
        portalComponent = portal.GetComponent<Portal>();
    }

    public void UnSetPortal()
    {
        isTouchingPortal = false;
        portal = null;
        portalComponent = null;
    }
}
