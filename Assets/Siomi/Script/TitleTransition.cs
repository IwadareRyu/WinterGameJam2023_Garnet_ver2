using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    //�擪�̃V�[���ɑJ�ڂ���
    public void TitleScene()
    {
        GManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}
