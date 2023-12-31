using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    [SerializeField] float _sceneChangeTime = 1f;
    //先頭のシーンに遷移する
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
