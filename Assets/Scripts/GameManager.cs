using UnityEngine;
using UnityEngine.UI;
using TMPro;

//�÷��̾ ���� ����(Ÿ������, �ְ�����, �� �ı�����)�� �����Ѵ�.
//�Ӽ� : �� ����
//���� 2: ���� ui ǥ��
//����3 : ���� ui text �ʱ�ȭ

public class GameManager : MonoBehaviour
{
    private int _attackScore;
    public int attackScore
    {
        get { return _attackScore; }
        set 
        {
            _attackScore = value;
            attackScoreText.text = _attackScore.ToString();
        }
    }
    private int _destroyScore;
    public int destroyScore
    {
        get { return _destroyScore; }
        set
        {
            _destroyScore = value;
            destroyScoreText.text = _destroyScore.ToString();
        }
    }
    public int bestScore;

    public TMP_Text attackScoreText;
    public TMP_Text destroyScoreText;
    public TMP_Text bextScoreText;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _attackScore = 0;
        _destroyScore = 0;
        attackScoreText.text = "0";
        destroyScoreText.text = "0";

        bestScore = PlayerPrefs.GetInt("Best Score");
        bextScoreText.text= bestScore.ToString();
        DontDestroyOnLoad(gameObject);
    }
    public void GameEnd()
    {
        bestScore = attackScore + destroyScore;
        bextScoreText.text = bestScore.ToString();
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("Best Score", bestScore);
    }
}
