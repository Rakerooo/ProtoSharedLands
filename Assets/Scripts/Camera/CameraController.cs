using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera cam;
        #region Movement attributes
            [Header("Movement attributes")] [Tooltip("WASD (Shift)")]
            [SerializeField] private float normalSpeed = 1;
            [SerializeField] private float fastSpeed = 3;
            [SerializeField] private float movementTime = 5;

            private Vector3 dragStartPosition, dragCurrentPosition;
            private Vector3 newPosition;
        #endregion
        #region Rotation attributes
            [Header("Rotation attributes")] [Tooltip("QE")]
            [SerializeField] private float rotationSpeed = 1;
            [SerializeField] private float rotationTime = 5;

            private Vector3 rotateStartPosition, rotateCurrentPosition;
            private Quaternion newRotation;
        #endregion
        #region Zoom attributes
            [Header("Zoom attributes")] [Tooltip("RF")]
            [SerializeField] private Vector3 zoomAmount;
            [SerializeField] private float zoomTime = 5;
            [SerializeField] private float scrollMultiplier = 5;
                    
            private Vector3 newZoom;
        #endregion
        
        #region Unity functions
            private void Awake()
            {
                var rigTransform = transform;
                newPosition = rigTransform.position;
                newRotation = rigTransform.rotation;
                newZoom = cam.transform.localPosition;
            }

            private void Update()
            {
                HandleMouseInputs();
                HandleKeyboardInputs();
            }
        #endregion

        #region Input handles
            private void HandleMouseInputs()
            {
                // Movement
                if (Input.GetMouseButtonDown(0))
                {
                    var plane = new Plane(Vector3.up, Vector3.zero);
                    
                    if (cam is null) return;
                    var ray = cam.ScreenPointToRay(Input.mousePosition);
                    
                    if (plane.Raycast(ray, out var entry))
                    {
                        dragStartPosition = ray.GetPoint(entry);
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    var plane = new Plane(Vector3.up, Vector3.zero);
                    
                    if (cam is null) return;
                    var ray = cam.ScreenPointToRay(Input.mousePosition);
                    
                    if (plane.Raycast(ray, out var entry))
                    {
                        dragCurrentPosition = ray.GetPoint(entry);

                        newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                    }
                }
                
                // Rotation
                if (Input.GetMouseButtonDown(2))
                    rotateStartPosition = Input.mousePosition;
                if (Input.GetMouseButton(2))
                {
                    rotateCurrentPosition = Input.mousePosition;
                    var diffRotationPosition = rotateStartPosition - rotateCurrentPosition;
                    rotateStartPosition = rotateCurrentPosition;
                    newRotation *= Quaternion.Euler(Vector3.up * (-diffRotationPosition.x / 5f));
                }
                
                // Zoom
                if (Input.mouseScrollDelta.y != 0 && Utils.IsMouseOverWindow(cam))
                {
                    newZoom += zoomAmount * (Input.mouseScrollDelta.y * scrollMultiplier);
                }
            }
            
            private void HandleKeyboardInputs()
            {
                var movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
                
                // Movement
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    newPosition += transform.forward * movementSpeed;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    newPosition += -transform.right * movementSpeed;
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    newPosition += -transform.forward * movementSpeed;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    newPosition += transform.right * movementSpeed;
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

                // Rotation
                if (Input.GetKey(KeyCode.Q))
                    newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
                if (Input.GetKey(KeyCode.E))
                    newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);
                
                // Zoom
                if (Input.GetKey(KeyCode.R))
                    newZoom += zoomAmount;
                if (Input.GetKey(KeyCode.F))
                    newZoom -= zoomAmount;
                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, newZoom, Time.deltaTime * zoomTime);
            }
        #endregion
    }
}