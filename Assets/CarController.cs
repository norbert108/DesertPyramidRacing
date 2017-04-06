using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<GameObject> wheels;
    float enginePower = 150.0f;
    float power = 0.0f;
    float brake = 0.0f;
    float steerAngle = 0.0f;
    float maxSteer = 25.0f;
    float friction = 0.2f;

    // initialization
    void Start()
    {
        /*wheels = GetComponentsInChildren<Transform>().ToList()
            .Select(t => t.gameObject)
            .Where(IsWheel).ToList();*/
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
        power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
        steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.1f : 0.0f;
        Debug.Log(power);

        getFrontWheels().ForEach(w => TurnWheel(w, steerAngle));

        wheels.ForEach(RotateWheel);

        if (brake > 0.0)
        {
            wheels.ForEach(BrakeWheel);
        }
        else if (power == 0)
        {
            wheels.ForEach(ApplyFriction);
        }

        getFrontWheels().ForEach(SetPower);
    }

    List<GameObject> getFrontWheels()
    {
        return new List<GameObject> { wheels[0], wheels[1] };
    }

    void SetPower(GameObject wheel)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.motorTorque = power;
        if (power > 0) wheelCollider.brakeTorque = 0;
    }

    void ApplyFriction(GameObject wheel)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.brakeTorque = friction;
    }

    void BrakeWheel(GameObject wheel)
    {
        WheelCollider wheelCollider = GetCollider(wheel);
        wheelCollider.brakeTorque = brake;
        wheelCollider.motorTorque = 0;

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
        wheel.transform.Rotate(0, wheelCollider.rpm / 60 * 360 * Time.deltaTime, 0);
    }

    WheelCollider GetCollider(GameObject wheel)
    {
        return gameObject.GetComponentInChildren<WheelCollider>();
    }
}
