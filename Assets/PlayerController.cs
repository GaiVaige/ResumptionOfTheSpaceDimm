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

    float rotationX;
    public float lookSpeed;
    public float lookXLimit;

    public GameObject playerCam;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();


        if (canMove)
        {
            DoMovement();
            DoRotation();
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
}
