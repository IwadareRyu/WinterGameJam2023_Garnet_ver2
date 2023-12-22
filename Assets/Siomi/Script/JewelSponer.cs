using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSponer : MonoBehaviour
{
    [SerializeField, Header("�����_���X�|�i�[�̏ꏊ")] Transform[] _jewelSponer;
    [SerializeField, Header("��΂̃v���n�u")] GameObject _jewelPrefab;
    [SerializeField, Header("�X�|�[������������΂̐�")] int _jewelCount = 3;
    private void Awake()
    {
        JewelSpon();
    }

    void JewelSpon()
    {
        //����΂𐶐������̂��̃J�E���^�ϐ�
        int count = 0;
        //�X�|�i�[��ԊǗ��p�̕ϐ���錾
        int[] sponerCount = new int[_jewelSponer.Length];

        //�ǂ��ɐ������邩�̏������s��
        while(count < _jewelCount)
        {
            int rand = Random.Range(0, _jewelSponer.Length);
            //����Ă����琶�����Ȃ�
            if (sponerCount[rand] == 0 )
            {
                Instantiate( _jewelPrefab, _jewelSponer[rand].position, Quaternion.identity);
                sponerCount[rand] = 1;
                count++;
            }
        }
    }
}
