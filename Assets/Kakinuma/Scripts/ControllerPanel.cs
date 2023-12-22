using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPanel : MonoBehaviour
{
    [Tooltip("è„Ç©ÇÁWASDÇÃèáÇ≈ì¸ÇÍÇÈ")]
    [SerializeField] GameObject[] _gameObjects;
    Image[] _images = null;
    Color[] _colors;
    float _alpha;
    // Start is called before the first frame update
    void Start()
    {
        _alpha = 0.5f;
        _images = new Image[_gameObjects.Length];
        _colors = new Color[_gameObjects.Length];
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i] = _gameObjects[i].GetComponent<Image>();
            _colors[i] = _images[i].color;
        }
        //_colors = _gameObjects.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.W))
        {
            _colors.a = _alpha + 0.5f;
        }
        else
        {
            _colors.a = _alpha;
        }*/
        //_gameObjects.GetComponent<Image>().color = _colors;
        ControllPanelColorChange();
    }

    void ControllPanelColorChange()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _colors[0].a = _alpha + 0.5f;
        }
        else
        {
            _colors[0].a = _alpha;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _colors[1].a = _alpha + 0.5f;
        }
        else
        {
            _colors[1].a = _alpha;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _colors[2].a = _alpha + 0.5f;
        }
        else
        {
            _colors[2].a = _alpha;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _colors[3].a = _alpha + 0.5f;
        }
        else
        {
            _colors[3].a = _alpha;
        }

        for (int i = 0; i < _images.Length; i++)
        {
            _gameObjects[i].GetComponent<Image>().color = _colors[i];
        }
    }
}
