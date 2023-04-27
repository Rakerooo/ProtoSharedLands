using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        #region Movement attributes
        [SerializeField] private float normalSpeed = 1;
        [SerializeField] private float fastSpeed = 3;
        [SerializeField] private float movementTime = 5;

        private Vector3 newPosition;
        #endregion

        #region Unity functions
        private void Start()
        {
            newPosition = transform.position;
        }

        private void Update()
        {
            HandleMovement();
        }
        #endregion

        #region Input handles
        private void HandleMovement()
        {
            var movementSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
            
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += transform.forward * movementSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += -transform.forward * movementSpeed;
            }
            if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += transform.right * movementSpeed;
            }
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += -transform.right * movementSpeed;
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }
        #endregion
    }
}
