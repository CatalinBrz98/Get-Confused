using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    private Camera portalCamera;
    private Plane portalPlane;
    private Camera playerCamera;
    private bool isRendering = true;

    private void Awake()
    {
        portalPlane = new Plane(-gameObject.transform.forward, gameObject.transform.position);
        portalCamera = GetComponentInChildren<Camera>();
        portalCamera.enabled = false;
        playerCamera = Camera.main;
        RenderTexture cameraTexture = new RenderTexture(Screen.width, Screen.height, 24);
        portalCamera.targetTexture = cameraTexture;
        portalCamera.cullingMask -= 1 << LayerMask.NameToLayer("Teleporters");
        Material renderMaterial = new Material(Shader.Find("Unlit/ScreenCutoutShader"));
        renderMaterial.mainTexture = cameraTexture;
        GetComponent<MeshRenderer>().material = renderMaterial;
    }

    // Update is called once per frame
    public void Render()
    {
        Matrix4x4 newPortalCameraMatrix = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        portalCamera.transform.SetPositionAndRotation(newPortalCameraMatrix.GetColumn(3), newPortalCameraMatrix.rotation);
        Plane cameraPlane = new Plane(-playerCamera.transform.forward, playerCamera.transform.position);
        if (GetComponent<BoxCollider>().bounds.Contains(playerCamera.transform.position) && cameraPlane.GetSide(transform.position))
            SetRender(false);
        else
            SetRender(true);
    }

    public bool GetSide(Vector3 point)
    {
        if (portalPlane.GetSide(point))
            return false;
        return true;
    }

    public void TeleportObject(GameObject other)
    {
        other.transform.position += linkedPortal.transform.position - gameObject.transform.position;
        other.GetComponent<Teleportable>().UnSetPortal();
    }

    public void SetRender(bool mode)
    {
        GetComponent<MeshRenderer>().enabled = mode;
        portalCamera.enabled = mode;
        isRendering = mode;
    }
}
