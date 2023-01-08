using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Palette palette;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform cursorPosition;
    [Space (4)]
    [SerializeField] private float speedDash;
    [SerializeField] private float distanseDash;
    [SerializeField] private float delayDash;
    [Space (4)]
    [SerializeField] private BoxCollider2D collider2d;

    private bool canDash = true;
    private bool isPause;
    private int layer;
    private bool triger;
    private Vector2 startPosCurcosr;


    public static Player Instance;

    private void Awake()
    {
        startPosCurcosr = cursorPosition.position;
        Instance = this;
    }
    public void SetLayser(int layser)
    {
        this.layer = layser;
    }

    private void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDash && !isPause && sprite.enabled)
        {
            //audioDash.pitch = Random.Range(0.7f, 1.3f);
            //audioDash.Play();
            StartCoroutine(Dash());
            canDash = false;
            Invoke("ResetDash", delayDash);
        }

        if (Input.GetKey("escape"))
        {
           // isPause = true;
            //menu.PauseOn();
        }
    }

    private void ResetDash()
    {
        canDash = true;
    }
    private IEnumerator Dash()
    {
        triger = true;
        while (triger && cursorPosition.localPosition.y < distanseDash)
        {
            Vector3 newPos = new Vector3(0f, cursorPosition.localPosition.y + speedDash * Time.fixedDeltaTime, 0f);
            collider2d.offset = newPos;
            cursorPosition.localPosition = newPos;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        triger = false;
        collider2d.offset = startPosCurcosr;
        cursorPosition.localPosition = startPosCurcosr;
        yield return new WaitUntil(() => canDash == true);
        collider2d.enabled = true;
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider2d.enabled = false;
        triger = false;
        if (gameObject.layer.Equals(collision.gameObject.layer))
        {

            MyEventManager.SendIncreasePoints();
            //GameManager.Instance.DisableCurrentShape();

//            winnerPointParcticle.transform.position = cursorPosition.position;
  //          winnerPointParcticle.SetActive(true);
    //        winnerPointParcticle2.SetActive(true);
      //      circle.DestroyCircle();
        //    ChangeColor();
          //  menu.UpPoints();

        }
        else
        {
            Debug.Log(gameObject.layer +"  " + collision.gameObject.layer);
            Debug.Break();
            //sprite.enabled = false;
            //circle.isGameOver = true;
            //music.StopMusic();
            //audioGameOver.Play();
        }
    }

    public void SetColorAndLayer(int indexColor)
    {
        sprite.color = palette.colors[indexColor];
        gameObject.layer = indexColor + 10;
    }
}
