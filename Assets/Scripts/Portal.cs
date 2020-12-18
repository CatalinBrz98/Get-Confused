using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalEnd;
    private GameObject portalCamera;
    private Plane portalPlane;
    private Camera playerCamera;

    private void Awake()
    {
        portalPlane = new Plane(-gameObject.transform.forward, gameObject.transform.position);
        portalCamera = transform.Find("Portal Camera").gameObject;
        playerCamera = GetPlayerCamera();
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

    public Camera GetPlayerCamera()
    {
        Camera playerCameraVar = null;
        var allCameras = Camera.allCameras;
        for (var i = 0; i < allCameras.Length; i++)
            if (allCameras[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                playerCameraVar = allCameras[i];
        return playerCameraVar;
    }

    public bool GetSide(Vector3 point)
    {
        if (portalPlane.GetSide(point))
            return false;
        return true;
    }

    public void TeleportObject(GameObject other)
    {
        other.transform.position += portalEnd.transform.position - gameObject.transform.position;
        other.GetComponent<Teleportable>().unSetPortal();
    }
}
