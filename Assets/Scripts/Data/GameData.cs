using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "GameData", menuName = "Scriptables/GameData")]
	public class GameData : ScriptableObject
	{
		public string CurrentPlayerName;
		public BestScoreData BestScoreData;

	}
}