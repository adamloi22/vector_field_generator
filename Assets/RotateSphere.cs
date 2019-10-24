using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    public float rotation_rate = 10;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotate_axis = new Vector3(0, 1, 0);
        Vector3 rotate_amount = rotate_axis * rotation_rate * Time.deltaTime;
        transform.Rotate(rotate_amount);
    }
}
