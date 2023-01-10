using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeVariable
{
    Circle,
    Triangle,
    Square,
    Capsule,
    Diamond,
    Hexagon,
}

[System.Serializable]
public struct ShapeSpawnOptions
{
    public float delay;
    public ShapeVariable type;
    public int[] colorIndexs;
}

[System.Serializable]
public class Wave
{
    [SerializeField] public ShapeSpawnOptions[] shapes;
    [SerializeField] public bool IsActive = false;
    [SerializeField] public bool IsEnd = false;

}
public class GameManager : MonoBehaviour
{
    [SerializeField] private float startSpeedReduction, startSpeedRotation, acceleratiomSpeedReduction, accelerationSpeedRotation;
    [SerializeField] private Wave[] waves;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private Palette palette;
    [SerializeField] private bool isAddNextShape;
    [SerializeField] private float size;
    private bool IsStartGame;
    public static GameManager Instance;
    private float speedReduction, speedRotation;
    private GameObject currentShape;


    private void Awake()
    {
        Instance = this;
        MyEventManager.OnStartGame.AddListener(StartGame);
        //MyEventManager.OnIncreasePoints.AddListener(GetNextShape);
        MyEventManager.OnIncreasePoints.AddListener(DisableCurrentShape);
    }


    //private void GetNextShape()
    //{
    //    isAddNextShape = true;
    //}
    private void SetColors()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            for (int s = 0; s < waves[w].shapes.Length; s++)
            {
                if (waves[w].shapes[s].type == ShapeVariable.Square)
                {
                    waves[w].shapes[s].colorIndexs = SelectionColors(4);
                }
                else if (waves[w].shapes[s].type == ShapeVariable.Circle)
                {
                    waves[w].shapes[s].colorIndexs = SelectionColors(Random.Range(3, 6));
                }
                else if (waves[w].shapes[s].type == ShapeVariable.Triangle)
                {
                    waves[w].shapes[s].colorIndexs = SelectionColors(3);
                }
                else if (waves[w].shapes[s].type == ShapeVariable.Capsule)
                {
                    waves[w].shapes[s].colorIndexs = SelectionColors(4);
                }
            }
        }
    }

    private int[] SelectionColors(int count)
    {
        int[] colorIndexs = new int[count];
        for (int i = 0; i < count; i++)
        {
            colorIndexs[i] = Random.Range(0, palette.colors.Length);
        }
        return colorIndexs;
    }

    private void StartGame()
    {
        SetColors();
        speedReduction = startSpeedReduction;
        speedRotation = startSpeedRotation;
        StartCoroutine(ActivationWawe());

    }

    //private IEnumerator WaitStartLavel()
    //{
    //    yield return new WaitUntil(() => IsStartGame == true);
    //    StartCoroutine(ActivationWawe());
    //    Debug.Log(2);
    //    yield break;
    //}
    private IEnumerator ActivationWawe()
    {
        isAddNextShape = true;
        for (int i = 0; i < waves.Length; i++)
        {
            StartCoroutine(SpawnShape(waves[i], i));
            yield return new WaitUntil(() => waves[i].IsEnd == true);
        }
        yield break;
    }

    private IEnumerator SpawnShape(Wave wave, int numberWave)
    {
        for (int j = 0; j < wave.shapes.Length; j++)
        {
            yield return new WaitForSeconds(0.05f);
            yield return new WaitUntil(() => isAddNextShape == true);
            //yield return new WaitForSeconds(wave.shapes[j].delay);
            AddShape(wave.shapes[j].type, numberWave, j);
            isAddNextShape = false;
        }
        wave.IsEnd = true;
        yield break;


    }

    public void AddShape(ShapeVariable nameShape, int numberWave, int numberShape)
    {
        currentShape = ShapePool.Instance.GetFromPool(nameShape.ToString(), Vector3.zero, transform.rotation);
        currentShape.transform.localScale = Vector3.one;
        int directionRotate = Random.Range(0, 2) > 0 ? 1 : -1;
        IShape s = currentShape.GetComponent<IShape>();
        //int[] ci = waves[numberWave].shapes[numberShape].colorIndexs;
        s.SetVariable(waves[numberWave].shapes[numberShape].colorIndexs, size,
            speedReduction, speedRotation * directionRotate);
        int index = Random.Range(0, waves[numberWave].shapes[numberShape].colorIndexs.Length);
        Player.Instance.SetColorAndLayer(waves[numberWave].shapes[numberShape].colorIndexs[index]);
        currentShape.SetActive(true);
    }


    public void DisableCurrentShape()
    {
        //Debug.Break();
       
        currentShape.GetComponent<IShape>().DisableShape();
        //currentShape.SetActive(false);
        Invoke("ResetAddNexShape", 0.2f);
    }

    private void ResetAddNexShape()
    {
        isAddNextShape = true;
    }
}
