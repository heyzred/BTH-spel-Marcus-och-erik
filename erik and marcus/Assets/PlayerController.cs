using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float looksensitivity = 3f;


    private PlayerMotor motor;

    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3d vector 
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov; 
        Vector3 movVertical = transform.forward * zMov;

        //Final movement vector 
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        //Apply movement 
        motor.Move(velocity);

        //Calculation rutation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * looksensitivity;

        //Apply rotation
        motor.Rotate(rotation);


        //Calculation camera rutation as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * looksensitivity;

        //Apply camera rotation
        motor.RotateCamera(cameraRotation);



    }

}
