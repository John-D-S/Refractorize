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
    [SerializeField]
    private float laserStartOffset = 0.25f;

    bool activated = true;

    private LineRenderer lazerBeam;
    private List<Vector3> lazerPoints = new List<Vector3>();

    private Dictionary<Collider2D, LazerActivator> lazerActivatorsByCollider = new Dictionary<Collider2D, LazerActivator>();
    private List<LazerActivator> thisFrameActivators = new List<LazerActivator>();
    private List<LazerActivator> lastFrameActivators = new List<LazerActivator>();

    Collider2D refractor = null;

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
            Vector2 position = gameObject.transform.position + gameObject.transform.right * laserStartOffset;
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

    /*
    private Vector2 AngleToVector2(float degrees, Vector2 originalDirection)
    {
        float radians = Mathf.Deg2Rad * (degrees + Vector2.SignedAngle(Vector2.up, originalDirection));
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }
    private Vector2 AngleToVector2(float degrees)
    {
        float radians = Mathf.Deg2Rad * (degrees);
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    private Vector2 Refract(Vector2 _incomingDirection, Vector2 _normal, float materialIOR)
    {

        if (Vector2.Angle(_normal, _incomingDirection) > 90) //going into the material
        {
            float angle = Mathf.Asin(Mathf.Sin(Vector2.SignedAngle(_incomingDirection, _normal) / materialIOR));
            return AngleToVector2(angle, _normal);
        }
        else //coming out of the material.
        {
            float angle = Mathf.Asin(materialIOR * Mathf.Sin(Vector2.SignedAngle(_incomingDirection, _normal)));
            return AngleToVector2(angle, _normal);
        }
    }
    */

    private Vector2 Refract(Vector2 incidentVector, Vector2 normal, float indexOfRefraction)
    {
        indexOfRefraction = 1 / indexOfRefraction;
        float N_dot_I = Vector2.Dot(normal, incidentVector);
        float k = 1f - indexOfRefraction * indexOfRefraction * (1 - N_dot_I * N_dot_I);
        if (k < 0)
            return Vector2.zero;
        else
            return indexOfRefraction * incidentVector - (indexOfRefraction * N_dot_I + Mathf.Sqrt(k)) * normal;
    }


    private void ShootLazer(Vector2 _position, Vector2 _direction, out Vector2 _nextPosition, out Vector2 _nextRotation, out bool _shootLazer)
    {
        _shootLazer = true;
        if (!refractor)
        {
            RaycastHit2D hit = Physics2D.Raycast(_position, _direction);
            _nextPosition = hit.point;
            _nextRotation = Vector2.zero;
            if (hit)
            {
                switch (hit.collider.tag)
                {
                    case "Mirror":
                        _nextPosition = hit.point;
                        _nextRotation = Vector2.Reflect(_direction, hit.normal).normalized;
                        return;
                    case "Refractor":
                        //Debug.Log("going in");
                        
                        /*
                        //https://www.reddit.com/r/Unity3D/comments/7k9wwi/laser_refraction_problem/
                        Debug.Log("going out");
                        _nextRotation = Refract(_direction, hit.normal, 1.55f);
                        */

                        //Debug.Log(_nextRotation);
                        _nextRotation = Refract(_direction, hit.normal, 1.55f);
                        _nextPosition = hit.point + _nextRotation * 0.01f;
                        refractor = hit.collider;
                        return;
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
        else
        {
            Vector2 backwardsRayPosition = _position + _direction * 10;
            Vector2 backwardsRayDirection = -_direction;

            Vector2 materialExitPoint = _position;
            Vector2 materialExitNormal = Vector2.zero;

            RaycastHit2D[] allHits = Physics2D.RaycastAll(backwardsRayPosition, backwardsRayDirection);
            foreach (RaycastHit2D hit in allHits)
                if (hit.collider == refractor)
                {
                    materialExitPoint = hit.point;
                    materialExitNormal = - hit.normal;
                    break;
                }

            _nextRotation = Refract(_direction, materialExitNormal, 1 / 1.55f);
            if (_nextRotation == Vector2.zero)
            {
                _nextRotation = Vector2.Reflect(_direction, materialExitNormal);
            }
            else
            {
                refractor = null;
            }
            Debug.Log(_nextRotation);
            _nextPosition = materialExitPoint + _nextRotation * 0.01f;
        }
    }
}
