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

    float rotationX;
    public float lookSpeed;
    public float lookXLimit;

    public GameObject playerCam;
    DialogueEngine de;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        de = FindObjectOfType<DialogueEngine>();
       
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
            canMove = true;
        }


        GetInput();


        if (canMove)
        {
            DoMovement();
            DoRotation();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForItem();
        }


    }

        public void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void DoMovement()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float totalMoveSpeedH = horizontalInput * moveSpeed;
        float totalMoveSpeedV = verticalInput * moveSpeed;

        moveDirection = (forward * totalMoveSpeedV) + (right * totalMoveSpeedH);
        moveDirection.y -= 9.8f;
        cc.Move(moveDirection * Time.deltaTime);
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

        if(hit.collider.gameObject.GetComponent<DialogueScriptableObject>() != null)
        {
            de.loadedDialogue = hit.collider.gameObject.GetComponent<DialogueScriptableObject>();
            de.gameObject.SetActive(true);
        }

    }
}
