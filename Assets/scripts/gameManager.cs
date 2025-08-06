using UnityEngine;
using TMPro;
public class gameManager : MonoBehaviour
{
    public Camera ARCamera;
    public static gameManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float score = 0;
    void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void UpdateScore(float points)
    {
        score += points;
        scoreText.text = string.Format("Puntuación: {0}", score);
    }
}
