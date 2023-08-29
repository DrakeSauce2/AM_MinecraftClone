using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
#pragma warning disable 649

    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [Space]
    [SerializeField] private float hitRange;

    private Transform highlight;

    PlayerController controls;
    PlayerController.PlayerActions playerInput;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerController();
        playerInput = controls.Player;

        // groundMovement.[action].performed += context => do something
        playerInput.WASD.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        playerInput.Jump.performed += _ => movement.OnJumpPressed();

        playerInput.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        playerInput.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);

        GetLookObj();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Hit();
        }

    }

    private void Hit()
    {
        if (GetLookObj())
            Destroy(GetLookObj());
    }
  
    private GameObject GetLookObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hitRange))
        {
            if (hit.transform.CompareTag("Block"))
            {
                hit.transform.gameObject.GetComponent<OcclusionObject>().Select();

                return hit.transform.gameObject;
            }
            else
            {
                hit.transform.gameObject.GetComponent<OcclusionObject>().Deselect();
                highlight = null;
                return null;
            }
        }
        else return null;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}
