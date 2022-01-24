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

    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!open && collision.gameObject.TryGetComponent(out KeyController key))
        {
            //change sprite to open
            sprite.sprite = openSprite;
            //spawn cross
            Instantiate(cross, boxCollider.bounds.center, Quaternion.identity);
            //make open
            open = true;
        }

 
    }
}
