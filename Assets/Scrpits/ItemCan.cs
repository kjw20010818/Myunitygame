using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCans : MonoBehaviour
{
    public float rotateSpeed;


    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

}
