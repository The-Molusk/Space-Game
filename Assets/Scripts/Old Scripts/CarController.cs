using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private bool isBreaking;
    private float currentBreakingForce;
    private float steeringAngle;

    [SerializeField] private float motorTorque;
    [SerializeField] private float breakingForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeftWheelColl;
    [SerializeField] private WheelCollider frontRightWheelColl;
    [SerializeField] private WheelCollider backLeftWheelColl;
    [SerializeField] private WheelCollider backRightWheelColl;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    private void FixedUpdate()
    {
        getInput();
        handleMotor();
        handleSteering();
        updateWheels();
    }

    private void getInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKeyDown(KeyCode.Space);
        
    }

    private void handleMotor()
    {
        frontLeftWheelColl.motorTorque = verticalInput * motorTorque;
        frontRightWheelColl.motorTorque = verticalInput * motorTorque;
        currentBreakingForce = isBreaking ? breakingForce : 0f;
        if (isBreaking)
        {
            applyBreaking();
        }
    }

    private void applyBreaking()
    {
        frontRightWheelColl.brakeTorque = currentBreakingForce;
        backRightWheelColl.brakeTorque = currentBreakingForce;
        frontLeftWheelColl.brakeTorque = currentBreakingForce;
        backLeftWheelColl.brakeTorque = currentBreakingForce;
    }
    
    private void handleSteering()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelColl.steerAngle = steeringAngle;
        frontRightWheelColl.steerAngle = steeringAngle;
    }
    private void updateWheels()
    {
        updateSingleWheel(frontLeftWheelColl, frontLeftWheelTransform);
        updateSingleWheel(frontRightWheelColl, frontRightWheelTransform);
        updateSingleWheel(backLeftWheelColl, backLeftWheelTransform);
        updateSingleWheel(backRightWheelColl, backRightWheelTransform);
    }

    private void updateSingleWheel(WheelCollider WheelColl, Transform WheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        WheelColl.GetWorldPose(out pos, out rot);
        WheelTransform.rotation = rot;
        WheelTransform.position = pos;
        
    }
}
