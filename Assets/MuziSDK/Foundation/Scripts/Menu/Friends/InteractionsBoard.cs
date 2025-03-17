using UnityEngine;

public class InteractionsBoard : MonoBehaviour
{
    public void Init(Vector2 openPosition)
    {
        transform.parent.position = openPosition;
    }
}
