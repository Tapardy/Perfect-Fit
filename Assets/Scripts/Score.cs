using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Score : MonoBehaviour
{
    [SerializeField] private int highScore;
    [SerializeField] private int maxComboMultiplier = 50;
    [SerializeField] private int scoreMultiplier = 1;
    [SerializeField] private int comboMultiplier = 2;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboMultiplierText; // New field
    [SerializeField] private TextMeshProUGUI currentPointsText; // New field
    [SerializeField] private TextMeshProUGUI multipliedPointsText; // New field
    
    private int _points;
    private int _score;
    private bool _comboInitiated = false;
    
    void Start()
    {
        ResetScore();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetPoints(100);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetPoints(300);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetPoints(500);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetPoints(700);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetPoints(1000);
        }
    }

    public void SetPoints(int points)
    {
        _points = points;
        IncreaseScore(_points);
    }
    
    private void IncreaseScore(int points)
    {
        if (points >= 1000)
        {
            if (!_comboInitiated)
            {
                _comboInitiated = true;
            }
            else
            {
                scoreMultiplier = IncreaseComboMultiplier(scoreMultiplier);
            }
        }
        else
        {
            scoreMultiplier = 1;
            _comboInitiated = false;
        }
        
        int currentPoints = points * scoreMultiplier;
        
        _score += currentPoints;
        UpdateUI(points, currentPoints);
    }

    private void UpdateUI(int basePoints, int multipliedPoints)
    {
        scoreText.text = "Score: " +_score.ToString();
        comboMultiplierText.text = "x" + scoreMultiplier.ToString();
        currentPointsText.text = "points " + basePoints.ToString();
        multipliedPointsText.text = "multiplied points " + multipliedPoints.ToString();
    }

    private int IncreaseComboMultiplier(int currentMultiplier)
    {
        return Mathf.Min(currentMultiplier * comboMultiplier, maxComboMultiplier);
    }

    public void SetHighScore()
    {
        if (_score > highScore)
        {
            highScore = _score;
        }
    }

    public void ResetScore()
    {
        _score = 0;
        scoreMultiplier = 1;
        _comboInitiated = false;
        UpdateUI(0, 0);
    }

    public void SetScoreMultiplier(int multiplier)
    {
        comboMultiplier = Mathf.Max(1, multiplier);
    }

    public void SetMaxComboMultiplier(int max)
    {
        maxComboMultiplier = Mathf.Max(1, max);
    }
}
