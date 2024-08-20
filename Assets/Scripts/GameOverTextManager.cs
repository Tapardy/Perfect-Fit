using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject scoreHandler;

    private float _score;

    // Update is called once per frame
    void Update()
    {
        _score = scoreHandler.GetComponent<Score>().getScore();
        scoreText.text = "Score: " + _score;
    }
}