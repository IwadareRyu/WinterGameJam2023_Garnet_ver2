using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameScript : MonoBehaviour
{
    GManager _gManager = null;
    // Start is called before the first frame update
    void Start()
    {
        _gManager = GManager.Instance;
        _gManager.ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
