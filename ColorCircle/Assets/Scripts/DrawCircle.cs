using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    [SerializeField] private UiMenu menu;
    [SerializeField] private Palette palette;
    [SerializeField] private int fragments;
    [SerializeField] private float radius;
    [SerializeField] private float speed, speedRotate;
    [SerializeField] private float accelerateSpeed;
    [SerializeField] private float accelerateRotateSpeed;
    [SerializeField] private GameObject fragmentCircle;
    [SerializeField] private List<LineController> lcS;
    [SerializeField] private OldPlayer player;
    public bool isGameOver = false;
    public bool isStartGame = false;
    private bool isTherePlayerColor = false;
    private float startPosition = 0f;
    // float offsetSpeed;
    //private void Start()
    //{
    //    CreateFregments();

    //}
    //private void Start()
    //{
    //    offsetSpeed = speed;
    //  //  menu.SetSpeedToLabel(speed - offsetSpeed);
    //}

    private void OnEnable()
    {
        MyEventManager.OnStartGame.AddListener(StartGame);
    }

    private void StartGame()
    {
        isStartGame = true;
        CreateFregments();
    }


    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            if (isStartGame)
                //if (!lcS[0] == null)
                if (!lcS[0].gameObject.activeSelf)
                {
                    lcS.Clear();
                    CreateFregments();
                }
        }
        else
        {
            menu.ShowGameOver();
        }
    }


    public void DestroyCircle()
    {
        for (int i = 0; i < fragments; i++)
        {
           // lcS[i].DestroyFragment();
            //lcS[i].isPassed = true;
        }
        speedRotate += accelerateRotateSpeed;
        speed += accelerateSpeed;
        //accelerateSpeed += accelerateSpeed;
     //  menu.SetSpeedToLabel(speed - offsetSpeed);
        //if (speed > SpeedEnd)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void CreateFregments()
    {
        fragments = Random.Range(3, 6);
        int directionRotate = Random.Range(0, 2) > 0 ? 1 : -1;
        for (int i = 0; i < fragments; i++)
        {
            var frag = ShapePool.Instance.GetFromPool(fragmentCircle.name, Vector3.zero, transform.rotation);
            //var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);            
            frag.SetActive(true);
            lcS.Add(frag.GetComponent<LineController>());
            int layer = Random.Range(0, 8) + 10;
            if (layer == player.layer)
            {
                isTherePlayerColor = true;
            }
            if (i == fragments - 1 && !isTherePlayerColor)
            {
                layer = player.layer;
               // Debug.Log(layer);
            }
            frag.layer = layer;
            //lcS[i].SetVariables(palette.colors[layer - 10], fragments, radius, startPosition, speed, speedRotate * directionRotate);
            startPosition = lcS[i].endPosition;
            //Debug.Log(startPosition);
        }
        isTherePlayerColor = false;
    }
}
