using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float HealthDepletionTime = 0.1f;
    [SerializeField]
    private float RechargeHealthTime = 0.1f;
    private float RechargeHealthTimer = 0;
    [System.NonSerialized]
    public float Health = 1;

    [SerializeField]
    private int MaxLives = 3;
    [System.NonSerialized]
    public int Lives;
    private CharacterController characterController;

    [SerializeField]
    RectTransform healthBar;
    [SerializeField]
    RectTransform healthHeart;

    [SerializeField]
    private Teleporter levelStartTeleporter;
    [SerializeField]
    private SceneSwitcher sceneSwitcher;

    void Start()
    {
        Lives = MaxLives;
        characterController = gameObject.GetComponent<CharacterController>();
    }
    
    public void LazerBurn()
    {
        if (Health > 0)
        {
            Health -= Time.deltaTime / HealthDepletionTime;
            RechargeHealthTimer = 0;
        }
        else
        {
            Health = 0;
            LooseALife();
        }
    }

    void LooseALife()
    {
        if (Lives > 0)
        {
            Lives -= 1;
            Destroy(healthBar.GetChild(0).gameObject);
            Health = 1;
            characterController.DropObject();
            gameObject.transform.position = levelStartTeleporter.transform.position;
        }
        else
        {
            sceneSwitcher.SwitchScene(gameObject.scene.name);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (RechargeHealthTimer > RechargeHealthTime)
        {
            Health = 1;
        }
        else
        {
            RechargeHealthTimer += Time.deltaTime;
        }
    }
}
