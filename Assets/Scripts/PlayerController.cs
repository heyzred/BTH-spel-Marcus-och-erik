﻿using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]

public class PlayerController : MonoBehaviour {


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;


    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Joint Options:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;
    private PlayerMotor motor;
    private ConfigurableJoint joint;

     void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
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

        float camerarRotationX = xRot * lookSensitivity;

        //Apply Camera rotation
        motor.RotateCamera(camerarRotationX);

        //Caltulate thrusterforce based on player Input
        Vector3 _thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        //apply the thruster force
        motor.ApplyThruster(_thrusterForce);


    }


    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        { mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce };

    }


}
