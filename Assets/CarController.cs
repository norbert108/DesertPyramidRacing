﻿using UnityEngine;
using System.Collections.Generic;

public class CarController: MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        WheelHit wheelHit;
        float leftWheelTravel = 1.0f;
        float rightWheelTravel = 1.0f;

        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

            // suspension bar
            bool groundedLeft = axleInfo.leftWheel.GetGroundHit(out wheelHit);
            if (groundedLeft) {
                leftWheelTravel = (-axleInfo.leftWheel.transform.InverseTransformPoint(wheelHit.point).y - axleInfo.leftWheel.radius) / axleInfo.leftWheel.suspensionDistance;
            } else {
                leftWheelTravel = 1.0f;
            }

            bool groundedRight = axleInfo.rightWheel.GetGroundHit(out wheelHit);
            if (groundedRight) {
                rightWheelTravel = (-axleInfo.rightWheel.transform.InverseTransformPoint(wheelHit.point).y - axleInfo.rightWheel.radius) / axleInfo.rightWheel.suspensionDistance;
            } else {
                rightWheelTravel = 1.0f;
            }

            float antiRollForce = (leftWheelTravel - rightWheelTravel) * axleInfo.leftWheel.suspensionSpring.spring;
            if (groundedLeft) GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.leftWheel.transform.up * -antiRollForce, axleInfo.leftWheel.transform.position);
            if (groundedRight) GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.rightWheel.transform.up * antiRollForce, axleInfo.rightWheel.transform.position);
        }
    }

    // finds the corresponding visual wheel and correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}