using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 1;
    void Update()
    {
        transform.Rotate((new Vector3(15, 30, 45) * rotationSpeed) * Time.deltaTime);
    }
}
