using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerItem : MonoBehaviour
{
    [SerializeField, Header("アイテムの使用インターバル")] float _interval;
    float _timer;
    /// <summary> UI用のアイテムインターバルの割合 </summary>
    public float _uiPercent;
    [SerializeField, Header("クラッカーのプレハブ")] GameObject _crackerPrefab;
    [SerializeField, Header("発射点")] GameObject _centerShotPos;
    [SerializeField, Header("横のあたり判定")] float _boxHorizontal = 5f;
    [SerializeField, Header("縦のあたり判定")] float _boxVertical = 5f;
    [SerializeField, Header("クラッカーをインスタンスする位置")] Transform _crackerPos;
    [SerializeField, Header("オーディオソースの設定")] AudioSource _audioSource;
    [SerializeField, Header("出したい音")] AudioClip _audioClip;
    [SerializeField] GameObject _playerDirection;
    [SerializeField] float _attackDelayTime = 1f;
    bool _isCraker;
    public bool IsCraker => _isCraker;

    private void Start()
    {
        _timer = _interval;
        _isCraker = true;
    }

    void Update()
    {
        //インターバルよりもタイマーが小さい場合に出す。
        if (!_isCraker)
            _timer += Time.deltaTime;
        //タイマーがインターバルよりも大きい場合
        if (_timer >= _interval && !_isCraker)
        {
            _isCraker = true;
            _timer = _interval;
        }
        //UIのスライダーに表示する
        UiKousin();
    }

    void UiKousin()
    {
        _uiPercent = _timer / _interval;
    }
    public IEnumerator Action()
    {
        yield return new WaitForSeconds(_attackDelayTime);
        if (_timer >= _interval)
        {
            ////音を出す
            //_audioSource.PlayOneShot(_audioClip);
            //クラッカーの処理を書く
            // 指定範囲のコライダーを全て取得する
            var cols = Physics2D.OverlapBoxAll(_centerShotPos.transform.position, new Vector2(_boxHorizontal, _boxVertical), _centerShotPos.transform.rotation.z);

            //プレイヤーとエネミーを探す
            foreach (var c in cols)
            {
                if (c.TryGetComponent<StunStateScripts>(out var stunState))
                {
                    //スタンさせる
                    stunState.ChangeStunState();
                }
            }
            //クラッカーをインスタンスする
            switch(_playerDirection.transform.rotation.z)
            {
                case -90:
                    GameObject cracker1 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
                    SpriteRenderer spriteRenderer1 = cracker1.GetComponent<SpriteRenderer>();
                    spriteRenderer1.flipX = true;
                    break;
                case 0:
                    GameObject cracker2 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.Euler(0, 0, 90));
                    SpriteRenderer spriteRenderer2 = cracker2.GetComponent<SpriteRenderer>();
                    spriteRenderer2.flipX = true;
                    break;
                case 90:
                    GameObject cracker3 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
                    break;
                case 180:
                    GameObject cracker4 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.Euler(0, 0, 90));
                    break;
            }   
            //Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
            _isCraker = false;
            _timer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_centerShotPos.transform.position, new Vector3(_boxHorizontal, _boxVertical, 0));
    }
}
