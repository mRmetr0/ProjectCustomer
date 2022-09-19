using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float xRotation;
    [SerializeField][Tooltip("The higher the number, the lower the cam goes")]
    private float CameraLowest = 25;
    [SerializeField][Tooltip("The lower the number, the higher the cam goes")]
    private float CameraHighets = -20;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private Vector3 camDistance;
    private GameObject mainCam;
    private PlayerMovement player;
    private GameObject yRotater;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCam != null) {
            yRotater = new GameObject();
            yRotater.transform.parent = transform;
            mainCam.transform.parent = yRotater.transform;
            mainCam.transform.position = camDistance;
        } else {
            Debug.Log("Main Camera is missing, be sure to add one!");
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MouseInputs();

        transform.position = player.transform.position;

        // X-axis look rotation:
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, CameraHighets, CameraLowest);
        //mainCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);  // <-Works but could be better;
        yRotater.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Y-axis look rotation:
        transform.Rotate(Vector3.up * mouseX); // <-Works but could be better;


    }

    private void MouseInputs() {
        mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
    }
}
