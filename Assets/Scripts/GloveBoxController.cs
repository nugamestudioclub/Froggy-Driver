using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class GloveBoxController : MonoBehaviour
{

    bool open = false;

    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private Sprite closedSprite;

    [SerializeField]
    GameObject cross;

    [SerializeField]
    [Range(0, short.MaxValue)]
    private int openSortingOrder;

    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!open && collision.gameObject.TryGetComponent(out KeyController key)
            && key.IsHeld )
        {
            //change sprite to open
            sprite.sprite = openSprite;
            sprite.sortingOrder = openSortingOrder;
            //spawn cross
            Instantiate(cross, boxCollider.bounds.center, Quaternion.identity);
            //make open
            open = true;
        }

 
    }
}
