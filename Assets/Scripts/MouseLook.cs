using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 8f;
    [SerializeField]
    private Transform playerTransform;
    private float yRotation;
    private Portal[] portals;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        portals = FindObjectsOfType<Portal>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        playerTransform.Rotate(Vector3.up * mouseX);
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }

    void OnPreCull()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }
    }
}
