using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { START, PLAY, LOSE, GAME_OVER };
    public event System.Action<GameState> OnStateChanged;
    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnCurrentLevelChanged;

    private GameState state;
    [SerializeField] private Transform spawnRegion;
    [SerializeField] private Transform levelRegion = null;
    [SerializeField] private Level LevelPrefab = null;
    [SerializeField] private List<Color> colors = new List<Color>();
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private List<GameObject> ObstaclePrefabs;
    [SerializeField] private Level lastLevel;
    [SerializeField] private int currentLevel;
    [SerializeField] private int score;
    


    public GameState State { get => state; set { state = value; OnStateChanged?.Invoke(state); } }

    public int CurrentLevel { get => currentLevel; private set { currentLevel = value; OnCurrentLevelChanged?.Invoke(value); } }

    public int Score { get => score; set { score = value; OnScoreChanged?.Invoke(value); } }

    public object player;

    public static GameController Instanse;
   

    private void Awake()
    {
        Instanse = this;
    }


    private void Start()
    {
        ObstaclePrefabs = Resources.LoadAll<GameObject>("GroupObstacles").ToList();
      
        for (int i = 0; i < 2; i++)
        {
            levels.Add(SpawnNewLevel());
        }
        ResetLevels();
     // player.OnGameOver += GameOver;// Ошибка, не могу понять как ее исправить.
    }

    //private void GameOver()// Ошибка в строчке 63
    // {
    //State = GameState.LOSE;
    // StartCoroutine(DelayAction(1.5f, () =>
    // {
    // State = GameState.GAME_OVER;
    // ResetGame();
    //OnGameOver.Invoke(Score); Ошибка, не могу понять как ее исправить.

    // }));
    //  }

    public void ResetGame()
    {
        ClearObstacle();
        ResetLevels();
       // player.Reset();// Ошибка, не могу понять как ее исправить.
    }

    private void ClearObstacle()
    {
        foreach (Transform child in spawnRegion.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerable DelayAction(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    private void ResetLevels()
    {
        levels[0].AnchoredPosition = new Vector3(0, -levels[0].Size.y / 2);
        for (int i = 1; i < levels.Count; i++)
        {
            levels[i].AnchoredPosition = new Vector3(0, levels[i - 1].AnchoredPosition.y + levels[i - 1].Size.y);
        }
    }

    private Level SpawnNewLevel()
    {
        Level level = Instantiate(LevelPrefab, Vector3.zero, Quaternion.identity, levelRegion);
        level.AnchoredPosition = Vector3.zero;
        level.BackColor = colors[UnityEngine.Random.Range(0, colors.Count)];
        level.Size = new Vector2(levelRegion.parent.GetComponent<RectTransform>().sizeDelta.x,
            levelRegion.parent.GetComponent<RectTransform>().sizeDelta.y * 2);
        level.OnFinishLevel += MoveLevelToTop;
        level.OnStartNewLevel += () => { CurrentLevel++; };
        return level;
    }

    private void MoveLevelToTop(Level level)
    {
        level.SetUp(new Vector3(0, lastLevel.AnchoredPosition.y + lastLevel.Size.y),
            colors[CurrentLevel % colors.Count], CurrentLevel);
        lastLevel = level;
        SpawnObstacle(ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Count)], spawnRegion);

    }

    public void StartGame()
    {
        CurrentLevel = 1;
        State = GameState.PLAY;
        SpawnObstacle(ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Count)], spawnRegion);
        StartCoroutine(ScoreCoroutine());
    }

    private IEnumerator ScoreCoroutine()
    {
        while (State == GameState.PLAY)
        {
            Score++;
            yield return new WaitForSeconds(0.2f);          
        }
    }

    private void SpawnObstacle(GameObject gameObject, Transform spawnRegion, bool isFirst = false)
    {
        Instantiate(gameObject, spawnRegion.transform.position * (isFirst ? 0.5f : 1), Quaternion.identity, spawnRegion);
    }
}
