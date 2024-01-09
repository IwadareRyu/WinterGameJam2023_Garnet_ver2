using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackerItem : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���̎g�p�C���^�[�o��")] float _interval;
    float _timer;
    /// <summary> UI�p�̃A�C�e���C���^�[�o���̊��� </summary>
    public float _uiPercent;
    [SerializeField, Header("�N���b�J�[�̃v���n�u")] GameObject _crackerPrefab;
    [SerializeField,Header("������̃I�u�W�F�N�g")] Transform _confettiTransform;
    [SerializeField, Header("���˓_")] GameObject _centerShotPos;
    [SerializeField, Header("���̂����蔻��")] float _boxHorizontal = 5f;
    [SerializeField, Header("�c�̂����蔻��")] float _boxVertical = 5f;
    [SerializeField, Header("�N���b�J�[���C���X�^���X����ʒu")] Transform _crackerPos;
    [SerializeField, Header("�I�[�f�B�I�\�[�X�̐ݒ�")] AudioSource _audioSource;
    [SerializeField, Header("�o��������")] AudioClip _audioClip;
    [SerializeField] GameObject _playerDirection;
    [SerializeField] float _attackDelayTime = 1f;
    bool _isCraker;
    float _tmpConfettiX = 0f;
    public bool IsCraker => _isCraker;

    private void Start()
    {
        _timer = _interval;
        _isCraker = true;
        if (_confettiTransform != null)
        {
            var scale = _confettiTransform.transform.localScale;
            _tmpConfettiX = scale.x;
            scale.x = 0f;
            _confettiTransform.localScale = scale;
        }
    }

    void Update()
    {
        //�C���^�[�o�������^�C�}�[���������ꍇ�ɏo���B
        if (!_isCraker)
            _timer += Time.deltaTime;
        //�^�C�}�[���C���^�[�o�������傫���ꍇ
        if (_timer >= _interval && !_isCraker)
        {
            _isCraker = true;
            _timer = _interval;
        }
        //UI�̃X���C�_�[�ɕ\������
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
            ////�����o��
            _audioSource.PlayOneShot(_audioClip);
            //�N���b�J�[�̏���������
            Collider2D[] cols = null;
            // �w��͈͂̃R���C�_�[��S�Ď擾����
            if (_playerDirection.transform.rotation.z > 0f && _playerDirection.transform.rotation.z < 1f)
            {
                cols = Physics2D.OverlapBoxAll(_centerShotPos.transform.position, new Vector2(_boxVertical, _boxHorizontal), _centerShotPos.transform.rotation.z);
            }
            else
            {
                cols = Physics2D.OverlapBoxAll(_centerShotPos.transform.position, new Vector2(_boxHorizontal, _boxVertical), _centerShotPos.transform.rotation.z);
            }
            //�v���C���[�ƃG�l�~�[��T��
            foreach (var c in cols)
            {
                if (c.TryGetComponent<StunStateScripts>(out var stunState))
                {
                    //�X�^��������
                    stunState.ChangeStunState();
                }
            }

            GameObject cracker1 = Instantiate(_crackerPrefab, _crackerPos.position, _playerDirection.transform.rotation);
            SpriteRenderer spriteRenderer1 = cracker1.GetComponent<SpriteRenderer>();
            spriteRenderer1.flipX = true;
            if(spriteRenderer1.transform.rotation.z == 1f)
            {
                spriteRenderer1.flipY = true;
            }
            if (_confettiTransform != null)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(_confettiTransform.DOScaleX(_tmpConfettiX, 0.5f))
                    .AppendInterval(1f)
                    .OnComplete(() => _confettiTransform.DOScaleX(0f, 0.1f));
                sequence.Play().SetLink(_confettiTransform.gameObject);
            }
            //�N���b�J�[���C���X�^���X����
            //switch (_playerDirection.transform.rotation.z)
            //{
            //    case -90:
            //        GameObject cracker1 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.Euler(0, 0, -90));
            //        SpriteRenderer spriteRenderer1 = cracker1.GetComponent<SpriteRenderer>();
            //        spriteRenderer1.flipX = true;
            //        break;
            //    case 0:
            //        GameObject cracker2 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
            //        SpriteRenderer spriteRenderer2 = cracker2.GetComponent<SpriteRenderer>();
            //        spriteRenderer2.flipX = true;
            //        break;
            //    case 90:
            //        GameObject cracker3 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.Euler(0, 0, 90));
            //        break;
            //    case 180:
            //        GameObject cracker4 = Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.Euler(0, 0, 180));
            //        break;
            //}   
            //Instantiate(_crackerPrefab, _crackerPos.position, Quaternion.identity);
            _isCraker = false;
            _timer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_playerDirection.transform.rotation.z > 0f && _playerDirection.transform.rotation.z < 1f)
        {
            Gizmos.DrawWireCube(_centerShotPos.transform.position, new Vector3(_boxVertical, _boxHorizontal, 0));
        }
        else
        {
            Gizmos.DrawWireCube(_centerShotPos.transform.position, new Vector3(_boxHorizontal, _boxVertical, 0));
        }
    }
}
