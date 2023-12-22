using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    //æ“ª‚ÌƒV[ƒ“‚É‘JˆÚ‚·‚é
    public void TitleScene()
    {
        GManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
}
