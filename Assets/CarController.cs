using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public Transform[] wheels;
 
    float enginePower = 150.0f;
    float power = 0.0f;
    float brake = 0.0f;
    float steer = 0.0f;
    float maxSteer = 25.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
        steer = Input.GetAxis("Horizontal") * maxSteer;
        brake = Input.GetKey("space") ? GetComponent<Rigidbody>().mass * 0.1f : 0.0f;
        Debug.Log(power);
        GetCollider(0).steerAngle = steer;
        GetCollider(1).steerAngle = steer;

        foreach(var wheel in wheels) {
            UpdateWheel(wheel);
        }


        if (brake > 0.0)
        {
            GetCollider(0).brakeTorque = brake;
            GetCollider(1).brakeTorque = brake;
            GetCollider(2).brakeTorque = brake;
            GetCollider(3).brakeTorque = brake;
            GetCollider(2).motorTorque = 0.0f;
            GetCollider(3).motorTorque = 0.0f;
        }
        else
        {
            GetCollider(0).brakeTorque = 0;
            GetCollider(1).brakeTorque = 0;
            GetCollider(2).brakeTorque = 0;
            GetCollider(3).brakeTorque = 0;
            GetCollider(2).motorTorque = -power;
            GetCollider(3).motorTorque = -power;
        }
    }

    void UpdateWheel(Transform wheel)
    {
        if (Input.GetKey(KeyCode.A))
        {

            wheel.Rotate(-Vector3.up * Time.deltaTime * power);
        }

        else if (Input.GetKey(KeyCode.D))
        {

            wheel.Rotate(Vector3.up * Time.deltaTime * power);
        }
    }

    WheelCollider GetCollider(int n) {
        return wheels[n].gameObject.GetComponent<WheelCollider>();
    }
}
