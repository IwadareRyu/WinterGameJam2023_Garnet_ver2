using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomEffect : MonoBehaviour
{
    [SerializeField, Header("エフェクトの最大値")] float _maxSize;
    [SerializeField, Header("演出時間")] float _effectTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Sequenceを生成
        var sequence = DOTween.Sequence();
        //Tweenをつなげる
        sequence.Append(transform.DOScale(new Vector3(_maxSize, _maxSize, _maxSize), _effectTime))
            .Append(transform.DOScale(new Vector3(0, 0, 0), _effectTime).OnComplete(() => Destroy(this.gameObject)));
    }  
}
