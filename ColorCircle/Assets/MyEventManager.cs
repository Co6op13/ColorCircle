using UnityEngine;
using UnityEngine.Events;

public static class MyEventManager
{
    public static UnityEvent OnStartGame = new UnityEvent();
    public static UnityEvent OnGameOver = new UnityEvent();
    public static UnityEvent OnIncreasePoints = new UnityEvent();

    public static void SendStartGame()
    {
        OnStartGame.Invoke();
    }

    public static void SendGameOver()
    {
        OnGameOver.Invoke();
    }

    public static void SendIncreasePoints()
    {
        OnIncreasePoints.Invoke();
    }
}
