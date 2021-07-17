using UnityEngine;

public class Lightning : MonoBehaviour
{
    public static float bottomY = -8f;

    private void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
        }
    }
}
