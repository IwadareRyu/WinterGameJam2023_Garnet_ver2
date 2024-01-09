using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] float _changeSceneTime = 1.0f;
    public void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(_changeSceneTime);
        //���݂̃V�[���̃C���f�b�N�X�ԍ����擾
        int nowSceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++nowSceneNum);
    }
}
