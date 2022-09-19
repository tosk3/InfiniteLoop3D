using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private Transform cameraTransfrom;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float fastSpeed;
    [SerializeField] private float movementTime;

    [SerializeField] private float rotationAmount;

    [SerializeField] private Vector3 zoomAmount;

    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private Vector3 newZoom;

    [SerializeField] private Vector3 dragStartPosition;
    [SerializeField] private Vector3 dragCurrentPosition;

    [SerializeField] private Vector3 rotateStartPosition;
    [SerializeField] private Vector3 rotateCurrentPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransfrom.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    private void HandleMouseInput()
    {

        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up,Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);

                //if over Ui dont do the raycast below
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

                //raycast another one
                Physics.Raycast(ray,out RaycastHit hitInfo,float.MaxValue);
                //check hit against layer number for hex
                if (hitInfo.transform == null) 
                {
                    //if hits nothing make it null
                    selectionManager.SetSelection(null);
                    return;
                }
                if (hitInfo.transform.gameObject.layer == 6)
                {
                    //if hits a hex, make it a selection
                    selectionManager.SetSelection(hitInfo.transform.gameObject);
                }
            }

        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }

        }
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up *(-difference.x / 5f));
            //newRotation *= Quaternion.Euler(Vector3.right *(-difference.y / 5f));

        }
    }
    private void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler (Vector3.up * -rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransfrom.localPosition = Vector3.Lerp (cameraTransfrom.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
