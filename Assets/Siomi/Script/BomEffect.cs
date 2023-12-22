using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomEffect : MonoBehaviour
{
    [SerializeField, Header("�G�t�F�N�g�̍ő�l")] float _maxSize;
    [SerializeField, Header("���o����")] float _effectTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Sequence�𐶐�
        var sequence = DOTween.Sequence();
        //Tween���Ȃ���
        sequence.Append(transform.DOScale(new Vector3(_maxSize, _maxSize, _maxSize), _effectTime))
            .Append(transform.DOScale(new Vector3(0, 0, 0), _effectTime).OnComplete(() => Destroy(this.gameObject)));
    }  
}
