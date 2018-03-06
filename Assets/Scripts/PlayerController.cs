using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;


    private PlayerMotor motor;

     void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

     void Update()
    {
        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");


        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        // Final movement vector
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        //apply movement
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (Turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(rotation);


        //Calculate Camera rotation as a 3D vector (Turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 camerarRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Apply Camera rotation
        motor.RotateCamera(camerarRotation);

    }
}
