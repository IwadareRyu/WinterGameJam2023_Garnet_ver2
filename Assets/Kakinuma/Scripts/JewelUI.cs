using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��̃I�u�W�F�N�g�ɕt����
/// 
/// </summary>
public class JewelUI : MonoBehaviour
{
    GManager _gameManager = null;
    [Tooltip("��΂�UI �Q�b�g��Ԃ̂��́@�z��̍Ōォ��\�������")]
    [SerializeField] GameObject[] _jewelUI;

    int _iconNumber = 0; // ��΂̐�
    int _nowjewelCount = 0;
    int _oldJewelCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GManager.Instance;
        _iconNumber = _jewelUI.Length;
    }

    // Update is called once per frame
    void Update()
    {
        _nowjewelCount = _gameManager.GetJewelCount();

        if (_oldJewelCount < _nowjewelCount)
        {
            _iconNumber -= 1;
            ShowIcon(_iconNumber);
            _oldJewelCount = _nowjewelCount;
        }
        /*if (Input.GetKeyUp(KeyCode.Space)) // �f�o�b�O�p�FInput.GetKeyUp(KeyCode.Space)
        {
            _nowjewelCount += 1;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Jewel:" + _nowjewelCount);
        }*/
    }

    public void ShowIcon(int number)
    {
        if (number >= 0)
        {
            _jewelUI[number].SetActive(true);
        }
    }
}
