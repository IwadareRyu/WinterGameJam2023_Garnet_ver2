using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sousasetumei : MonoBehaviour
{
    [SerializeField, Header("‘€ìà–¾‚Ìƒpƒlƒ‹")] GameObject _sousaPanel;
    bool _panelflag = false;
    // Start is called before the first frame update
    void Start()
    {
        _sousaPanel.SetActive(_panelflag);
    }

    public void PanelChange()
    {
        _panelflag = !_panelflag;
        _sousaPanel.SetActive(_panelflag);
    }
}
