using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTimer : MonoBehaviour
{
    Slider _slider = null;
    [SerializeField] float _interval = 1; // アイテムごとの使用インターバル _timer
    [Tooltip("～Itemがついたオブジェクト")]
    [SerializeField] GameObject _itemObject = null;
    [SerializeField] ItemSliderState _state;
    BomItem _bomItemScript = null;
    CrackerItem _crackerItem;
    BoarInterval _boarInterval;
    DreamStateScripts _dreamStatescripts;
    float _nowInterval = 0; // アイテムごとの_uiPercent

    //テスト用
    //BomTest _bomItem;
    //[SerializeField] GameObject _item1 = null;

    // Start is called before the first frame update
    void Start()
    {
        if (_state == ItemSliderState.Bom)
        {
            _bomItemScript = _itemObject.GetComponent<BomItem>();
        }
        else if(_state == ItemSliderState.Cracer)
        {
            _crackerItem = _itemObject.GetComponent<CrackerItem>();
        }
        else if(_state == ItemSliderState.Boar)
        {
            _boarInterval = _itemObject.GetComponent<BoarInterval>();
        }
        else
        {
            _dreamStatescripts = _itemObject.GetComponent<DreamStateScripts>();
        }
        //テスト用
        //_bomItem = _itemObject.GetComponent<BomTest>();
        //_nowInterval = 1;
        _slider = GetComponent<Slider>();
        //_slider.maxValue = _interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == ItemSliderState.Bom)
        {
            _nowInterval = 1 - _bomItemScript._uiPercent;
        }
        else if (_state == ItemSliderState.Cracer)
        {
            _nowInterval = 1 - _crackerItem._uiPercent;
        }
        else if (_state == ItemSliderState.Boar)
        {
            _nowInterval = 1 - _boarInterval._uiPersent;
        }
        else
        {
            _nowInterval = 1 - _dreamStatescripts._uiPersent;
        }

        _slider.value = _nowInterval;
    }

    enum ItemSliderState
    {
        Bom,
        Cracer,
        Boar,
        Dream,
    }
}
