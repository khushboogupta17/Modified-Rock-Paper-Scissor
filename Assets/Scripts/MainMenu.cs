using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayButton;
    public TextMeshProUGUI HighestScoreText;

    private void Start()
    {
        PlayButton.onClick.AddListener(OnPlayClicked);
        SetHighestScore();
    }

    public void SetHighestScore()
    {
        var highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        HighestScoreText.text = "High Score: " + highestScore;
    }

    public void OnPlayClicked()
    {
        SceneManager.Instance.LoadScene(1);
    }
}
