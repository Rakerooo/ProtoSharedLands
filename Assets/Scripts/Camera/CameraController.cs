using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera cam;
        [SerializeField] private Transform rig;
        #region Movement attributes
            [Header("Movement attributes")] [Tooltip("WASD (Shift)")]
            [SerializeField] private float normalSpeed = 1;
            [SerializeField] private float fastSpeed = 3;
            [SerializeField] private float movementTime = 5;
            [SerializeField] private float maxVelocity = 500;
            [SerializeField] private float maxRange = 2500;
            [SerializeField] private float minRange = 250;
            [SerializeField] private float panningMarginLeft = 0.1f;
            [SerializeField] private float panningMarginRight = 0.1f;
            [SerializeField] private float panningMarginTop = 0.075f;
            [SerializeField] private float panningMarginBottom = 0.075f;

            private Vector3 dragStartPosition, dragCurrentPosition;
            private Vector3 newPosition;
            private Vector3 currentVelocity;
            private bool dragStartSet;
            
            private float movementSpeed => Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
            private Vector3 view => cam == null ? Vector3.zero : cam.ScreenToViewportPoint(Input.mousePosition) ;
            private bool IsPanningLeft => view.x <= panningMarginLeft && view.x >= 0;
            private bool IsPanningRight => view.x >= 1 - panningMarginRight && view.x <= 1;
            private bool IsPanningTop => view.y >= 1 - panningMarginTop && view.y <= 1;
            private bool IsPanningBottom => view.y <= panningMarginBottom && view.y >= 0;
        #endregion
        #region Rotation attributes
            [Header("Rotation attributes")] [Tooltip("QE")]
            [SerializeField] private float rotationSpeed = 1;
            [SerializeField] private float rotationTime = 5;
            [SerializeField] private float cancelRotationMultiplier = 0.2f;

            private Vector3 rotateStartPosition, rotateCurrentPosition;
            private Quaternion newRotation;
            private Quaternion oldRotation;
            private bool rotatingKeyboard, rotatingMouse;
        #endregion
        #region Zoom attributes
            [Header("Zoom attributes")] [Tooltip("RF")]
            [SerializeField] private float zoomForce = 0.05f;
            [SerializeField] private float zoomTime = 5;
            [SerializeField] private float scrollMultiplier = 5;
            [SerializeField] private Transform start;
            [SerializeField] private Transform end;
            [SerializeField] [Range(0, 1)] private float zoomAmount;

            private float oldZoomAmount;
            private Transform currentTransform;
        #endregion
        #region Clamping
            [Header("Clamping")] [Tooltip("North is forward (Z)")]
            [SerializeField] private Transform maxNorth;
            [SerializeField] private Transform maxSouth;
            [SerializeField] private Transform maxEast;
            [SerializeField] private Transform maxWest;
        #endregion
        
        #region Unity functions
            private void Awake()
            {
                newPosition = rig.position;
                newRotation = rig.rotation;
                oldZoomAmount = zoomAmount;
                if (rotationTime == 0) rotationTime = 0.001f;
                if (zoomTime == 0) zoomTime = 0.001f;
            }

            private void Update()
            {
                Debug.Log($"{view.x} {view.y} {view.z} : {IsPanningLeft}, {IsPanningRight}, {IsPanningTop}, {IsPanningBottom} ({Utils.IsMouseOverWindow(cam)})");
                HandleMouseInputs();
                HandleKeyboardInputs();
                ProcessInputs();
            }
        #endregion

        #region Input handles
            private void HandleMouseInputs()
            {
                // Movement
                if (!dragStartSet && !rotatingMouse)
                {
                    if (IsPanningLeft) newPosition += -rig.right * movementSpeed;
                    if (IsPanningRight) newPosition += rig.right * movementSpeed;
                    if (IsPanningTop) newPosition += rig.forward * movementSpeed;
                    if (IsPanningBottom) newPosition += -rig.forward * movementSpeed;
                }
                var range = Mathf.Lerp(minRange, maxRange, oldZoomAmount == 0 ? 1 : 1 - oldZoomAmount);
                if (Input.GetMouseButtonDown(0))
                {
                    var plane = new Plane(Vector3.up, Vector3.zero);
                    
                    if (cam is null) return;
                    var ray = cam.ScreenPointToRay(Input.mousePosition);
                    
                    if (plane.Raycast(ray, out var entry))
                    {
                        if (entry > range) return;
                        
                        dragStartPosition = ray.GetPoint(entry);
                        dragStartSet = true;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    var plane = new Plane(Vector3.up, Vector3.zero);
                    
                    if (cam is null) return;
                    var ray = cam.ScreenPointToRay(Input.mousePosition);
                    
                    if (plane.Raycast(ray, out var entry))
                    {
                        if (entry > range) return;
                        
                        if (dragStartSet == false)
                        {
                            dragStartPosition = ray.GetPoint(entry);
                            dragStartSet = true;
                            return;
                        }
                        
                        dragCurrentPosition = ray.GetPoint(entry);

                        var angleFactor = 1 - Mathf.Clamp01(Mathf.Exp(-cam.transform.eulerAngles.x));
                        newPosition = (rig.position + dragStartPosition - dragCurrentPosition) * angleFactor;
                    }
                }
                if (Input.GetMouseButtonUp(0)) dragStartSet = false;
                
                // Rotation
                if (Input.GetMouseButtonDown(1))
                {
                    rotateStartPosition = Input.mousePosition;
                    if (!rotatingKeyboard)
                    {
                        oldRotation = newRotation;
                        rotatingMouse = true;
                    }
                }
                if (Input.GetMouseButton(1))
                {
                    rotateCurrentPosition = Input.mousePosition;
                    var diffRotationPosition = rotateStartPosition - rotateCurrentPosition;
                    rotateStartPosition = rotateCurrentPosition;
                    newRotation *= Quaternion.Euler(Vector3.up * (-diffRotationPosition.x / 5f));
                }
                if (Input.GetMouseButtonUp(1))
                {
                    newRotation = oldRotation;
                    rotatingMouse = false;
                }
                
                // Zoom
                if (Input.mouseScrollDelta.y != 0 && Utils.IsMouseOverWindow(cam)) zoomAmount += zoomForce * (Input.mouseScrollDelta.y * scrollMultiplier * Time.deltaTime);
            }
            
            private void HandleKeyboardInputs()
            {
                // Movement
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    newPosition += rig.forward * movementSpeed;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    newPosition += -rig.right * movementSpeed;
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    newPosition += -rig.forward * movementSpeed;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    newPosition += rig.right * movementSpeed;

                // Rotation
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) && !rotatingMouse)
                {
                    oldRotation = newRotation;
                    rotatingKeyboard = true;
                }
                if (Input.GetKey(KeyCode.Q))
                    newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
                if (Input.GetKey(KeyCode.E))
                    newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
                if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
                {
                    newRotation = oldRotation;
                    rotatingKeyboard = false;
                }
                
                // Zoom
                if (Input.GetKey(KeyCode.R))
                    zoomAmount += zoomForce * Time.deltaTime;
                if (Input.GetKey(KeyCode.F))
                    zoomAmount -= zoomForce * Time.deltaTime;
            }
        #endregion
        
        private void ProcessInputs()
        {
            // Movement
            newPosition.x = Mathf.Clamp(newPosition.x, maxWest.position.x, maxEast.position.x);
            newPosition.z = Mathf.Clamp(newPosition.z, maxSouth.position.z, maxNorth.position.z);
            rig.position = Vector3.SmoothDamp(rig.position, newPosition, ref currentVelocity, movementTime, maxVelocity);
            
            // Rotation
            rig.rotation = Quaternion.Lerp(rig.rotation, newRotation, Time.deltaTime / rotationTime * (rotatingMouse || rotatingKeyboard ? 1 : cancelRotationMultiplier));
            
            // Zoom
            oldZoomAmount = Mathf.Lerp(oldZoomAmount, zoomAmount, Time.deltaTime / zoomTime);
            
            cam.transform.localPosition = Vector3.Lerp(end.localPosition, start.localPosition, oldZoomAmount);
            var newZoomRotation = Quaternion.Lerp(end.rotation, start.rotation, oldZoomAmount);
            
            var camTransform = cam.transform;
            var camAngles = camTransform.eulerAngles;
            camTransform.eulerAngles = new Vector3(newZoomRotation.eulerAngles.x, camAngles.y, camAngles.z);
            
            if (camTransform.localPosition == start.localPosition || camTransform.localPosition == end.localPosition)
                zoomAmount = Mathf.Clamp(zoomAmount, 0, 1);
        }
    }
}