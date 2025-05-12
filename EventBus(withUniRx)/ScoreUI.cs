using UnityEngine;
using UniRx;

public class ScoreUI : MonoBehaviour
{
	private void Start()
	{
		EventBus.OnEvent<PlayerScoredEvent>()
			.Subscribe(evt =>
			{
				Debug.Log($"[ScoreUI] Player {evt.PlayerId} gained {evt.ScoreGained} points!");
				// ����� ����� ���������� ����
			})
			.AddTo(this); // �������������� ������� ��� ����������� �������
	}
}
