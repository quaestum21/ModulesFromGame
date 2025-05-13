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
                // Here will be the update of ui
            })
			.AddTo(this); // automatic unsubscribe when object is destroyed
    }
}
