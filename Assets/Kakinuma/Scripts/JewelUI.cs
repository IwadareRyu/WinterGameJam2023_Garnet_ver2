using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 空のオブジェクトに付ける
/// 
/// </summary>
public class JewelUI : MonoBehaviour
{
    GameObject _gmObject = null;
    GManager _gameManager = null;
    [Tooltip("宝石のUI ゲット状態のもの　配列の最後から表示される")]
    [SerializeField] GameObject[] _jewelUI;
    /*[Tooltip("宝石のUI ゲットした状態")]
    [SerializeField] Image _getJewelUI;*/

    int _iconNumber = 0; // 宝石の数
    int _nowjewelCount = 0;
    int _oldJewelCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //    _gmObject = GameObject.Find("GameManager");
        _gameManager = GManager.Instance;
        _nowjewelCount = _gameManager.GetJewelCount();
        _iconNumber = _jewelUI.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (_oldJewelCount < _nowjewelCount) // ★宝石を手に入れたら デバッグ用：Input.GetKeyUp(KeyCode.Space)
        {
            _iconNumber -= 1;
            ShowIcon(_iconNumber);
            _oldJewelCount = _nowjewelCount;
        }
    }

    public void ShowIcon(int number)
    {
        for (int i = 0; i < _jewelUI.Length; i++)
        {
            _jewelUI[i].SetActive(true);
        }

        for (int i = 0; i < number; i++)
        {
            _jewelUI[i].SetActive(false);
        }
    }
}
