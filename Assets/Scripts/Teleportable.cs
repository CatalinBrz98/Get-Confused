using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    private bool isTouchingPortal = false;
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
        if (isTouchingPortal && portalComponent.GetSide(gameObject.transform.position))
            portalComponent.TeleportObject(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject portalObject = other.gameObject;
        if (portalObject.layer == 11 && !portalObject.GetComponent<Portal>().GetSide(gameObject.transform.position))
            setPortal(portalObject);
    }

    void OnTriggerExit(Collider other)
    {
        GameObject portalObject = other.gameObject;
        if (portalObject.layer == 11 && portalObject == portal)
            unSetPortal();
    }

    void setPortal(GameObject portalObject)
    {
        isTouchingPortal = true;
        portal = portalObject;
        portalComponent = portal.GetComponent<Portal>();
    }

    public void unSetPortal()
    {
        isTouchingPortal = false;
        portal = null;
        portalComponent = null;
    }
}
