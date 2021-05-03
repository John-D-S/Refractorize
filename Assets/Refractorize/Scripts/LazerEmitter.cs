using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LazerEmitter : Activatable
{
    [SerializeField]
    private int maxBeams = 50;
    [SerializeField]
    private bool shootOnlyWhenActivated;

    bool activated = true;

    private LineRenderer lazerBeam;
    private List<Vector3> lazerPoints = new List<Vector3>();

    private Dictionary<Collider2D, LazerActivator> lazerActivatorsByCollider = new Dictionary<Collider2D, LazerActivator>();
    private List<LazerActivator> thisFrameActivators = new List<LazerActivator>();
    private List<LazerActivator> lastFrameActivators = new List<LazerActivator>();


    public override void Activate()
    {
        activated = true;
    }

    public override void Deactivate()
    {
        activated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (shootOnlyWhenActivated)
        {
            activated = false;
        }
        
        lazerBeam = gameObject.GetComponent<LineRenderer>();
        
        foreach (LazerActivator lazerActivator in GameObject.FindObjectsOfType<LazerActivator>())
        {
            lazerActivatorsByCollider.Add(lazerActivator.gameObject.GetComponent<Collider2D>(), lazerActivator);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            lazerPoints.Clear();
            Vector2 position = transform.position;
            Vector2 direction = transform.right;
            int i = 0;
            bool shootLazer = true;
            while (i < maxBeams && shootLazer)
            {
                Vector3 lastPosition = new Vector3(position.x, position.y, -0.1f);
                lazerPoints.Add(lastPosition);
                ShootLazer(position, direction, out position, out direction, out shootLazer);
                //Vector3 newPosition = new Vector3(position.x, position.y, -0.1f);
                if (shootLazer)
                {
                    Vector2 startOfBeamAddition = (position - new Vector2(lastPosition.x, lastPosition.y)).normalized * 0.05f;
                    Vector2 endOfBeamAddition = Vector3.ClampMagnitude(position - new Vector2(lastPosition.x, lastPosition.y), 1) * 0.95f;

                    Vector3 StartWidthEveningPoint = lastPosition + new Vector3(startOfBeamAddition.x, startOfBeamAddition.y, -0.1f);
                    Vector3 EndWidthEveningPoint = lastPosition + new Vector3(endOfBeamAddition.x, endOfBeamAddition.y, -0.1f);
                    //Debug.DrawLine(StartWidthEveningPoint + Vector3.up * 0.1f, StartWidthEveningPoint - Vector3.up * 0.1f, Color.blue);
                    lazerPoints.Add(StartWidthEveningPoint);
                    lazerPoints.Add(EndWidthEveningPoint);
                }
                //Debug.Log(shootLazer);
                i++;
            }

            lazerBeam.positionCount = lazerPoints.Count;
            //Debug.Log(lazerPoints.Count);
            i = 0;
            foreach (Vector3 point in lazerPoints)
            {
                lazerBeam.SetPosition(i, point);
                i++;
            }

            foreach (LazerActivator lazerActivator in lastFrameActivators)
            {

                if (!thisFrameActivators.Contains(lazerActivator))
                    lazerActivator.Deactivate();
            }


            lastFrameActivators = thisFrameActivators;
            thisFrameActivators = new List<LazerActivator>();
        }
        else
        {
            lazerBeam.positionCount = 0;
        }
    }

    private void ShootLazer(Vector2 _position, Vector2 _direction, out Vector2 _nextPosition, out Vector2 _nextRotation, out bool _shootLazer)
    {
        RaycastHit2D hit = Physics2D.Raycast(_position, _direction);
        _nextPosition = hit.point;
        _nextRotation = Vector2.zero;
        _shootLazer = true;
        if (hit)
        {
            switch (hit.collider.tag)
            {
                case "Mirror":
                    _nextPosition = hit.point;
                    _nextRotation = Vector2.Reflect(_direction, hit.normal).normalized;
                    return;
                //case "Refractor":
                //    _nextPosition = hit.point;
                //    _nextRotation = Vector3.AngleBetween(_direction, hit.normal)
                case "Activator":
                    LazerActivator currentActivator = lazerActivatorsByCollider[hit.collider];
                    currentActivator.Activate();
                    if (currentActivator.laserPassThrough)
                    {
                        _nextPosition = hit.point + _direction * 0.01f;
                        _nextRotation = _direction;
                    }
                    if (!thisFrameActivators.Contains(currentActivator))
                    {
                        thisFrameActivators.Add(currentActivator);
                    }
                    return;
                default:
                    return;
            }
        }
        _shootLazer = false;
    }
}
