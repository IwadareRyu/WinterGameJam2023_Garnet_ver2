using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomItem : MonoBehaviour
{
    [SerializeField, Header("アイテムの使用インターバル")] float _interval;
    float _timer;
    /// <summary> UI用のアイテムインターバルの割合 </summary>
    public float _uiPercent;
    [SerializeField] GameObject _bomPrefab;
    Transform _playerPos;
    bool _isBom;
    public bool IsBom => _isBom;

    private void Start()
    {
        _timer = _interval;
        _isBom = true;
    }

    void Update()
    {
        //インターバルよりもタイマーが小さい場合に出す。
        if (!_isBom)
            _timer += Time.deltaTime;
        //タイマーがインターバルよりも大きい場合
        if (_timer >= _interval && !_isBom)
        {
            _isBom = true;
            _timer = _interval;
        }
        //UIのスライダーに表示する
        UiKousin();
    }

    void UiKousin()
    {
        _uiPercent = _timer / _interval;
    }
    public void Action()
    {
        if (_timer >= _interval)
        {
            _playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
            _isBom = false;
            //爆弾を生成する
            Instantiate(_bomPrefab, _playerPos.position, Quaternion.identity);
            _timer = 0;
        }
        
    }
}
