using UnityEngine;
using UnityEngine.UI;  // ���ǉ�

public class GameManager : MonoBehaviour
{

    [SerializeField]
    PlayerController player = null;
    [SerializeField]
    MeteorSpawner[] spawners = null;
    [SerializeField]
    Canvas gameStartCanvas = null;
    [SerializeField]
    Canvas gameOverCanvas = null;
    [SerializeField]
    Text scoreText = null;  // ���ǉ�
    [SerializeField]
    Canvas scoreCanvas = null;  // ���ǉ�

    // ���ǉ�
    int score = 0;
    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, 9999999);
            scoreText.text = score.ToString();
        }
        get
        {
            return score;
        }
    }

    void Start()
    {
        player.gameObject.SetActive(false);
        SetActiveSpawners(false);

        // �^�C�g����ʂ�\��
        gameStartCanvas.gameObject.SetActive(true);

        Score = 0;
    }

    public void GameStart()
    {
        // �X�R�A�����Z�b�g����
        Score = 0;
        scoreCanvas.gameObject.SetActive(true);
        // �^�C�g����ʂ�Q�[���I�[�o�[��ʂ��\��
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // ��ʏ�Ɏc���Ă���覐΂��폜
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject meteor in meteors)
        {
            Destroy(meteor);
        }

        // �v���C���[�̍��W�����_�ɖ߂��A�A�N�e�B�u��Ԃɂ���
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        // �X�|�i�[���N��
        SetActiveSpawners(true);
    }

    public void GameOver()
    {
        // �Q�[���I�[�o�[��ʂ̕\��
        gameOverCanvas.gameObject.SetActive(true);

        // �X�|�i�[���~
        SetActiveSpawners(false);
    }

    void SetActiveSpawners(bool value)
    {
        foreach (MeteorSpawner spawner in spawners)
        {
            spawner.isActive = value;
        }
    }

}