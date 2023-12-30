using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    //-------Var-------//

    [Header("累計プレイ時間（減算方式）")] public float initialTimer = 300.0f;
    [Header("残り時間表示テキスト")] public Text timerText;
    [Header("経過時間表示テキスト")] public Text elapsedTimeText;
    [Header("スコア表示テキスト")]public Text scoreText;
    [Header("敵と衝突時の残り時間スピードアップ速度")] public float speedUpSpeed = 10.0f;
    [Header("タイムスコア（１秒につき何点乗算するか）")] public float TimeScore = 100.0f;
    [Header("ジュエルスコア（１つにつき何点乗算するか）")] public int jewelScore = 10000;
    [Header("BGMデータ")] public AudioSource bgmSource;
    [Header("BGMの音量調整")] public AudioClip bgm;
    [Header("タイトル画面の名前")] public string titleSceneName;
    [Header("プレイ画面の名前")] public string mainSceneName;
    [Header("リザルト画面の名前")] public string resultSceneName;

    private int jewelCount = 0;
    private float gameTimer;
    private float elapsedTime = 0;
    private int score = 0;
    // 現在スピードアップ中かどうか
    private bool isSpeedUp = false;
    // スピードアップの経過時間
    private float speedUpTimer = 0; 


    //-----Single-----//

    public static GManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            // 残り時間の更新
            float deltaTime = Time.deltaTime;

            if (isSpeedUp)
            {
                // x倍の速さで減少
                gameTimer -= deltaTime * speedUpSpeed;

                speedUpTimer += deltaTime * speedUpSpeed;

                // x秒経過したら元のスピードに戻す
                if (speedUpTimer >= speedUpSpeed)
                {
                    isSpeedUp = false;
                }
            }
            else
            {
                gameTimer -= deltaTime;
            }
            //timerText.text = "Time: " + (300f + gameTimer).ToString("F2");

            if (gameTimer <= 0)
            {
                CalculateScore();
                SceneManager.LoadScene(resultSceneName);
            }

            // 経過時間の更新
            if (isSpeedUp)
            {
                elapsedTime += deltaTime * speedUpSpeed;
            }
            else
            { 
                elapsedTime += deltaTime;
            }
            // 経過時間の表示
            UpdateElapsedTimeText(elapsedTimeText);
        }
    }

    //------Scene------//

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //------Jewel------//

    // ジュエルの数を増やす（ジュエルを取った時に呼ぶ）
    public void AddJewelCount()
    {
        jewelCount++;
    }

    public int GetJewelCount()
    {
        return jewelCount;
    }

    //------Score-----//

    // スコアの計算をする
    public void CalculateScore()
    {
        score = jewelCount * jewelScore - (int)(gameTimer * TimeScore);

        if (score <= 0)
        {
            score = 0;
        }
    }

    // スコアの表示（リザルト画面で呼ぶ）引数無し
    //public void ShowScore()
    //{
    //    scoreText.text = "Score: " + score.ToString();
    //}

    // スコアの表示（リザルト画面で呼ぶ） 引数ありに訂正
    public void ShowScore(Text scoreDisplay)
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + score.ToString();
        }
    }

    // スコアの取得
    public int GetScore()
    {
        return score;
    }

    //------Time------//

    // 経過時間の表示
    public void UpdateElapsedTimeText(Text timeDisplay)
    {
        if (timeDisplay != null)
        {
            timeDisplay.text = "Elapsed Time: " + elapsedTime.ToString("F2");
        }
    }

    public void ResetTimer()
    {
        gameTimer = initialTimer;
    }

    public float GetGameTimer()
    {
        return gameTimer;
    }

    // 敵と衝突した時に呼ぶ
    public void clashEnemy()
    {
       // スピードアップを開始
       isSpeedUp = true; 
       speedUpTimer = 0;
    }

    //-------BGM-------//

    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource != null && bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
            bgmSource.loop = true;
        }
    }
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = volume;
        }
    }

    public float GetBGMVolume()
    {
        return bgmSource != null ? bgmSource.volume : 0;
    }

    //------Goal-----//

    // ゴールした時に呼ぶ
    public void Goal()
    {
        CalculateScore();
        SceneManager.LoadScene(resultSceneName);
    }

    //------Reset-----//
    
    // スタート画面にて呼ぶ
    public void ResetGame()
    {
        ResetTimer();
        jewelCount = 0;
        elapsedTime = 0;
        score = 0;
    }
}