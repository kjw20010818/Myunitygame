using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Update() // ������ �Ÿ����� �÷��̾� ����
    {
        transform.position = target.position + offset;
    }
}
