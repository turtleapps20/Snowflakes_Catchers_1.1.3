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
    public InterAd interAd;//������������� �������
    private int tryCount;//����� �������

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

        //�������� � ������� ���������
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

    void Flip()//������� ���������
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Snowflake")//��������� ������������ � ����������
        {
            Instantiate(soundSnow, transform.position, Quaternion.identity);//��������������� �����
            Instantiate(effect, transform.position, Quaternion.identity);//����� ������ ��� ������������ � ��������
            snow++;//���������� ����� ��������
            PlayerPrefs.SetInt("Snowflake", snow);
            snowText.text = snow.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Lightning")) //���� ���������� � �������
        {
            Instantiate(soundLightning, transform.position, Quaternion.identity);//��������������� �����
            camAnim.SetTrigger("snake");//��������������� �������� ������������
            Instantiate(effectStar, transform.position, Quaternion.identity);//����� ������ ��� ������������ � ��������
            HeartSystem.health -= 1; //���������� 1 �����
            lives--;
            Destroy(collision.gameObject);//������ ������������
        }
    }

    public void Damage()
    {
        if (lives <= 0)//���� ���-�� ������ <= 0, ��
        {
            tryCount++;//������������� ���-�� �������
            PlayerPrefs.SetInt("tryCount", tryCount);//���-�� ������� ����������� � ������
            if(tryCount % 2 == 0)//������ 2-� �������
            {
                interAd.ShowAd();//������������� ������� ������������
            }

            Camera.main.GetComponent<UIManager>().Lose();//����� ������ ���������
            gameObject.SetActive(false); //�������� ������������
        }
    }
}
