using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void GameStart()
    {
        //現在のシーンのインデックス番号を取得
        int nowSceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++nowSceneNum);
    }
}
