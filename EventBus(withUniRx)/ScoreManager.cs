using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public void AddScore(int playerId, int score)
    {
        Debug.Log($"[ScoreManager] Player {playerId} scored {score} points.");

        // Публикация события
        EventBus.Publish(new PlayerScoredEvent(playerId, score));
    }

    // вызов по кнопке
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(1, 10);
        }
    }
}
