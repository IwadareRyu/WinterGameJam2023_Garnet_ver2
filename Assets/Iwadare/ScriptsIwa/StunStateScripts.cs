using DG.Tweening;
using UnityEngine;

public class StunStateScripts : MonoBehaviour
{
    StunState _stunState;
    public StunState StunState => _stunState;
    [SerializeField] float _stunTime = 5f;
    [SerializeField] Transform _spriteTrans;
    float _time;
    [SerializeField] Animator _anim;

    // Start is called before the first frame update
    private void Awake()
    {
        _stunState = StunState.Normal;
    }

    private void Update()
    {
        if (_stunState == StunState.Stun)
        {
            _time += Time.deltaTime;
        }

        if (_time > _stunTime)
        {
            Debug.Log("Normal");
            _time = 0f;
            if (_anim != null) { _anim.Play("Idle"); }
            _stunState = StunState.Normal;
        }
    }

    public void ExplosionScale()
    {
        Debug.Log("ドカーン！");
        var tmpScale = transform.localScale;
        var expSeq = DOTween.Sequence();
        expSeq.Append(_spriteTrans.DOScale(tmpScale * 3f, _stunTime / 2f / 4f))
            .Append(_spriteTrans.DOScale(tmpScale, _stunTime / 2f / 2f))
            .Append(_spriteTrans.DOScale(tmpScale * 1.5f, _stunTime / 2f / 8f))
            .Append(_spriteTrans.DOScale(tmpScale, _stunTime / 2f / 8f));
        expSeq.Play().SetLink(gameObject);
    }

    /// <summary>Stun状態切り替え</summary>
    public void ChangeStunState()
    {
        Debug.Log("Stun");
        _stunState = StunState.Stun;
        if (_anim != null)
        {
            _anim.Play("Stun");
        }
    }

}

public enum StunState
{
    Normal,
    Stun
}
