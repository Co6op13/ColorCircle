using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayer : MonoBehaviour
{
    [SerializeField] private float deadZone = 80f;
    [SerializeField] private LineRenderer lineTarget;
    [SerializeField] private float lengthDirection = 10;
    [Space(4)]
    [SerializeField] private RandomMusik music;
    [SerializeField] private AudioSource audioDash;
    [SerializeField] private AudioSource audioGameOver;
    [SerializeField] private AudioSource[] audioUpPoint;
    [SerializeField] private UiMenu menu;
    [SerializeField] private Palette palette;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private DrawCircle circle;
    [SerializeField] private GameObject winnerPointParcticle;
    [SerializeField] private GameObject winnerPointParcticle2;
    [SerializeField] private Transform cursorPosition;
    [SerializeField] private float speedDash;
    [SerializeField] private BoxCollider2D collider2d;
    [SerializeField] private float distanseDash;
    [SerializeField] private float delayDash;
    [SerializeField] private Vector2 touchStart;
    [SerializeField] private Vector2 touchPosition;
    private Vector2 swipeDelta;

    private Vector2 direction;
    private bool isSwaiping;
    private bool isMobile;
    private bool isTouch;
    public bool isPause;
    private bool canDash = true;
    private ParticleSystem.MainModule particle;
    private ParticleSystem.MainModule particle2;
    [SerializeField] bool triger;
    public int layer;

    private void Awake()
    {
        particle = winnerPointParcticle.GetComponent<ParticleSystem>().main;
        particle2 = winnerPointParcticle2.GetComponent<ParticleSystem>().main;
        ChangeColor();
        //circle.CreateFregments();
    }


    private void Start()
    {
        isMobile = Application.isMobilePlatform;        
            MyEventManager.OnStartGame.AddListener(StartGame);
        
    }

    private void StartGame()
    {
        cursorPosition.gameObject.SetActive(true);
        //gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (isSwaiping)
        {
            lineTarget.enabled = true;
            lineTarget.startColor = palette.colors[layer - 10];
            float rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);
            lineTarget.SetPosition(0, Vector2.zero);
            lineTarget.SetPosition(1, direction * lengthDirection);
            if(!isTouch)
            {
                audioDash.pitch = Random.Range(0.7f, 1.3f);
                audioDash.Play();
                StartCoroutine(Dash());
                canDash = false;
                isSwaiping = false;
                Invoke("ResetDash", delayDash);
                lineTarget.enabled = false;

            }
        }

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = touch.position;
                    isTouch = true;
                    break;
                case TouchPhase.Ended:
                    isTouch = false;
                    break;
                case TouchPhase.Moved:
                    isSwaiping = true;
                    touchPosition = touch.position;
                    direction = (touch.position - touchStart).normalized;
                    break;
            }
        }
        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        isTouch = true;
        //        isSwiping = true;
        //        tapPosition = Input.GetTouch(0).position;
        //    }
        //    else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
        //        Input.GetTouch(0).phase == TouchPhase.Ended)
        //    {

        //    }
        //}
        //CheckSwipe();
        //if (direction != Vector2.zero)
        //{
        //    float rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);

        //    if (!isSwiping)
        //    {
        //        audioDash.pitch = Random.Range(0.7f, 1.3f);
        //        audioDash.Play();
        //        StartCoroutine(Dash());
        //        canDash = false;
        //        Invoke("ResetDash", delayDash);
        //        direction = Vector2.zero;
        //        ResetSwipe();
        //    }
        //}


        //Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotateZ - 90);
        //if (Input.GetKeyDown(KeyCode.Mouse0) && canDash && !isPause && sprite.enabled)
        //{
        //    audioDash.pitch = Random.Range(0.7f, 1.3f);
        //    audioDash.Play();
        //    StartCoroutine(Dash());
        //    canDash = false;
        //    Invoke("ResetDash", delayDash);
        //}

        //    if (Input.GetKey("escape"))
        //    {
        //        isPause = true;
        //        menu.PauseOn();
        //    }
        //}


    }

    //private void CheckSwipe()
    //{
    //    swipeDelta = Vector2.zero;
    //    if (isSwiping)
    //    {
    //        if (isMobile && Input.touchCount > 0)
    //            swipeDelta = Input.GetTouch(0).position - tapPosition;
    //    }

    //    if (swipeDelta.magnitude > deadZone)
    //    {
    //        direction = swipeDelta - tapPosition;
    //        lineTarget.positionCount = 2;
    //        lineTarget.SetPosition(0, Vector2.zero);
    //        lineTarget.SetPosition(1, direction * 3);
    //    }


    //}

    //private void ResetSwipe()
    //{
    //    isSwiping = false;
    //    tapPosition = Vector2.zero;
    //    swipeDelta = Vector2.zero;
    //}

    private void ResetDash()
    {
        canDash = true;
    }
    private IEnumerator Dash()
    {
        triger = true;
        while (triger && cursorPosition.localPosition.y < distanseDash)
        {
            Vector3 newPos = new Vector3(0f, cursorPosition.localPosition.y + speedDash * Time.deltaTime, 0f);
            collider2d.offset = newPos;
            cursorPosition.localPosition = newPos;
            yield return new WaitForSeconds(0.02f);
        }
        triger = false;
        while (cursorPosition.localPosition.y > 4)
        {
            Vector3 newPos = new Vector3(0f,
                                cursorPosition.localPosition.y - speedDash * Time.deltaTime,
                                0f);
            collider2d.offset = newPos;
            cursorPosition.localPosition = newPos;
            // yield return new WaitForSeconds(0.01f);
        }
        cursorPosition.localPosition = new Vector2(0f, 4f);
        yield break;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider2d.enabled = false;
        triger = false;
        if (gameObject.layer.Equals(collision.gameObject.layer))
        {
            StartCoroutine(SoundBlup());
            //audioUpPoint.pitch = Random.Range(0.7f, 1.3f);
            //audioUpPoint.Play();
            winnerPointParcticle.transform.position = cursorPosition.position;
            winnerPointParcticle.SetActive(true);
            winnerPointParcticle2.SetActive(true);
            circle.DestroyCircle();
            ChangeColor();
            menu.IncreasePoints();

            //circle.CreateFregments();
        }
        else
        {
            //Debug.Log(4);
            //Debug.Log(gameObject.layer + "   " + collision.gameObject.layer);
            //Debug.Break();
            sprite.enabled = false;
            circle.isGameOver = true;
            music.StopMusic();
            audioGameOver.Play();
        }
    }


    private IEnumerator SoundBlup()
    {
        //int count = Random.Range(5, 25);
        for (int i = 0; i < audioUpPoint.Length; i++)
        {
            audioUpPoint[i].volume = Random.Range(0.3f, 0.8f);
            audioUpPoint[i].pitch = Random.Range(0.7f, 1.3f);
            audioUpPoint[i].Play();
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
        }
        yield break;
    }

    private void ChangeColor()
    {
        layer = Random.Range(0, 8) + 10;
        gameObject.layer = layer;
        sprite.color = palette.colors[layer - 10];
        Invoke("ChangehColorParticelDelay", 0.5f);
        collider2d.enabled = true;
    }

    private void ChangehColorParticelDelay()
    {
        particle.startColor = palette.colors[layer - 10];
        particle2.startColor = palette.colors[layer - 10];
    }

}
