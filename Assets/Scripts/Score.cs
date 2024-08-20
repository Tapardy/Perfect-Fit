using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Score : MonoBehaviour
{
    [SerializeField] private int highScore;
    [SerializeField] private int maxComboMultiplier = 50;
    [SerializeField] private int scoreMultiplier = 1;
    [SerializeField] private int comboMultiplier = 2;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboMultiplierText;
    [SerializeField] private TextMeshProUGUI currentPointsText;
    [SerializeField] private TextMeshProUGUI multipliedPointsText;
    
    // Arrays for audio clips and corresponding mixer groups
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioMixerGroup audioMixers;
    
    // Internal AudioSources to play clips with specific mixers
    private AudioSource[] _audioSources;

    private bool _perfectFit;
    private int _points;
    public int score;
    private bool _comboInitiated = false;

    void Start()
    {
        _audioSources = new AudioSource[audioClips.Length];
        for (int i = 0; i < audioClips.Length; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = audioClips[i];
            source.outputAudioMixerGroup = audioMixers;
            _audioSources[i] = source;
        }

        ResetScore();
    }


    public void SetPoints(int points)
    {
        IncreaseScore(points);
    }

    public int getScore()
    {
        return score;
    }
    private void IncreaseScore(int points)
    {
        if (points >= 1000)
        {
            if (!_comboInitiated)
            {
                _comboInitiated = true;
                _perfectFit = true;
            }
            else
            {
                _perfectFit = true;
            }
        }
        else
        {
            _perfectFit = false;
        }
        
        int currentPoints = points * scoreMultiplier;
        
        score += currentPoints;

        if (_perfectFit)
        {
            scoreMultiplier = IncreaseComboMultiplier(scoreMultiplier);
            _audioSources[0].Play();  
        }
        else
        {
            scoreMultiplier = 1;
            if (_comboInitiated)
            {
                _audioSources[1].Play(); 
                _comboInitiated = false;
            }
            else
            {
                _audioSources[2].Play();
            }
            
        }

        UpdateUI(points, currentPoints);
    }

    private void UpdateUI(int basePoints, int multipliedPoints)
    {
        scoreText.text = "Score: " + score.ToString();
        comboMultiplierText.text = "x" + scoreMultiplier.ToString();
        currentPointsText.text = "points: " + basePoints.ToString();
        multipliedPointsText.text = "Points: " + multipliedPoints.ToString();
    }

    private int IncreaseComboMultiplier(int currentMultiplier)
    {
        return Mathf.Min(currentMultiplier * comboMultiplier, maxComboMultiplier);
    }

    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void ResetScore()
    {
        score = 0;
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
