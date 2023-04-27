using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform camTransform;
        
        #region Movement attributes
            [Header("Movement attributes")] [Tooltip("WASD (Shift)")]
            [SerializeField] private float normalSpeed = 1;
            [SerializeField] private float fastSpeed = 3;
            [SerializeField] private float movementTime = 5;

            private Vector3 newPosition;
        #endregion
        #region Rotation attributes
            [Header("Rotation attributes")] [Tooltip("QE")]
            [SerializeField] private float rotationSpeed = 1;
            [SerializeField] private float rotationTime = 5;
                
            private Quaternion newRotation;
        #endregion
        #region Zoom attributes
            [Header("Zoom attributes")] [Tooltip("RF")]
            [SerializeField] private Vector3 zoomAmount;
            [SerializeField] private float zoomTime = 5;
                    
            private Vector3 newZoom;
        #endregion
        
        #region Unity functions
            private void Awake()
            {
                var rigTransform = transform;
                newPosition = rigTransform.position;
                newRotation = rigTransform.rotation;
                newZoom = camTransform.localPosition;
            }

            private void Update()
            {
                HandleKeyboardInputs();
            }
        #endregion

        #region Input handles
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
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, newZoom, Time.deltaTime * zoomTime);
            }
        #endregion
    }
}