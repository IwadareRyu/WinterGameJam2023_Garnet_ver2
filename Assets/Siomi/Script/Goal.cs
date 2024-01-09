using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    SpriteRenderer _mySprite;
    Collider2D _myColider;
    [SerializeField] Canvas _canvas;

    private void Start()
    {
        _mySprite = GetComponent<SpriteRenderer>();
        _myColider = GetComponent<Collider2D>();
        _mySprite.enabled = false;
        _canvas.enabled = false;
        _myColider.enabled = false;
    }
    //private void Update()
    //{
    //    //�W���G���̃J�E���g������Ă���
    //    int juwelCount = GManager.Instance.GetJewelCount();
    //    //3�ȏ����Ă�����S�[�����o��������
    //    if (juwelCount >= 3)
    //    {
    //        this.gameObject.SetActive(true);
    //    }
    //}

    public void GoalPointEnable()
    {
        _mySprite.enabled = true;
        _canvas.enabled = true;
        _myColider.enabled = true;
    }

    //�S�[���̏���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player") && DreamStateScripts.DreamState == DreamState.Normal)
        {
            //�S�[���̏���������
            GManager.Instance.Goal();
        }
    }
}
