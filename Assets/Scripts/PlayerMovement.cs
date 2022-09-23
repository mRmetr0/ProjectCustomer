using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    private LayerMask mask;
    [SerializeField]
    private GameObject polePos;
    [SerializeField]
    SoundManager sounds;
    [SerializeField]
    Animator playerAnims;
    [SerializeField]
    GameManager gameManager;

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
        gameManager.flagsPlaced = 0;
        HUD = FindObjectOfType<HUDManager>();
        camOrientation = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rotToDir = transform.rotation;
    }

    private void Start () {
        //Make sure the player starts the game standing on the ground:
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask)) {
            if (hit.collider != null) {
                this.transform.position = new Vector3(transform.position.x, hit.point.y + (transform.localScale.y/2)+1, transform.position.z);
            }
        }
    }

    private void Update()
    {
        PlayerInput();
        RotatePlayer();
        FlagPlacement();
        RandomSound();
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            sounds.sfxClips[4].Play();
            playerAnims.SetBool("idle", false);
            playerAnims.SetBool("run", true);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            playerAnims.SetBool("run", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnims.SetBool("run", false);
            playerAnims.SetBool("idle", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            sounds.sfxClips[4].Stop();
        }
    }
    public void RandomSound()
    {
        int i = UnityEngine.Random.Range(0, 10000);
        if(i <= 7)
        {
            sounds.sfxClips[0].Play();
        }
        if(i >= 7 && i <= 15)
        {
            sounds.sfxClips[5].Play();
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * GetSpeed() * 500f * Time.deltaTime, ForceMode.Force);
        rb.AddForce(new Vector3(0, -20f, 0));
    }

    private void FlagPlacement () {
        if (Input.GetKeyDown(KeyCode.E) && flagAmout>0) {
            flagAmout--;
            GameManager.instance.flags++;
            onMineCheck.Invoke();
            Pole.transform.position = polePos.transform.position + poleOffset;
            Instantiate(Pole);
        }
    }

    private float GetSpeed() {
        return Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }

    private void RotatePlayer() {
        if (moveDir != Vector3.zero) {
            rotToDir = Quaternion.LookRotation(moveDir);
            //rotToDir = Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z));
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
