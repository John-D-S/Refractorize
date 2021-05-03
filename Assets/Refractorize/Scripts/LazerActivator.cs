using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class LazerActivator : MonoBehaviour
{
    [SerializeField] private Activatable objectToActivate;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activatedSprite;
    private Sprite deactivatedSprite;

    public void Activate()
    {
        objectToActivate.Activate();
        spriteRenderer.sprite = activatedSprite;
    }

    public void Deactivate()
    {
        objectToActivate.Deactivate();
        spriteRenderer.sprite = deactivatedSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Activator";
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        deactivatedSprite = spriteRenderer.sprite;
    }
}
