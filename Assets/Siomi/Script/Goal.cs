using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //[SerializeField] GameObject go;
    private void Start()
    {
        //this.gameObject.SetActive(false);
    }
    private void Update()
    {
        //�W���G���̃J�E���g������Ă���
        int juwelCount = GManager.Instance.GetJewelCount();
        //3�ȏ����Ă�����S�[�����o��������
        if (juwelCount >= 3)
        {
            this.gameObject.SetActive(true);
        }
    }

    //�S�[���̏���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            //�S�[���̏���������
            GManager.Instance.Goal();
        }
    }
}
