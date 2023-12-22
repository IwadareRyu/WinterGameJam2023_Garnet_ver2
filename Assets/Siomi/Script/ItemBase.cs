using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField, Header("アイテムの使用インターバル")] float _interval;
    float _timer;
    /// <summary> UI用のアイテムインターバルの割合 </summary>
    float _uiPercent;

    private void Start()
    {
        _timer = _interval;
    }

    void Update()
    {
        //インターバルよりもタイマーが小さい場合に出す。
        if (!(_timer >= _interval))
            _timer += Time.deltaTime;
        //UIのスライダーに表示する
        UiKousin();
    }

    void UiKousin()
    {
        _uiPercent = _timer / _interval;
    }
    protected abstract void Action();
}
