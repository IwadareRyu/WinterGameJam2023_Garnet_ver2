using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    //-------Var-------//

    [Header("�݌v�v���C���ԁi���Z�����j")] public float initialTimer = 300.0f;
    [Header("�c�莞�ԕ\���e�L�X�g")] public Text timerText;
    [Header("�o�ߎ��ԕ\���e�L�X�g")] public Text elapsedTimeText;
    [Header("�X�R�A�\���e�L�X�g")]public Text scoreText;
    [Header("�G�ƏՓˎ��̎c�莞�ԃX�s�[�h�A�b�v���x")] public float speedUpSpeed = 10.0f;
    [Header("�^�C���X�R�A�i�P�b�ɂ����_��Z���邩�j")] public float TimeScore = 100.0f;
    [Header("�W���G���X�R�A�i�P�ɂ����_��Z���邩�j")] public int jewelScore = 10000;
    [Header("BGM�f�[�^")] public AudioSource bgmSource;
    [Header("BGM�̉��ʒ���")] public AudioClip bgm;
    [Header("�^�C�g����ʂ̖��O")] public string titleSceneName;
    [Header("�v���C��ʂ̖��O")] public string mainSceneName;
    [Header("���U���g��ʂ̖��O")] public string resultSceneName;

    private int jewelCount = 0;
    private float gameTimer;
    private float elapsedTime = 0;
    private int score = 0;
    // ���݃X�s�[�h�A�b�v�����ǂ���
    private bool isSpeedUp = false;
    // �X�s�[�h�A�b�v�̌o�ߎ���
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
            // �c�莞�Ԃ̍X�V
            float deltaTime = Time.deltaTime;

            if (isSpeedUp)
            {
                // x�{�̑����Ō���
                gameTimer -= deltaTime * speedUpSpeed;

                speedUpTimer += deltaTime * speedUpSpeed;

                // x�b�o�߂����猳�̃X�s�[�h�ɖ߂�
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

            // �o�ߎ��Ԃ̍X�V
            if (isSpeedUp)
            {
                elapsedTime += deltaTime * speedUpSpeed;
            }
            else
            { 
                elapsedTime += deltaTime;
            }
            // �o�ߎ��Ԃ̕\��
            UpdateElapsedTimeText(elapsedTimeText);
        }
    }

    //------Scene------//

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //------Jewel------//

    // �W���G���̐��𑝂₷�i�W���G������������ɌĂԁj
    public void AddJewelCount()
    {
        jewelCount++;
    }

    public int GetJewelCount()
    {
        return jewelCount;
    }

    //------Score-----//

    // �X�R�A�̌v�Z������
    public void CalculateScore()
    {
        score = jewelCount * jewelScore - (int)(gameTimer * TimeScore);

        if (score <= 0)
        {
            score = 0;
        }
    }

    // �X�R�A�̕\���i���U���g��ʂŌĂԁj��������
    //public void ShowScore()
    //{
    //    scoreText.text = "Score: " + score.ToString();
    //}

    // �X�R�A�̕\���i���U���g��ʂŌĂԁj ��������ɒ���
    public void ShowScore(Text scoreDisplay)
    {
        if (scoreDisplay != null)
        {
            scoreDisplay.text = "Score: " + score.ToString();
        }
    }

    // �X�R�A�̎擾
    public int GetScore()
    {
        return score;
    }

    //------Time------//

    // �o�ߎ��Ԃ̕\��
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

    // �G�ƏՓ˂������ɌĂ�
    public void clashEnemy()
    {
       // �X�s�[�h�A�b�v���J�n
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

    // �S�[���������ɌĂ�
    public void Goal()
    {
        CalculateScore();
        SceneManager.LoadScene(resultSceneName);
    }

    //------Reset-----//
    
    // �X�^�[�g��ʂɂČĂ�
    public void ResetGame()
    {
        ResetTimer();
        jewelCount = 0;
        elapsedTime = 0;
        score = 0;
    }
}