using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(LineRenderer))]
public class LazerActivator : MonoBehaviour
{
    [SerializeField] private Activatable objectToActivate = null;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite activatedSprite;
    private Sprite deactivatedSprite;

    [SerializeField]
    private bool bonusActivator;
    [SerializeField]
    private bool showConnection;
    private LineRenderer connectionLine;

    public bool laserPassThrough;

    private float timeUntilDeactivationMax = 0.1f;
    private float timeUntilDeactivation;
    [System.NonSerialized]
    public bool activated;

    public void Activate()
    {
        if (!bonusActivator && objectToActivate)
        {
            objectToActivate.Activate();
        }
        activated = true;
        spriteRenderer.sprite = activatedSprite;
        timeUntilDeactivation = timeUntilDeactivationMax;
    }

    public void Deactivate()
    {
        if (!bonusActivator && objectToActivate)
        {
            objectToActivate.Deactivate();
        }
        activated = false;
        spriteRenderer.sprite = deactivatedSprite;
    }

    private void UpdateConnectionLine()
    {
        connectionLine.positionCount = 2;
        connectionLine.SetPosition(0, gameObject.transform.position - Vector3.forward * 0.01f);
        connectionLine.SetPosition(1, objectToActivate.gameObject.transform.position - Vector3.forward * 0.01f);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Activator";
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        deactivatedSprite = spriteRenderer.sprite;
        if (showConnection)
        {
            connectionLine = gameObject.GetComponent<LineRenderer>();
            UpdateConnectionLine();
        }
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
