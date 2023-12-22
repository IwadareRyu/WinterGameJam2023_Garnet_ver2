using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Jewel : MonoBehaviour
{
    [SerializeField] bool IsDream = false;
    [SerializeField] bool IsFound = false;
    [SerializeField, Header("�I�[�f�B�I�\�[�X�̐ݒ�")] AudioSource _audioSource;
    [SerializeField, Header("�o��������")] AudioClip _audioClip;
    Collider2D _col;
    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        DreamStateScripts.DreamWorld += DreamChange;
        DreamStateScripts.DreamWorldEnd += DreamChange;

        _col = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _col.enabled = false;
        _spriteRenderer.enabled = false;
    }

    private void OnDisable()
    {
        DreamStateScripts.DreamWorld -= DreamChange;
        DreamStateScripts.DreamWorldEnd -= DreamChange;
    }

    /// <summary> ���ƌ�����؂�ւ����Ƃ��̏��� </summary>
    public void DreamChange()
    {
        IsDream = !IsDream;
        if (IsDream || IsFound)
        {
            _spriteRenderer.enabled = true;
            _col.enabled = true;
        }
        else
        {
            _spriteRenderer.enabled = false;
            _col.enabled = false;
        }
    }

    /// <summary> �W���G���ɂ��������Ƃ��̏��� </summary>
    public void JewelFound()
    {
        if(IsDream)
        {
            IsFound = true;
        }    
        else
        {
            //�Q�[���}�l�[�W���[�̃W���G���X�R�A���X�V���ăf�X�g���C����
            GManager.Instance.AddJewelCount();
            //�����o��
            //_audioSource.PlayOneShot(_audioClip);
            Destroy(this.gameObject);
        }

    }
}
