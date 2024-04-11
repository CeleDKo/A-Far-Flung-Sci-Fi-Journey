using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private float speed = 2;
    private Vector2 vectorDirection = Vector2.zero;
    private const float destroyXPosition = 16;

    private Transform player;
    private bool toPlayer = false;

    private void Start()
    {
        vectorDirection = direction == Direction.Left ? Vector2.left : Vector2.right;
    }

    private void Update()
    {
        if (toPlayer && player != null)
        {
            vectorDirection = player.position - transform.position;
        }

        transform.Translate(speed * Time.deltaTime * vectorDirection.normalized, Space.World);
        if (transform.position.x > destroyXPosition || transform.position.x < -destroyXPosition) Destroy(gameObject);
    }


    public void EnableMovingToPlayer(Transform player)
    {
        this.player = player;
        speed *= 2;
        toPlayer = true;
    }
    public void DisableMovingToPlayer()
    {
        toPlayer = false;
    }
    private enum Direction
    {
        Left,
        Right
    }
}