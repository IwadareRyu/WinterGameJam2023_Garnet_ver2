using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScripts : MonoBehaviour
{
    [SerializeField] float _destroyCount = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _destroyCount);
    }
}
