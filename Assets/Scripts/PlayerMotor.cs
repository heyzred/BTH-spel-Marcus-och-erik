using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 85f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;


        private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }
        // Gets a movement Vector
        public void Move (Vector3 _velocity)
    {

        velocity = _velocity;
    }


    // Gets a rotation Vector
    public void Rotate(Vector3 _rotation)
    {

        rotation = _rotation;
    }


    // Gets a rotation Vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {

        cameraRotationX = _cameraRotationX;
    }

    //get a force vector for our thruster
    public void ApplyThruster (Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }


    // Run every physics iteration
    void FixedUpdate()
    {

        PerformMovement();
        PerformRotation();

    }
    //perform movement based on velocity variable
    void PerformMovement()
    {

        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

    }
        //Perform rotation
        void PerformRotation ()
        {

            rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
                     if (cam != null)
                    {
                        // set our rotation and clamp it
                        currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
                    }
        }





    








}
