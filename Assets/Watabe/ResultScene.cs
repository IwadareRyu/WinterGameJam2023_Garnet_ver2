using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    private void Start()
    {
        // ゲームマネージャーのインスタンスを取得
        GManager gameManager = GManager.Instance;

        // ゲームマネージャーのスコアを取得
        int score = gameManager.GetScore();

        // スコアを表示する
        Text scoreText = GameObject.Find("Score").GetComponent<Text>();
        
        scoreText.text = score.ToString();

        // テキストを中央寄せにする
        scoreText.alignment = TextAnchor.MiddleCenter;
    }

    // ボタンを押したらタイトルに戻る
    public void OnClickStart()
    {
        SceneManager.LoadScene("Title");
    }
}