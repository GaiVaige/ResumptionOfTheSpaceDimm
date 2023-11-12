using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    public float moveSpeed;
    Vector3 moveDirection;
    Vector3 mousePosition;
    CharacterController cc;
    public bool canMove;
    public float checkDistance;

    public float sprintRate;
    float currentSprintRate;
    public bool isSprinting;


    float rotationX;
    public float lookSpeed;
    public float lookXLimit;

    public GameObject playerCam;
    public Camera pc;
    DialogueEngine de;
    PlayerInventory npc;
    public bool isInDialogue;

    public SoundManager sm;
    public AudioSource walking;
    public AudioSource running;
    public AudioSource itemPickup;

    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>().gameObject;
        npc = GetComponent<PlayerInventory>();
        pc = playerCam.GetComponent<Camera>();
        sm = FindObjectOfType<SoundManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        de = FindObjectOfType<DialogueEngine>();
        de.gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {


        if (de.isActiveAndEnabled)
        {
            canMove = false;
        }
        else
        {
            isInDialogue = false;
            canMove = true;
        }


        GetInput();


        if (canMove)
        {
            DoMovement();
            DoRotation();



        }


        if (Input.GetKeyDown(KeyCode.E) && !isInDialogue)
        {
            CheckForItem();
        }






    }

        public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");



        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }



        if((horizontalInput != 0 || verticalInput != 0) && canMove)
        {
            walking.enabled = true;
        }
        else
        {
            walking.enabled = false;
        }
       
    }

    public void DoMovement()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float totalMoveSpeedH = horizontalInput * moveSpeed;
        float totalMoveSpeedV = verticalInput * moveSpeed;

        moveDirection = (forward * totalMoveSpeedV) + (right * totalMoveSpeedH);
        moveDirection.y -= 9.8f;


        if (isSprinting)
        {
            currentSprintRate = sprintRate;
        }
        else
        {
            currentSprintRate = 1;
        }




        cc.Move(moveDirection * currentSprintRate * Time.deltaTime);

    }

    public void DoRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);


    }

    public void CheckForItem()
    {

        RaycastHit hit;


        Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, checkDistance, LayerMask.GetMask("Interactable"));


        if (hit.collider != null)
        {

            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 100f, Color.red);


            if (hit.collider.gameObject.GetComponent<ScriptableObjectContainer>())
            {
                hit.collider.gameObject.GetComponent<ScriptableObjectContainer>().counter++;

                de.gameObject.SetActive(true);
                de.loadedDialogue = hit.collider.gameObject.GetComponent<ScriptableObjectContainer>().dso;
                if (hit.collider.gameObject.GetComponent<ScriptableObjectContainer>().dso.itemToAdd != null)
                {
                    if (npc.playerItems.Contains(hit.collider.gameObject.GetComponent<ScriptableObjectContainer>().dso.itemToAdd.gameObject) == false)
                    {
                        npc.playerItems.Add(hit.collider.gameObject.GetComponent<ScriptableObjectContainer>().dso.itemToAdd.gameObject);
                    }

                }
                de.LoadNewDialogue();
                isInDialogue = true;

            }
            else if (hit.collider.gameObject.GetComponent<DoorCheck>())
            {
                hit.collider.gameObject.GetComponent<DoorCheck>().OpenDoor();
            }
            else
            {
                Debug.Log("Help");
            }
        }



    }
}
