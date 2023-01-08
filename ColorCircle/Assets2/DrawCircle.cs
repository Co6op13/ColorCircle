using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DrawCircle : MonoBehaviour
{

    [System.Serializable]
    public class ListCircle
    {
        public List<LineController> listFragments;// = new List<LineController>();

    }
    [SerializeField] private List<ListColor> wave;
    [SerializeField] private UiMenu menu;
    [SerializeField] private Palette palette;
    [SerializeField] private int fragments;
    [SerializeField] private float radius;
    [SerializeField] private float speed, SpeedEnd;
    [SerializeField] private float accelerateSpeed;
    [SerializeField] private GameObject fragmentCircle;
    [SerializeField] private List<ListCircle> listFragments;
    //[SerializeField] private List<LineController> listFragments;
    [SerializeField] private Player player;
    [SerializeField] int countCircle;
    public bool isGameOver = false;
    public bool isStartGame = false;
    private bool isTherePlayerColor = false;
    private float startPosition = 0f;

    private void Start()
    {
        wave = new List<ListColor>();
        wave.Add(new ListColor(palette));
        wave.Add(new ListColor(palette));
        wave.Add(new ListColor(palette));
        listFragments = new List<ListCircle>();
        listFragments.Add(new ListCircle());
        listFragments[0].listFragments = new List<LineController>();
        listFragments[0].listFragments.Add(new LineController());
        ChoiceColor();
    }

    public void DeleteCurrentWaveAndCreateNew()
    {
        wave.RemoveAt(0);
        wave.Add(new ListColor(palette));
        countCircle--;
    }

    public void ChoiceColor()
    {
        int index = Random.Range(0, wave[0].listColors.Count);
        player.nextColor = wave[0].listColors[index];
    }


    public void DrawNewCircle()
    {
        //CreateFragments(0);
        StartCoroutine(CorDrawNewCircle());
    }

    public IEnumerator CorDrawNewCircle()
    {

        while (!isGameOver)
        {
            if (wave.Count  > countCircle)
            {
                //for (int i = 0; i < 3; i++)
                //{
                //    //listFragments[i].
                    CreateFragments(0);
                //    yield return new WaitForSeconds(3f);
                //}  
            }
            yield return new WaitForSeconds(3f);
        }
        menu.ShowGameOver();
        yield break;
    }
    //private void FixedUpdate()
    //{
    //    if (!isGameOver)
    //    {
    //        if (isStartGame)
    //            if (listFragments[0] == null)
    //            {
    //                listFragments.Clear();
    //                CreateFregments();
    //            }
    //    }
    //    else
    //    {
    //        menu.ShowGameOver();
    //    }
    //}


    public void DisableColliders()
    {
        for (int i = 0; i < fragments; i++)
        {
            listFragments[0].listFragments[i].DisableCollider();
            listFragments[0].listFragments[i].isPassed = true;
        }
        speed += accelerateSpeed;
        //wave.RemoveAt(0);
    }

    public void CreateFragments(int waveIndex)
    {
        fragments = wave[waveIndex].listColors.Count;
        for (int i = 0; i < fragments; i++)
        {
            var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);
            listFragments[waveIndex].listFragments.Add(frag.GetComponent<LineController>());
            listFragments[waveIndex].listFragments[i].SetVariables(palette.colors[wave[waveIndex].listColors[i]],
                                            fragments, radius, startPosition, speed);
            frag.layer = wave[0].listColors[i] + 10;
            startPosition = listFragments[waveIndex].listFragments[i].endPosition;
        }
        ChoiceColor();
        countCircle++;
        //fragments = Random.Range(3, 6);
        //for (int i = 0; i < fragments; i++)
        //{
        //    var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);
        //    listFragments.Add(frag.GetComponent<LineController>());
        //    int layer = Random.Range(0, 8) + 10;
        //    if (layer == player.layer)
        //    {
        //        isTherePlayerColor = true;
        //    }
        //    if (i == fragments - 1 && !isTherePlayerColor)
        //    {
        //        layer = player.layer;
        //       // Debug.Log(layer);
        //    }
        //    frag.layer = layer;
        //    listFragments[i].SetVariables(palette.colors[layer - 10], fragments, radius, startPosition, speed);            
        //    startPosition = listFragments[i].endPosition;
        //    Debug.Log(startPosition);
        //}
        //isTherePlayerColor = false;
    }
}
