using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracker : MonoBehaviour
{
    [SerializeField] float _destroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
}
