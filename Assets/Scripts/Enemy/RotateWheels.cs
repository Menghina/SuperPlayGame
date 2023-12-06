using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    private Transform[] wheels;
    float rotationSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;i < wheels.Length;i++)
        {
            wheels[i].Rotate(transform.rotation.eulerAngles.x - Time.deltaTime * rotationSpeed, 0, 0);
        }
    }
}
