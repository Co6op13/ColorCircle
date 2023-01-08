using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListColor
{
    public List<int> listColors = new List<int>();

    public ListColor (Palette palette)
    {
        int count = Random.Range(3, 7);
        List<int> indexColors = new List<int>(count);
        for (int index = 0; index < palette.colors.Length; index++)
        {
            indexColors.Add(index);
        }
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, indexColors.Count);
            listColors.Add(indexColors[index]);
            indexColors.RemoveAt(index);
        }
    }

    //public void GenerateColors(Palette palette)
    //{
    //    int count = Random.Range(3, 7);
    //    List<int> indexColors = new List<int>(count);
    //    for (int index = 0; index < palette.colors.Length; index++)
    //    {
    //        indexColors.Add(index);
    //    }
    //    for (int i = 0; i < count; i++)
    //    {
    //        int index = Random.Range(0, indexColors.Count);
    //        listColors.Add(indexColors[index]);
    //        indexColors.RemoveAt(index);
    //    }
    //}
}
