using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bom : MonoBehaviour
{
    [SerializeField, Header("�����܂ł̎���")] float _bomInterval = 5f;
    [SerializeField, Header("�����G�t�F�N�g")] GameObject _bomEffect;
    [SerializeField, Header("�����͈�")] float _bomRange = 5.0f;
    [SerializeField, Header("�I�[�f�B�I�\�[�X�̐ݒ�")] AudioSource _audioSource;
    [SerializeField, Header("�o��������")] AudioClip _audioClip;
    private void Start()
    {
        Invoke("Expllosion", _bomInterval);
    }

    void Expllosion()
    {
        ////�����o��
        //_audioSource.PlayOneShot(_audioClip);

        // �w��͈͂̃R���C�_�[��S�Ď擾����
        var cols = Physics2D.OverlapCircleAll(this.transform.position, _bomRange);

        //�v���C���[�ƃG�l�~�[��T��
        foreach(var c in cols)
        {
            if(c.TryGetComponent<StunStateScripts>(out var stunState))
            {
                //�������̃L�����̏���
                stunState.ExplosionScale();
                //�X�^��������
                stunState.ChangeStunState();
            }
        }
        //�����G�t�F�N�g���o��������
        Instantiate(_bomEffect, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //�����͈͂̃M�Y��
        Gizmos.DrawWireSphere(transform.position, _bomRange);
    }
} 
