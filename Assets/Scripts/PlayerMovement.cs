using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float drag;
    [SerializeField] [Range(1, 10)]
    private float rotationSpeed;
    [SerializeField]
    private int flagAmout;
    [SerializeField]
    private GameObject Pole;
    [SerializeField]
    private GameObject polePosition;

    private float vInput;
    private float hInput;

    private Rigidbody rb;
    private Vector3 moveDir;
    private Vector3 poleOffset = new Vector3(0, 10, 0);
    private Quaternion rotToDir;
    private CameraController camOrientation;
    private HUDManager HUD;
    public static event Action onMineCheck;

    private void Awake()
    {
        HUD = FindObjectOfType<HUDManager>();
        camOrientation = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rotToDir = transform.rotation;
    }

    // private void Start () {
    //     Ray ray = new Ray(transform.position, Vector3.down);
    //     if (Physics.Raycast(ray)) {
    //         this.transform.position = ray.GetPoint();
    //     }
    // }

    private void Update()
    {
        PlayerInput();
        RotatePlayer();
        if (Input.GetKeyDown(KeyCode.E) && flagAmout>0) {
            flagAmout--;
            onMineCheck.Invoke();
            Pole.transform.position = polePosition.transform.position + poleOffset;
            Instantiate(Pole);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * GetSpeed() * 500f * Time.deltaTime, ForceMode.Force);
        rb.AddForce(new Vector3(0, -10f, 0));
    }

    private float GetSpeed() {
        return Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }

    private void RotatePlayer() {
        if (moveDir != Vector3.zero) {
            rotToDir = Quaternion.LookRotation(moveDir);
        }
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, rotToDir, Time.deltaTime * rotationSpeed);
    }

    private void PlayerInput () {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveDir = camOrientation.transform.forward * vInput + camOrientation.transform.right * hInput;
    }
}
