using UnityEngine;
using UnityEngine.UI;

public class Snowman : MonoBehaviour
{
    public float speed;
    public float moveInput;
    private bool facingRight = true;
    private Rigidbody2D rb;

    public int lives = 5;

    [SerializeField] private int snow;
    [SerializeField] private Text snowText;

    [Header("Effects")]
    public GameObject effect;
    public GameObject effectStar;

    public GameObject shild;

    public Joystick joystick;

    private Animator camAnim;

    [Header("Sound")]
    public GameObject soundSnow;
    public GameObject soundLightning;

    [Header("Ads")]
    public InterAd interAd;//межстраничная реклама
    private int tryCount;//число попыток

    void Start()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        tryCount = PlayerPrefs.GetInt("tryCount");
    }

    void FixedUpdate()
    {
        snow = PlayerPrefs.GetInt("Snowflake");
        snowText.text = snow.ToString();

        //движение с помощью джойстика
        moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
        Damage();
    }

    void Flip()//поворот персонажа
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Snowflake")//обработка столкновения с снежинками
        {
            Instantiate(soundSnow, transform.position, Quaternion.identity);//воспроизведение звука
            Instantiate(effect, transform.position, Quaternion.identity);//спавн частиц при столкновении с объектом
            snow++;//увеличение счёта снежинок
            PlayerPrefs.SetInt("Snowflake", snow);
            snowText.text = snow.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Lightning")) //если столкнулся с молнией
        {
            Instantiate(soundLightning, transform.position, Quaternion.identity);//воспроизведение звука
            camAnim.SetTrigger("snake");//воспроизведение анимации столкновения
            Instantiate(effectStar, transform.position, Quaternion.identity);//спавн частиц при столкновении с объектом
            HeartSystem.health -= 1; //отнимается 1 жизнь
            lives--;
            Destroy(collision.gameObject);//молния уничтожается
        }
    }

    public void Damage()
    {
        if (lives <= 0)//если кол-во жизней <= 0, то
        {
            tryCount++;//увеличивается кол-во попыток
            PlayerPrefs.SetInt("tryCount", tryCount);//кол-во попыток сохраняется в реестр
            if(tryCount % 2 == 0)//каждую 2-ю попытку
            {
                interAd.ShowAd();//межстраничная реклама отображается
            }

            Camera.main.GetComponent<UIManager>().Lose();//вызов панели проигрыша
            gameObject.SetActive(false); //снеговик уничтожается
        }
    }
}
