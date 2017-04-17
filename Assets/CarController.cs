using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<GameObject> wheels;
    public GameObject drivingWheel;
    float enginePower = 150.0f;
    float power = 0.0f;
    float brake = 0.0f;
    float steerAngle = 0.0f;
    float maxSteer = 35.0f;
    float friction = 25f;

    // initialization
    void Start()
    {
        Debug.Log("start");
    }

    public List<T> FindComponentsInChildWithTag<T>(string tag) where T : Component
    {
        List<T> children = new List<T>();
        foreach (Transform tr in transform)
        {
            if (tr.tag == tag)
            {
                children.Add(tr.GetComponent<T>());
            }
        }
        return children;
    }

    private bool IsWheel(GameObject gameObject)
    {
        Debug.Log("Tag:" + gameObject.tag);
        return gameObject.tag == "wheel";
    }

    // update per frame
    void Update()
    {
        float steerFactor = Input.GetAxis("Horizontal");
        steerAngle = steerFactor * maxSteer;
        power = Input.GetAxis("Vertical") * enginePower * (1 + Math.Abs(steerFactor)) * Time.deltaTime * 250.0f;
        brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 150.5f : 0.0f;
        Debug.Log(power);

        getFrontWheels().ForEach(w => TurnWheel(w, steerAngle));
        TurnDrivingWheel(steerAngle);

        wheels.ForEach(RotateWheel);
        Debug.Log("update brake: " + brake);
        if (brake > 0.0)
        {
            getRearWheels().ForEach(BrakeWheel);
        }
        else if (power == 0)
        {
            wheels.ForEach(ApplyFriction);
        }

        getFrontWheels().ForEach(w => SetPower(w, power));
        getRearWheels().ForEach(w => SetPower(w, 0.5f * power));
    }

    List<GameObject> getFrontWheels()
    {
        return new List<GameObject> { wheels[0], wheels[1] };
    }

    List<GameObject> getRearWheels()
    {
        return new List<GameObject> { wheels[2], wheels[3] };
    }

    void SetPower(GameObject wheel, float power)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.motorTorque = -power;
        //Debug.Log(power);
        if (power != 0) wheelCollider.brakeTorque = 0;
        //else Debug.Log("friction");
    }

    void ApplyFriction(GameObject wheel)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.brakeTorque = friction;
    }

    void BrakeWheel(GameObject wheel)
    {
        Debug.Log("brake: " + brake);
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.brakeTorque = brake;
        wheelCollider.motorTorque = 0;

    }

    void TurnDrivingWheel(float steerAngle = 0)
    {
        Vector3 wheelAngles = drivingWheel.transform.localEulerAngles;
        drivingWheel.transform.localEulerAngles = new Vector3(-(2*steerAngle - wheelAngles.z), wheelAngles.y, wheelAngles.z);
    }

    void TurnWheel(GameObject wheel, float steerAngle=0)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.steerAngle = steerAngle;
        Vector3 wheelAngles = wheel.transform.localEulerAngles;
        wheel.transform.localEulerAngles = new Vector3(wheelAngles.x, steerAngle - wheelAngles.z, wheelAngles.z);
    }

    void RotateWheel(GameObject wheel)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheel.transform.Rotate(0, -wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0);
    }

    WheelCollider GetCollider(GameObject wheel)
    {
        return wheel.GetComponent<WheelCollider>();
    }
}
