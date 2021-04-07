using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalEnd;
    private GameObject portalCamera;
    private Plane portalPlane;
    private Camera playerCamera;
    private bool isRendering = true;

    private void Awake()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (float)(1 * transform.localScale.z / transform.lossyScale.z));
        portalPlane = new Plane(-gameObject.transform.forward, gameObject.transform.position);
        portalCamera = transform.Find("Portal Camera").gameObject;
        playerCamera = Camera.main;
        RenderTexture cameraTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalCamera.GetComponent<Camera>().targetTexture = cameraTexture;
        portalCamera.GetComponent<Camera>().cullingMask -= 1 << LayerMask.NameToLayer("Teleporters");
        Material renderMaterial = new Material(Shader.Find("Unlit/ScreenCutoutShader"));
        renderMaterial.mainTexture = cameraTexture;
        GetComponent<MeshRenderer>().material = renderMaterial;
    }

    //Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        portalCamera.transform.position = portalEnd.transform.position + playerCamera.transform.position - transform.position;
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(transform.rotation, portalEnd.transform.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.transform.forward;
        portalCamera.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

    public bool GetSide(Vector3 point)
    {
        if (portalPlane.GetSide(point))
            return false;
        return true;
    }

    public void TeleportObject(GameObject other)
    {
        portalEnd.GetComponent<Portal>().SetRender(false);
        other.transform.position += portalEnd.transform.position - gameObject.transform.position;
        other.GetComponent<Teleportable>().UnSetPortal();
    }

    public void SetRender(bool mode)
    {
        GetComponent<MeshRenderer>().enabled = mode;
        portalCamera.SetActive(mode);
        isRendering = mode;
    }
}
