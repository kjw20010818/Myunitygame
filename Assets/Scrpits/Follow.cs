using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Update() // 고정된 거리에서 플레이어 추적
    {
        transform.position = target.position + offset;
    }
}
