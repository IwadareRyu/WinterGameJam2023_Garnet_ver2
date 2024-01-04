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
    [SerializeField] AudioSource _runAudioSource;
    [SerializeField] AudioSource _itemUseAudioSource;
    [SerializeField] AudioClip _audioRun;
    [SerializeField] AudioClip _dreamAudio;
    [SerializeField] AudioClip _boarAudio;
    bool _isWalk;
    bool _bearBool;
    bool _actionBool;
    Rigidbody2D _rb;
    float _x, _y;
    float _tmpx, _tmpy;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tmpy = 1;
        _runAudioSource.clip = _audioRun;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stunStateScripts.StunState == StunState.Normal)
        {
            if (!_bearBool && !_actionBool)
            {
                ///�ړ��n�ݒ�
                //�ړ�
                _x = Input.GetAxisRaw("Horizontal");
                _y = Input.GetAxisRaw("Vertical");
                if (!_runAudioSource.isPlaying && DreamStateScripts.DreamState == DreamState.Normal)
                {
                    _runAudioSource.Play();
                }
                if (_x != 0 || _y != 0)
                {
                    _tmpx = _x;
                    _tmpy = _y;

                    _isWalk = true;
                }   //�ړ������̕ۑ�
                else
                {
                    _isWalk = false;
                    if (_runAudioSource.isPlaying) { _runAudioSource.Stop(); }
                }

                if (DreamStateScripts.DreamState == DreamState.Normal)
                {
                    /// �A�C�e���ݒ�
                    if (Input.GetButtonDown("Fire1") && _bomItem.IsBom)
                    {
                        Bomb();
                    }// �������e

                    if (Input.GetButtonDown("Fire2") && _crakerItem.IsCraker)
                    {
                        StartCoroutine(StunGun());
                        _runAudioSource.Stop();
                    }// �N���b�J�[

                    if (Input.GetButtonDown("Fire3") && _boarInterval.BoarUseBool)
                    {
                        BoarSpeedUp();
                        
                    }// �C�m�V�V�X�s�A�b�v

                    if (Input.GetButtonDown("Skill") && DreamStateScripts.IsCountTimer)
                    {
                        DreamStateScripts.DreamWorld.Invoke();
                        _runAudioSource.Stop();
                        if (_dreamAudio != null) { _itemUseAudioSource.PlayOneShot(_dreamAudio); }
                    }// �����E
                }

            }
        }
        else
        {
            _runAudioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (_stunStateScripts.StunState != StunState.Stun && !_actionBool)
        {
            //�ړ�����
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

    /// <summary>�C�m�V�V���[�h�ɂȂ�Ƃ��ɌĂ΂�郁�\�b�h</summary>
    void BoarSpeedUp()
    {
        _runAudioSource.pitch = 2;
        if (_boarAudio != null) { _itemUseAudioSource.PlayOneShot(_boarAudio); }
        _bearBool = true;
        _runAudioSource.Play();
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

    /// <summary>Stun����Ƃ��ɌĂяo����郁�\�b�h</summary>
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
            _runAudioSource.pitch = 1;
            if (collision.gameObject.TryGetComponent<StunStateScripts>(out var stun))
            {
                stun.ChangeStunState();
            }
            _bearBool = false;
            _boarInterval.ResetBoar();
            _runAudioSource.Stop();
            Debug.Log("SpeedNormal");
        }
        if(collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.InitialPlayerSpawn(transform);
        }
    }

}
