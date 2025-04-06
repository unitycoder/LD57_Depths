using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }

}
