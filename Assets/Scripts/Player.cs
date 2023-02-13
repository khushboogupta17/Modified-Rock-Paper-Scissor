using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : MonoBehaviour
    {
        public string Name;
        public int Score;
        public int HighestScore;

        public void IncrementScore(int delta)
        {
            Score += delta;
            UIManager.Instance.SetScore(Score);
        }

        void SaveScore()
        {
            var oldHighestScore = PlayerPrefs.GetInt("HighestScore", 0);
            if (oldHighestScore < Score)
            {
                PlayerPrefs.SetInt("HighestScore", Score);
            }
        }

        private void OnDestroy()
        {
            SaveScore();
        }
    }
}