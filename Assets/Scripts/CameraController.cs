using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSensitivity = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float sensitivityX = 100f;
    // public float sensitivityY = 100f;

    private float currentZoom = 10f;
    private float currentX = 0f;
    // private float currentY = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if (Input.GetMouseButton(2))
        {
            Debug.Log("Middle Mouse Clicked!");
            currentX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            // currentY -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentX);
        // transform.RotateAround(target.position, Vector3.right, currentY);
    }
}
