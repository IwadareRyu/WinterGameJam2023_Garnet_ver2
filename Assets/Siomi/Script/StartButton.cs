using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void GameStart()
    {
        //���݂̃V�[���̃C���f�b�N�X�ԍ����擾
        int nowSceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++nowSceneNum);
    }
}
