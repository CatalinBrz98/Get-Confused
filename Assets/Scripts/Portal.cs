using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Portal linkedPortal;
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
        if (!isRendering)
            return;
        Matrix4x4 newPortalCameraMatrix = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        portalCamera.transform.SetPositionAndRotation(newPortalCameraMatrix.GetColumn(3), newPortalCameraMatrix.rotation);
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (GetComponent<BoxCollider>().bounds.Contains(playerCamera.transform.position) && !portalPlane.Raycast(cameraRay, out float enter))
            SetRender(false);
        else
            SetRender(true);
    }

    public bool GetSide(Vector3 point)
    {
        return !portalPlane.GetSide(point);
    }

    public void TeleportObject(GameObject other)
    {
        Matrix4x4 newPortalCameraMatrix = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * other.transform.localToWorldMatrix;
        other.transform.SetPositionAndRotation(newPortalCameraMatrix.GetColumn(3), newPortalCameraMatrix.rotation);
        other.GetComponent<Teleportable>().UnSetPortal();
        linkedPortal.Render();
    }

    public void SetRender(bool mode)
    {
        GetComponent<MeshRenderer>().enabled = mode;
        portalCamera.enabled = mode;
        isRendering = mode;
        if (mode)
            portalCamera.Render();
    }
}
