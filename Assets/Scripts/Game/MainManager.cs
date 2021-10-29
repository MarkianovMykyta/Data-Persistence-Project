using Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private Brick _brickPrefab;
        [SerializeField] private int _lineCount = 6;
        [SerializeField] private Rigidbody _ball;

        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private GameData _gameData;
    
        private bool _started = false;
        private int _points;
        private bool _gameOver = false;

    
        // Start is called before the first frame update
        void Start()
        {
            const float step = 0.6f;
            int perLine = Mathf.FloorToInt(4.0f / step);
        
            int[] pointCountArray = new [] {1,1,2,2,5,5};
            for (int i = 0; i < _lineCount; ++i)
            {
                for (int x = 0; x < perLine; ++x)
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                    brick.OnDestroyed.AddListener(AddPoint);
                }
            }

            UpdateBestScore(_gameData.BestScoreData);
        }

        private void Update()
        {
            if (!_started)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _started = true;
                    float randomDirection = Random.Range(-1.0f, 1.0f);
                    Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                    forceDir.Normalize();

                    _ball.transform.SetParent(null);
                    _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                }
            }
            else if (_gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        void AddPoint(int point)
        {
            _points += point;
            _scoreText.text = $"Score : {_points}";
        }

        public void GameOver()
        {
            if (_gameData.BestScoreData == null || _gameData.BestScoreData.Score < _points)
            {
                _gameData.BestScoreData = new BestScoreData()
                {
                    UserName = _gameData.CurrentPlayerName,
                    Score = _points
                };
                UpdateBestScore(_gameData.BestScoreData);
                DataLoader.SaveData(_gameData.BestScoreData);
            }
            
            _gameOver = true;
            _gameOverText.SetActive(true);
        }

        private void UpdateBestScore(BestScoreData bestScoreData)
        {
            if (bestScoreData != null && !string.IsNullOrEmpty(bestScoreData.UserName))
            {
                _bestScoreText.text =
                    $"Best Score: {bestScoreData.UserName} : {bestScoreData.Score}";
            }
            else
            {
                _bestScoreText.text = "Best Score: 0";
            }
        }
    }
}
