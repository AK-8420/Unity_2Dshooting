using UnityEngine;
using UnityEngine.UI;  // ★追加

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
    Text scoreText = null;  // ★追加
    [SerializeField]
    Canvas scoreCanvas = null;  // ★追加

    // ★追加
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

        // タイトル画面を表示
        gameStartCanvas.gameObject.SetActive(true);

        Score = 0;
    }

    public void GameStart()
    {
        // スコアをリセットする
        Score = 0;
        scoreCanvas.gameObject.SetActive(true);
        // タイトル画面やゲームオーバー画面を非表示
        gameStartCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);

        // 画面上に残っている隕石を削除
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject meteor in meteors)
        {
            Destroy(meteor);
        }

        // プレイヤーの座標を原点に戻し、アクティブ状態にする
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        // スポナーを起動
        SetActiveSpawners(true);
    }

    public void GameOver()
    {
        // ゲームオーバー画面の表示
        gameOverCanvas.gameObject.SetActive(true);

        // スポナーを停止
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