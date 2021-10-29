using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
	public class Menu : MonoBehaviour
	{
		[SerializeField] private TMP_Text _bestScoreText;
		[SerializeField] private TMP_InputField _nameInputField;
		[SerializeField] private Button _startButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private GameData _gameData;

		private void Awake()
		{
			_startButton.onClick.AddListener(OnStartClicked);
			_quitButton.onClick.AddListener(OnQuitClicked);

			var bestScoreData = DataLoader.LoadData();
			if (bestScoreData != null && !string.IsNullOrEmpty(bestScoreData.UserName))
			{
				_gameData.BestScoreData = bestScoreData;
			}

			SetupBestScore(bestScoreData);
		}

		private void OnStartClicked()
		{
			var userName = _nameInputField.text;
			if (string.IsNullOrEmpty(userName)) return;

			_gameData.CurrentPlayerName = userName;

			SceneManager.LoadScene(1);
		}

		private void OnQuitClicked()
		{
			Application.Quit();
		}

		private void SetupBestScore(BestScoreData bestScoreData)
		{
			if (bestScoreData == null || string.IsNullOrEmpty(bestScoreData.UserName))
			{
				_bestScoreText.text = "No best score yet";
			}
			else
			{
				_bestScoreText.text = $"Best Score: {bestScoreData.UserName} - {bestScoreData.Score}";
			}
		}
	}
}