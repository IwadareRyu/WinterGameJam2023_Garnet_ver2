using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDream : MonoBehaviour
{
    /// <summary> �v���C���[�̃I�u�W�F�N�g </summary>
    [SerializeField] GameObject _playerObject;
    /// <summary> �v���C���[�̃��W�b�h�{�f�B </summary>
    CapsuleCollider2D _playerCollider;
    /// <summary> �v���C���[��SpriteRendere </summary>
    [SerializeField] SpriteRenderer _playerRendere;
    [SerializeField, Header("��������A���t�@�l"), Range(0, 1)] float _alpha;
    [SerializeField, Header("�A���t�@�l�̓��슮������")] float _alphaTime;

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
        //�v���C���[��IsTrigger��ON�ɂ���
        _playerCollider.isTrigger = true;
        //���X�ɃA���t�@�l������������
        DOTween.ToAlpha(
            () => _playerRendere.color,
            color => _playerRendere.color = color,
            _alpha, _alphaTime);
    }

    public  void DreamEnd()
    {
        //�v���C���[��IsTrigger��OFF�ɂ���
        _playerCollider.isTrigger = false;
        //���X�ɃA���t�@�l��傫������
        DOTween.ToAlpha(
            () => _playerRendere.color,
            color => _playerRendere.color = color,
            1, _alphaTime);
    }
}