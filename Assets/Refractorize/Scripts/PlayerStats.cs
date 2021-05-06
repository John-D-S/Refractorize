using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float HealthDepletionTime = 0.1f;
    [System.NonSerialized]
    public float Health = 1;

    [SerializeField]
    private int MaxLives = 3;
    [System.NonSerialized]
    public int Lives;

    [SerializeField]
    private Teleporter levelStartTeleporter;
    [SerializeField]
    private SceneSwitcher sceneSwitcher;

    public void LazerBurn()
    {
        if (Health > 0)
        {
            Health -= Time.deltaTime / HealthDepletionTime;
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
            Health = 1;
            gameObject.transform.position = levelStartTeleporter.transform.position;
        }
        else
        {
            sceneSwitcher.SwitchScene(gameObject.scene);
        }
    }

    //void Resta

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
