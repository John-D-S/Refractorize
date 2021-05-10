using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Serialization;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Teleporter : Activatable
{
    [SerializeField] private bool StartingTeleporter;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite StartingSprite;
    [SerializeField] private Sprite FinishSpriteOff;
    [SerializeField] private Sprite FinishSpriteOn;

    [SerializeField] private LevelSaver LevelSaver;

    private bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (!StartingTeleporter)
        {
            spriteRenderer.sprite = FinishSpriteOff;
        }
        else
        {
            spriteRenderer.sprite = StartingSprite;
        }
    }

    public override void Activate()
    {
        isOn = true;
        spriteRenderer.sprite = FinishSpriteOn;
    }

    public override void Deactivate()
    {
        isOn = false;
        spriteRenderer.sprite = FinishSpriteOff;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOn && collision.tag == "Player")
        {
            LevelSaver.CompleteLevel();
        }
    }
}
