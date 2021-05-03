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

    public bool laserPassThrough;

    private float timeUntilDeactivationMax = 0.1f;
    private float timeUntilDeactivation;
    bool activated;

    public void Activate()
    {
        if (objectToActivate)
        {
            objectToActivate.Activate();
        }
        activated = true;
        spriteRenderer.sprite = activatedSprite;
        timeUntilDeactivation = timeUntilDeactivationMax;
    }

    public void Deactivate()
    {
        activated = false;
        if (objectToActivate)
        {
            objectToActivate.Deactivate();
        }
        spriteRenderer.sprite = deactivatedSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Activator";
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        deactivatedSprite = spriteRenderer.sprite;
    }

    private void FixedUpdate()
    {
        if (activated && timeUntilDeactivation > 0)
        {
            timeUntilDeactivation -= Time.fixedDeltaTime;
        }
        else
        {
            Deactivate();
        }
    }
}
