using UnityEngine;

public class CircleShape : MonoBehaviour, IShape
{
    [SerializeField] private GameObject fragment;
    [SerializeField] private Palette palette;
    [SerializeField] private float size;
    [SerializeField] private float speedReduction, speedRotate;
    private float startPosition = 0;
    private float angle;
    private GameObject[] fragments;

   
    //private void Start()
    //{
    //    int[] aa = new int[] { 1, 2, 3, 5 };

    //    SetVariable(aa, size, speedReduction, speedRotate);
    //}

    public void SetVariable(int[] colorIndexs, float size, float speedReduction, float speedRotate)
    {
        fragments = new GameObject[colorIndexs.Length];
        for (int i = 0; i < colorIndexs.Length; i++)
        {
            fragments[i] = ShapePool.Instance.GetFromPool(fragment.name, Vector3.zero, transform.rotation);
            fragments[i].gameObject.transform.SetParent(gameObject.transform);
            //var frag = Instantiate(fragmentCircle, Vector3.zero, transform.rotation);            
            var lc = fragments[i].GetComponent<LineController>();
            fragments[i].layer = colorIndexs[i] + 10;
            lc.SetVariables(palette.colors[colorIndexs[i]], size, colorIndexs.Length, startPosition);
            startPosition = lc.endPosition;
            //Debug.Log(startPosition);
            fragments[i].SetActive(true);
            fragments[i].GetComponent<LineCollision>().SetColliderPosition();
        }
        this.speedReduction = speedReduction;
        this.speedRotate = speedRotate;
        angle = Random.Range(0, 360);

    }

    private void FixedUpdate()
    {
        var scale = transform.localScale.x - speedReduction * Time.fixedDeltaTime;
        angle += speedRotate * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.localScale = new Vector3(scale, scale, 1f);
        if (scale < 0)
        {
            gameObject.SetActive(false);
            //gameObject.transform.DetachChildren();
        }
        
    }

    public void DisableShape()
    {

        for (int i = 0; i < fragments.Length; i++)
        {
            fragments[i].GetComponent<LineController>().DisableFragment(transform);
        }
        //gameObject.transform.DetachChildren();
        gameObject.SetActive(false);
    }

    //private void OnDisable()
    //{

    //    if (fragments != null)
    //    {
    //        for (int i = 0; i < fragments.Length; i++)
    //        {
    //            fragments[i].SetActive(false);

    //        }
    //    }
    //    fragments = null;
    //}
}
