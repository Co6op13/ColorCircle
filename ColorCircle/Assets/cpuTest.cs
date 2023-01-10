using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuTest : MonoBehaviour
{
    [SerializeField] private GameObject testCube;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                var t = Instantiate(testCube, Vector2.zero, transform.rotation);
                t.gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            }
            yield return new WaitForSeconds(0.1f);
            count += 10;
        }
    }
}
