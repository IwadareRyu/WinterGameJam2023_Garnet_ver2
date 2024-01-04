using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerText : MonoBehaviour
{
    GManager _gManager = null;
    Text _text = null;
    // Start is called before the first frame update
    void Start()
    {
        _gManager = GManager.Instance;
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _gManager.GetGameTimer(_text);
    }
}
