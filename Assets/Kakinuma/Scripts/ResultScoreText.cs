using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreText : MonoBehaviour
{
    GManager _gManager = null;
    Text _text = null;
    [SerializeField] Text _timeText = null;
    [SerializeField] Text _juwelScoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        _gManager = GManager.Instance;
        _text = GetComponent<Text>();
        _gManager.GetScore(_text);
        GManager.Instance.GetJuwelScore(_juwelScoreText);
        GManager.Instance.GetTimeScore(_timeText);
    }
}
