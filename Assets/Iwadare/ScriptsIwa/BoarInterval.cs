using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarInterval : MonoBehaviour
{
    [SerializeField] float _boarInterval = 5f;
    float _boarTime;
    public float _uiPersent = 1;
    bool _boarUseBool;
    public bool BoarUseBool => _boarUseBool;
    // Start is called before the first frame update
    void Start()
    {
        _boarTime = _boarInterval;
        _boarUseBool = true;
        UIUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (_boarUseBool) { return; }

        _boarTime += Time.deltaTime;

        if(_boarTime >= _boarInterval)
        {
            _boarUseBool = true;
            _boarTime = _boarInterval;
        }

        UIUpdate();
    }

    void UIUpdate()
    {
        _uiPersent = _boarTime / _boarInterval;
    }

    public void UseBoar()
    {
        _boarTime = 0;
        UIUpdate();
    }

    public void ResetBoar()
    {
        _boarUseBool = false;
    }
}
