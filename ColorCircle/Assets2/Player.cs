using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    public int nextColor;
    private ParticleSystem.MainModule particle;
    private ParticleSystem.MainModule particle2;
    public int layer;



    void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ-90);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKey("escape"))
        {
            menu.PauseOn();
        }
    }

    private IEnumerator Dash()
    {
        while (cursorPosition.localPosition.y < distanseDash)
        {
            Vector3 newPos = new Vector3(0f,
                                cursorPosition.localPosition.y + speedDash * Time.deltaTime,
                                0f);
            collider2d.offset = newPos;
            cursorPosition.localPosition = newPos;
            yield return new WaitForSeconds(0.02f);
        }
        while (cursorPosition.localPosition.y > 4)
        {
            Vector3 newPos = new Vector3(0f,
                                cursorPosition.localPosition.y - speedDash / 1.5f * Time.deltaTime,
                                0f);
            collider2d.offset = newPos;
            cursorPosition.localPosition = newPos;
            yield return new WaitForSeconds(0.02f);
        }
        yield break;
    }

    private void Awake()
    {
        particle = winnerPointParcticle.GetComponent<ParticleSystem>().main;
        particle2 = winnerPointParcticle2.GetComponent<ParticleSystem>().main;
        ChangeColor();
        //circle.CreateFregments();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.layer.Equals(collision.gameObject.layer))
        {
            winnerPointParcticle.transform.position = cursorPosition.position;
            winnerPointParcticle.SetActive(true);
            winnerPointParcticle2.SetActive(true);
            circle.DisableColliders();
            circle.DeleteCurrentWaveAndCreateNew();
            circle.ChoiceColor();
            ChangeColor();
            menu.UpPoints();
            
            //circle.CreateFregments();
        }
        else
        {
            sprite.enabled = false;
            circle.isGameOver = true;
        }
         
    }

    public void ChangeColor()
    {
        //layer = Random.Range(0, 8) + 10;
        //gameObject.layer = layer;
        
        layer = nextColor + 10;
        this.gameObject.layer = layer;
        sprite.color = palette.colors[nextColor];
        Invoke("ChangehColorParticelDelay", 0.5f);
    }

    private void ChangehColorParticelDelay()
    {
        particle.startColor = palette.colors[nextColor];
        particle2.startColor = palette.colors[nextColor];
    }

}
