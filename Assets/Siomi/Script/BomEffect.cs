using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomEffect : MonoBehaviour
{
    [SerializeField, Header("エフェクトの最大値")] float _maxSize;
    [SerializeField, Header("演出時間")] float _effectTime = 1f;
    [SerializeField] SpriteRenderer _mySpriteRenderer;
    [SerializeField] Sprite[] _sprite;
    int _spriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Sequenceを生成
        var sequence = DOTween.Sequence();
        //Tweenをつなげる
        sequence.Append(transform.DOScale(new Vector3(_maxSize, _maxSize, _maxSize), _effectTime))
            .Append(transform.DOScale(new Vector3(0, 0, 0), _effectTime).OnComplete(() => Destroy(this.gameObject)));
        var sequenceSprite = DOTween.Sequence();
        sequenceSprite.AppendInterval(_effectTime / 3)
            .AppendCallback(() => { SpriteChange(); })
            .AppendInterval(_effectTime / 3)
            .AppendCallback(() => { SpriteChange(); });
    }

    void SpriteChange()
    {
        _spriteIndex++;
        _mySpriteRenderer.sprite = _sprite[_spriteIndex];
    }
}
