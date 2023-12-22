using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDream : MonoBehaviour
{
    /// <summary> プレイヤーのオブジェクト </summary>
    [SerializeField] GameObject _playerObject;
    /// <summary> プレイヤーのリジッドボディ </summary>
    CapsuleCollider2D _playerCollider;
    /// <summary> プレイヤーのSpriteRendere </summary>
    [SerializeField] SpriteRenderer _playerRendere;
    [SerializeField, Header("薄くするアルファ値"), Range(0, 1)] float _alpha;
    [SerializeField, Header("アルファ値の動作完了時間")] float _alphaTime;

    private void Start()
    {
        //_playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        _playerCollider = _playerObject.GetComponent<CapsuleCollider2D>();
        DreamStateScripts.DreamWorld += DreamStart;
        DreamStateScripts.DreamWorldEnd += DreamEnd;
    }

    private void OnDisable()
    {
        DreamStateScripts.DreamWorld -= DreamStart;
        DreamStateScripts.DreamWorldEnd -= DreamEnd;
    }

    public void DreamStart()
    {
        //プレイヤーのIsTriggerをONにする
        _playerCollider.isTrigger = true;
        //徐々にアルファ値を小さくする
        DOTween.ToAlpha(
            () => _playerRendere.color,
            color => _playerRendere.color = color,
            _alpha, _alphaTime);
    }

    public  void DreamEnd()
    {
        //プレイヤーのIsTriggerをOFFにする
        _playerCollider.isTrigger = false;
        //徐々にアルファ値を大きくする
        DOTween.ToAlpha(
            () => _playerRendere.color,
            color => _playerRendere.color = color,
            1, _alphaTime);
    }
}