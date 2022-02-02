using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portal : MonoBehaviour
{
    private Collider2D myCollider;

    private bool Contains(Collider2D collision)
    {
        float x = myCollider.bounds.size.x / 2;
        float y = myCollider.bounds.size.y / 2;
        float minX = transform.position.x - x;
        float maxX = transform.position.x + x;
        float minY = transform.position.y - y;
        float maxY = transform.position.y + y;

        return minX <= collision.transform.position.x
            && collision.transform.position.x <= maxX
            && minY <= collision.transform.position.y
            && collision.transform.position.y <= maxY;
    }

    private void DiscardIfPossible(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GrabbableController grab)
            && grab.IsDiscardable && Contains(collision))
            grab.Discard();
    }

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        DiscardIfPossible(collision);
    }

    public void Spawn(GameObject obj)
    {

    }
}