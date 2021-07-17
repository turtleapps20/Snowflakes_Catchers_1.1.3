using UnityEngine;

public class CloudSnow : MonoBehaviour
{
    public GameObject snowPrefab; //шаблон создания снежинок
    public float speed = 1f; //скорость движения облака
    public float leftAndRightEdge = 10f; //расстояние через которое должно измениться направление движения облака
    public float chanceToChangeDirections = 0.1f; //вероятность случайного изменения направления движения
    public float secondsBetweenSnowDrops = 1f; //частота создания снежинок

    private void Start()
    {
        Invoke("DropSnow", 2f); //сбрасывать снежинки через каждые 2 секунды
    }

    void DropSnow()
    {
        GameObject snow = Instantiate(snowPrefab); //создаётся снежинка
        snow.transform.position = transform.position; //местоположение снежинки равно местоположению облака
        Invoke("DropSnow", secondsBetweenSnowDrops);//сбрасывает снежинку
    }

    private void Update()
    {
        //выполняется перемещение
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //изменение направления
        if(pos.x < -leftAndRightEdge) //если текущая позиция х < отрицательного значения leftAndRightEdge, то
        {
            speed = Mathf.Abs(speed); //начать движение вправо
        }
        else if(pos.x > leftAndRightEdge) //если текущая позиция х > leftAndRightEdge, то
        {
            speed = -Mathf.Abs(speed); //начать движение влево
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections) //если случайное значение меньше chanceToChangeDirections, то
        {
            speed *= -1; //изменить направление движения облака
        }
    }
}
