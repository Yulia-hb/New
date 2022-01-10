using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameController gameController;
    public GameObject startMenu, gameTitle;
    public GameObject gameOverObg, scoreObg, levelObg;
    public Protection _protection;
    public Text scoreText, levelText, gameOverScoreText;

    void Start()
    {
        gameController.OnStateChanged += UpdateUI;
        gameController.OnScoreChanged += (int value) => { scoreText.text = value.ToString(); };
        gameController.OnCurrentLevelChanged += (int value) => { levelText.text = value.ToString(); };
    }

    void UpdateUI(GameController.GameState state)
    {
        startMenu.SetActive(state == GameController.GameState.START);
        gameTitle.SetActive(state == GameController.GameState.START);
        gameOverObg.SetActive(state == GameController.GameState.GAME_OVER);
        ShowLevelAndScore(state == GameController.GameState.PLAY || state == GameController.GameState.GAME_OVER);
        _protection.gameObject.SetActive(state == GameController.GameState.PLAY);

    }
    private void ShowLevelAndScore(bool show)
    {
        scoreObg.SetActive(show);
        levelObg.SetActive(show);
    }
    
}
