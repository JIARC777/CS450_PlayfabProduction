using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Clamping")]
    public float minY;
    public float maxY;

    private float rotX;
    private float rotY;
    [HideInInspector]
    public float sens;
    // Start is called before the first frame update
    void Start()
    {
        //Lock cursor so that mouse axis controls camera 
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get the mouse movement inputs
        rotX += Input.GetAxis("Mouse X") * sens;
        rotY += Input.GetAxis("Mouse Y") * sens;
        rotY = Mathf.Clamp(rotY, minY, maxY);
        // camera vertical rotation
        transform.localRotation = Quaternion.Euler(-rotY, 0, 0);
        // camera horizontal rotation
        transform.parent.rotation = Quaternion.Euler(transform.rotation.x, rotX, 0);
    }
}
