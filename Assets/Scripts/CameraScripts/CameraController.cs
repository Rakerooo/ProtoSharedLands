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
            [SerializeField] private float panningMarginWidth = 0.01f;
            [SerializeField] private float panningMarginHeight = 0.01f;
            [SerializeField] private float panningAngleWidth = 0.1f;
            [SerializeField] private float panningAngleHeight = 0.1f;

            private Vector3 dragStartPosition, dragCurrentPosition;
            private Vector3 newPosition;
            private Vector3 currentVelocity;
            private bool dragStartSet;
            
            private float movementSpeed => Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
            private Vector3 view => cam == null ? Vector3.zero : cam.ScreenToViewportPoint(Input.mousePosition) ;
            private bool IsPanningLeft => view.x <= panningMarginWidth;
            private bool IsPanningRight => view.x >= 1 - panningMarginWidth;
            private bool IsPanningTop => view.y >= 1 - panningMarginHeight;
            private bool IsPanningBottom => view.y <= panningMarginHeight;

            private bool IsPanningTopLeft => view.x <= panningAngleWidth && view.y >= 1 - panningAngleHeight;
            private bool IsPanningTopRight => view.x >= 1 - panningAngleWidth && view.y >= 1 - panningAngleHeight;
            private bool IsPanningBottomLeft => view.x <= panningAngleWidth && view.y <= panningAngleHeight;
            private bool IsPanningBottomRight => view.x >= 1 - panningAngleWidth && view.y <= panningAngleHeight;
        #endregion
        #region Rotation attributes
            [Header("Rotation attributes")] [Tooltip("QE")]
            [SerializeField] private float rotationSpeed = 1;
            [SerializeField] private float rotationTime = 5;
            [SerializeField] private float cancelRotationMultiplier = 0.2f;

            private Vector3 rotateStartPosition, rotateCurrentPosition;
            private Quaternion newRotation;
            private Quaternion baseRotation;
            private bool rotatingL, rotatingR;
            private bool rotatingKeyboardL, rotatingKeyboardR, rotatingMouse;
            private bool rotatingKeyboard => rotatingKeyboardL || rotatingKeyboardR;
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
                baseRotation = newRotation;
                oldZoomAmount = zoomAmount;
                if (rotationTime == 0) rotationTime = 0.001f;
                if (zoomTime == 0) zoomTime = 0.001f;
            }

            private void Update()
            {
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
                    if (IsPanningLeft || IsPanningTopLeft || IsPanningBottomLeft) newPosition += -rig.right * movementSpeed;
                    if (IsPanningRight || IsPanningTopRight || IsPanningBottomRight) newPosition += rig.right * movementSpeed;
                    if (IsPanningTop || IsPanningTopLeft || IsPanningTopRight) newPosition += rig.forward * movementSpeed;
                    if (IsPanningBottom || IsPanningBottomLeft || IsPanningBottomRight) newPosition += -rig.forward * movementSpeed;
                }
                var range = Mathf.Lerp(minRange, maxRange, oldZoomAmount == 0 ? 1 : 1 - oldZoomAmount);
                if (Input.GetMouseButtonDown(2))
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
                if (Input.GetMouseButton(2))
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
                if (Input.GetMouseButtonUp(2)) dragStartSet = false;
                
                // Rotation
                if (Input.GetMouseButtonDown(1))
                {
                    rotateStartPosition = Input.mousePosition;
                    newRotation = rig.rotation;
                    if (!rotatingKeyboard) rotatingMouse = true;
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
                    newRotation = baseRotation;
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
                if (!rotatingMouse)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        rotatingKeyboardL = true;
                        newRotation = rig.rotation;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        rotatingKeyboardR = true;
                        newRotation = rig.rotation;
                    }
                }
                if (Input.GetKey(KeyCode.Q))
                    newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
                if (Input.GetKey(KeyCode.E))
                    newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    if (!rotatingKeyboardR) newRotation = baseRotation;
                    rotatingKeyboardL = false;
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    if (!rotatingKeyboardL) newRotation = baseRotation;
                    rotatingKeyboardR = false;
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
            var rotationY = newRotation.eulerAngles.y;
            rotationY = Mathf.Abs(rotationY);
            switch (rotationY)
            {
                case < 360 and > 350 or < 0:
                    rotatingL = true;
                    rotatingR = false;
                    break;
                case > 0 and < 10:
                    rotatingL = false;
                    rotatingR = true;
                    break;
            }

            if (rotatingMouse || rotatingKeyboard)
            {
                if (rotatingL)
                {
                    if (rotationY < 180) rotationY = 180;
                }
                else if (rotatingR)
                {
                    if (rotationY > 180) rotationY = 180;
                }
            }

            newRotation.eulerAngles = new Vector3(newRotation.eulerAngles.x, rotationY);
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