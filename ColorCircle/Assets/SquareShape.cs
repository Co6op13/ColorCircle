using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareShape : MonoBehaviour, IShape
{
    [SerializeField] private Line[] lines;
    [SerializeField] private Palette palette;
    [SerializeField] private float lengthLine;
    [SerializeField] private float speedReduction, speedRotate;
    private float angle;

    private void OnEnable()
    {
        lines[0].startPos = new Vector2(lengthLine, -lengthLine);
        lines[0].endPos = new Vector2(lengthLine, lengthLine);
        lines[1].startPos = new Vector2(lengthLine, lengthLine);
        lines[1].endPos = new Vector2(-lengthLine, lengthLine);
        lines[2].startPos = new Vector2(-lengthLine, lengthLine);
        lines[2].endPos = new Vector2(-lengthLine, -lengthLine);
        lines[3].startPos = new Vector2(-lengthLine, -lengthLine);
        lines[3].endPos = new Vector2(lengthLine, -lengthLine);
    }


    private void FixedUpdate()
    {
        var scale = transform.localScale.x -  speedReduction * Time.fixedDeltaTime;
        angle += speedRotate * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.localScale = new Vector3(scale, scale, 1f);
        if(scale < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetVariable(int[] colorIndexs, float speedReduction, float speedRotate)
    {
        for(int i = 0; i < lines.Length; i++)
        {
            lines[i].SetColor(palette.colors[colorIndexs[i]]);
            lines[i].gameObject.layer = colorIndexs[i] + 10;
        }
        this.speedReduction = speedReduction;
        this.speedRotate = speedRotate;

        angle = Random.Range(0, 360);
    }
}
