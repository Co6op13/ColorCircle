using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle2 : MonoBehaviour
{
    [SerializeField] private GameObject fragmentCircle;
    [SerializeField] private List<LineController> fragmentsList;
    [SerializeField] private Palette palette;
    [SerializeField] private float offsetMagick = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        Draw();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Draw()
    {
        float startPos = 0f;        
    for (int i = 0; i < 4; i++)
        {
            var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);
            fragmentsList.Add(frag.GetComponent<LineController>());
            int layer = Random.Range(0, 8) + 10;
           // startPos = fragmentsList[i].SetVariables(palette.colors[layer - 10], 4, 20f, startPos, 5f, layer);
        }
    }
}
