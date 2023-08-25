using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement Fields")]
    [SerializeField] private float f_MoveSpeed;
    [SerializeField] private float f_Gravity;
    [SerializeField] private float f_JumpForce;

    [SerializeField] private float xRotation, ySensetivity, xSensetivity;

    public Camera cam;

    private CharacterController controller;
    private Vector3 playerVel;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    Vector3 refVel = Vector3.zero;

    private void Update()
    {
        ProcessMove();

        ProcessDownForce();

        ProcessLook();

        if (Input.GetKeyDown(KeyCode.Space))
            ProcessJump();
    }

    public void ProcessLook()
    {
        Vector2 lookdir = Input.mousePosition;

        gameObject.transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + lookdir.x, transform.rotation.z);
        cam.transform.localEulerAngles = new Vector3(transform.rotation.x + lookdir.y, transform.rotation.y, transform.rotation.z);

        Mathf.Clamp(cam.transform.localEulerAngles.y, -90, 90);

        Vector2 lookVector = Input.mousePosition;

        //Calculate rotation for up and down
        xRotation -= (lookVector.y * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //Apply to camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        gameObject.transform.Rotate(Vector3.up * (lookVector.x * Time.deltaTime) * xSensetivity);

    }

    public void ProcessMove()
    {
        controller.Move(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * f_MoveSpeed);
    }

    public Vector3 MoveDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void ProcessDownForce()
    {
        controller.Move(Vector3.down * Time.deltaTime * f_Gravity);
    }

    private void ProcessJump()
    {
        controller.Move(Vector3.up * f_JumpForce * Time.deltaTime);
    }
}
