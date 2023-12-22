using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] StunStateScripts _stunStateScripts;
    [SerializeField] float _speed = 3f;
    [SerializeField] float _boarSpeed = 5f;
    [SerializeField] float _dreamSpeed = 4f;
    [SerializeField] Transform _rotaObj;
    [SerializeField] Transform _cracerInsPos;
    [SerializeField] Cracker _crakerPrefab;
    [SerializeField] CrackerItem _crakerItem;
    [SerializeField] BoarInterval _boarInterval;
    [SerializeField] BomItem _bomItem;
    [SerializeField] Animator _anim;
    AudioSource _audioSource;
    [SerializeField] AudioClip _audioRun;
    bool _isWalk;
    bool _bearBool;
    bool _actionBool;
    Rigidbody2D _rb;
    float _x, _y;
    float _tmpx, _tmpy;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _tmpy = 1;
        _audioSource.clip = _audioRun;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunStateScripts.StunState == StunState.Normal)
        {

            if (!_bearBool && !_actionBool)
            {
                //移動
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
                if (_x != 0 || _y != 0)
                {
                    _tmpx = _x;
                    _tmpy = _y;

                    _isWalk = true;
                }
                else
                {
                    _isWalk = false;
                    if (_audioSource.isPlaying) { _audioSource.Stop(); }
                }

                if (DreamStateScripts.DreamState == DreamState.Normal)
                {
                    if (Input.GetButtonDown("Fire1") && _bomItem.IsBom)
                    {
                        Bomb();
                    }// 時限爆弾

                    if (Input.GetButtonDown("Fire2") && _crakerItem.IsCraker)
                    {
                        StartCoroutine(StunGun());
                    }// クラッカー

                    if (Input.GetButtonDown("Fire3") && _boarInterval.BoarUseBool)
                    {
                        BoarSpeedUp();
                    }// イノシシスピアップ

                    if (Input.GetButtonDown("Skill") && DreamStateScripts.IsCountTimer)
                    {
                        DreamStateScripts.DreamWorld.Invoke();
                    }// 夢世界
                }

            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (_stunStateScripts.StunState != StunState.Stun && !_actionBool)
        {
            //移動処理
            var horizontal = Vector2.right * _x;
            var vertical = Vector2.up * _y;
            if (!_bearBool && DreamStateScripts.DreamState == DreamState.Normal)
            {
                _rb.velocity = horizontal.normalized * _speed + vertical.normalized * _speed;
            }
            else if (_bearBool)
            {
                _rb.velocity = horizontal.normalized * _boarSpeed + vertical.normalized * _boarSpeed;
            }
            else
            {
                _rb.velocity = horizontal.normalized * _dreamSpeed + vertical.normalized * _dreamSpeed;
            }
            DirectionTarget();
            _anim.SetFloat("x", _tmpx);
            _anim.SetFloat("y", _tmpy);
            _anim.SetBool("walk", _isWalk);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    void DirectionTarget()
    {
        var rota = _rotaObj.eulerAngles;
        if (_tmpx != 0)
        {
            if (_tmpx > 0)
            {
                rota.z = 0f;
            }
            else
            {
                rota.z = 180f;
            }
        }
        else
        {
            if (_tmpy > 0)
            {
                rota.z = 90f;
            }
            else
            {
                rota.z = 270f;
            }
        }
        _rotaObj.eulerAngles = rota;
    }

    void Bomb()
    {
        _bomItem.Action();
    }

    /// <summary>イノシシモードになるときに呼ばれるメソッド</summary>
    void BoarSpeedUp()
    {

        _bearBool = true;
        _audioSource.Play();
        _x = _tmpx;
        _y = _tmpy;
        Debug.Log("Boar");
        _boarInterval.UseBoar();
    }

    IEnumerator StunGun()
    {
        _actionBool = true;
        _anim.SetBool("Action", _actionBool);
        yield return StartCoroutine(_crakerItem.Action());
        _actionBool = false;
        _anim.SetBool("Action", _actionBool);
    }

    /// <summary>Stunするときに呼び出されるメソッド</summary>
    void Stun()
    {
        _stunStateScripts.ChangeStunState();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Jewel>(out var jewel))
        {
            jewel.JewelFound();            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bearBool)
        {
            Stun();
            if (collision.gameObject.TryGetComponent<StunStateScripts>(out var stun))
            {
                stun.ChangeStunState();
            }
            _bearBool = false;
            _boarInterval.ResetBoar();
            _audioSource.Stop();
            Debug.Log("SpeedNormal");
        }
    }

}
