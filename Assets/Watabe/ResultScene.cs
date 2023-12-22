using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScene : MonoBehaviour
{
    private void Start()
    {
        // �Q�[���}�l�[�W���[�̃C���X�^���X���擾
        GManager gameManager = GManager.Instance;

        // �Q�[���}�l�[�W���[�̃X�R�A���擾
        int score = gameManager.GetScore();

        // �X�R�A��\������
        Text scoreText = GameObject.Find("Score").GetComponent<Text>();
        
        scoreText.text = score.ToString();

        // �e�L�X�g�𒆉��񂹂ɂ���
        scoreText.alignment = TextAnchor.MiddleCenter;
    }

    // �{�^������������^�C�g���ɖ߂�
    public void OnClickStart()
    {
        SceneManager.LoadScene("Title");
    }
}