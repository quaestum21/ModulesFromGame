using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public void AddScore(int playerId, int score)
    {
        Debug.Log($"[ScoreManager] Player {playerId} scored {score} points.");

        // ���������� �������
        EventBus.Publish(new PlayerScoredEvent(playerId, score));
    }

    // ����� �� ������
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddScore(1, 10);
        }
    }
}
