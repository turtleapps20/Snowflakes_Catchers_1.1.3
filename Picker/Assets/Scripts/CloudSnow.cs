using UnityEngine;

public class CloudSnow : MonoBehaviour
{
    public GameObject snowPrefab; //������ �������� ��������
    public float speed = 1f; //�������� �������� ������
    public float leftAndRightEdge = 10f; //���������� ����� ������� ������ ���������� ����������� �������� ������
    public float chanceToChangeDirections = 0.1f; //����������� ���������� ��������� ����������� ��������
    public float secondsBetweenSnowDrops = 1f; //������� �������� ��������

    private void Start()
    {
        Invoke("DropSnow", 2f); //���������� �������� ����� ������ 2 �������
    }

    void DropSnow()
    {
        GameObject snow = Instantiate(snowPrefab); //�������� ��������
        snow.transform.position = transform.position; //�������������� �������� ����� �������������� ������
        Invoke("DropSnow", secondsBetweenSnowDrops);//���������� ��������
    }

    private void Update()
    {
        //����������� �����������
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //��������� �����������
        if(pos.x < -leftAndRightEdge) //���� ������� ������� � < �������������� �������� leftAndRightEdge, ��
        {
            speed = Mathf.Abs(speed); //������ �������� ������
        }
        else if(pos.x > leftAndRightEdge) //���� ������� ������� � > leftAndRightEdge, ��
        {
            speed = -Mathf.Abs(speed); //������ �������� �����
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections) //���� ��������� �������� ������ chanceToChangeDirections, ��
        {
            speed *= -1; //�������� ����������� �������� ������
        }
    }
}
