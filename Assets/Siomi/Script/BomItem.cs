using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomItem : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̎g�p�C���^�[�o��")] float _interval;
    float _timer;
    /// <summary> UI�p�̃A�C�e���C���^�[�o���̊��� </summary>
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
        //�C���^�[�o�������^�C�}�[���������ꍇ�ɏo���B
        if (!_isBom)
            _timer += Time.deltaTime;
        //�^�C�}�[���C���^�[�o�������傫���ꍇ
        if (_timer >= _interval && !_isBom)
        {
            _isBom = true;
            _timer = _interval;
        }
        //UI�̃X���C�_�[�ɕ\������
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
            //���e�𐶐�����
            Instantiate(_bomPrefab, _playerPos.position, Quaternion.identity);
            _timer = 0;
        }
        
    }
}
