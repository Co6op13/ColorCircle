using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UiMenu : MonoBehaviour
{
    [SerializeField] private DrawCircle circle;
    [SerializeField] private Player player;
    VisualElement root;
    VisualElement mainMenu;
    VisualElement startMeny;
    VisualElement pauseMeny;
    VisualElement panelWinner, gameOverMenu;

    int points = 0;
    Label pointsLabel;
    Label speedLabel;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        mainMenu = root.Q<VisualElement>("MainMenu");
        startMeny = root.Q<VisualElement>("StartMenu");
        pauseMeny = root.Q<VisualElement>("PauseMenu");
        gameOverMenu = root.Q<VisualElement>("GameOverMenu");
        Button startGameButton = root.Q<Button>("StartGameButton");
        Button exittGameButton = root.Q<Button>("ExitGameButton");
        Button resumeButton = root.Q<Button>("ResumeButton");
        Button restartGameButton = root.Q<Button>("RestartGameButton");
        Button restartGameButton1 = root.Q<Button>("RestartButton1");
        Button exitGameButton = root.Q<Button>("ExitGameButton");
        Button exitGameButton2 = root.Q<Button>("ExitGameButton2");

        MyEventManager.OnIncreasePoints.AddListener(IncreasePoints);

        pointsLabel = root.Q<Label>("PointsLabel");
        speedLabel = root.Q<Label>("SpeedLabel");
        exitGameButton.clicked += ExitGame;
        exitGameButton2.clicked += ExitGame;
        resumeButton.clicked += PauseOff;
        startGameButton.clicked += StartGame;
        restartGameButton1.clicked += RestartLavel;
        restartGameButton.clicked += RestartLavel;
        ShowStartMenu();
    }

    private void RestartLavel()
    {
        Debug.Log(12333);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        //player.isPause = false;
        mainMenu.style.visibility = Visibility.Hidden;
        pauseMeny.style.visibility = Visibility.Hidden;
    }
    public void PauseOn()
    {
        mainMenu.style.visibility = Visibility.Visible;
        pauseMeny.style.visibility = Visibility.Visible;
        Time.timeScale = 0;
    }
    private void StartGame()
    {
        MyEventManager.SendStartGame();
        //circle.isStartGame = true;
        //circle.DrawNewCircle();
        //Player.Instance.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        startMeny.style.visibility = Visibility.Hidden;
        mainMenu.style.visibility = Visibility.Hidden;       
    }

    private void ShowStartMenu()
    {
        mainMenu.style.visibility = Visibility.Visible;
        startMeny.style.visibility = Visibility.Visible;
    }

    public void ShowGameOver()
    {
        mainMenu.style.visibility = Visibility.Visible;
        gameOverMenu.style.visibility = Visibility.Visible;
        Label gameOverPoints = root.Q<Label>("GameOverPoints");
        gameOverPoints.text = points.ToString();
    }

    public void IncreasePoints()
    {
        points++;
        pointsLabel.text = points.ToString();
    }

    public void SetSpeedToLabel(float speed)
    {
        //speedLabel.text = Math.Round(speed).ToString();
    }
    //{
    //    moneyLabel.text = GameManager.Instance.GetAmountMoney().ToString();
    //}

}
