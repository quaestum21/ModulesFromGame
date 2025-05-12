public struct PlayerScoredEvent
{
	public int PlayerId;
	public int ScoreGained;

	public PlayerScoredEvent(int playerId, int scoreGained)
	{
		PlayerId = playerId;
		ScoreGained = scoreGained;
	}
}
