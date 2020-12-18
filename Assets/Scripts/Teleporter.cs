using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private GameObject firstSide, secondSide;
    public GameObject teleporterEnd;
    public bool invertDirection = false;
    private Vector3 teleporterDirection;
    private Plane teleporterPlane;

    //Start is called before the first frame update
    void Start()
    {
        Teleporter teleporterComponent = gameObject.GetComponent<Teleporter>();
        BoxCollider teleporterBoxCollider = gameObject.GetComponent<BoxCollider>();
        teleporterDirection = Vector3.Cross((teleporterBoxCollider.bounds.max - teleporterBoxCollider.bounds.min).normalized, Vector3.up).normalized * teleporterComponent.getDirection();
        teleporterPlane = new Plane(teleporterDirection, teleporterBoxCollider.bounds.center);
        firstSide = gameObject.transform.Find("First Side").gameObject;
        secondSide = gameObject.transform.Find("Second Side").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getDirection()
    {
        if (invertDirection)
            return 1;
        return -1;
    }

    public bool getSide(Vector3 point)
    {
        if (teleporterPlane.GetSide(point))
            return true;
        return false;
    }
}
