using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance for global access
    public static ScoreManager Instance;

    // UI text element to display the score
    public Text scoreText;

    // Tracks the player's score
    private int score;

    private void Awake()
    {
        // Ensure only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add points to the score and update the UI
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Update the displayed score
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the Inspector.");
        }
    }
}
