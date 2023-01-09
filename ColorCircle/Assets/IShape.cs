using UnityEngine;
public interface IShape
{
    void SetVariable(int[] colors, float size, float speedReduction, float  speedRotate);

    void DisableShape();
}