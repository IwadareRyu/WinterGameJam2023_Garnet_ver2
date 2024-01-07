using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSleep : MonoBehaviour
{
    [SerializeField] float _myDestroyTime = 4f;

    public void MoveObject(float speed)
    {
        Destroy(gameObject,_myDestroyTime);
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.left.normalized * speed;
    }
}
