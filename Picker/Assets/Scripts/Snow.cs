using UnityEngine;

public class Snow : MonoBehaviour
{
    public static float bottomY = -8f;

    void Update()
    {
        if(transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
}
