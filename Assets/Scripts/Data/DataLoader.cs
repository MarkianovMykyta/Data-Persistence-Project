using System.IO;
using UnityEngine;

namespace Data
{
	public static class DataLoader
	{
		private static readonly string _path = Application.persistentDataPath + "/bestScore.save";
	
		public static void SaveData(BestScoreData bestScoreData)
		{
			if (bestScoreData == null || string.IsNullOrEmpty(bestScoreData.UserName))
			{
				Debug.LogError("You are trying to save null data!");
				return;
			}

			var json = JsonUtility.ToJson(bestScoreData);
			File.WriteAllText(_path, json);
		}

		public static BestScoreData LoadData()
		{
			if (File.Exists(_path))
			{
				var json = File.ReadAllText(_path);
				var bestScoreData = JsonUtility.FromJson<BestScoreData>(json);

				return bestScoreData;
			}

			return null;
		}
	}
}