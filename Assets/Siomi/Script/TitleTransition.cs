using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    [SerializeField] float _sceneChangeTime = 1f;
    //�擪�̃V�[���ɑJ�ڂ���
    public void TitleScene()
    {
        StartCoroutine(TitleSceneCoroutine());
    }

    IEnumerator TitleSceneCoroutine()
    {
        yield return new WaitForSeconds(_sceneChangeTime);
        GManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}
