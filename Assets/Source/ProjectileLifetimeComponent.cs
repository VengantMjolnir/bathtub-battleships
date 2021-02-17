using UnityEngine;
using System.Collections;
using RogueEyebrow.Variables;

public class ProjectileLifetimeComponent : MonoBehaviour
{
    private float elapsedTime = 0.0f;

    public FloatReference lifetime = new FloatReference(3);
    public FloatReference waterHeight = new FloatReference(-0.25f);
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject splashParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

    private bool hasCollided = false;
    private Transform cachedTransform;

    // Use this for initialization
    void Start()
    {
        cachedTransform = transform;
        elapsedTime = 0.0f;
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifetime)
        {
            Impact();
        }

        if (cachedTransform.position.y < waterHeight)
        {
            Impact(true);
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (!hasCollided)
        {
            hasCollided = true;

            Impact();
        }
    }

    private void Impact(bool isWaterImpact = false)
    {
        if (isWaterImpact)
        {
            splashParticle = Instantiate(splashParticle, cachedTransform.position, Quaternion.Euler(-90f,0f,0f)) as GameObject;
            Destroy(splashParticle, 5f);
        } else
        {
            impactParticle = Instantiate(impactParticle, cachedTransform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            Destroy(impactParticle, 5f);
        }

        foreach (GameObject trail in trailParticles)
        {
            GameObject curTrail = cachedTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, 3f);
        }
        Destroy(projectileParticle, 3f);
        Destroy(gameObject);

        ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
        //Component at [0] is that of the parent i.e. this object (if there is any)
        for (int i = 1; i < trails.Length; i++)
        {

            ParticleSystem trail = trails[i];

            if (trail.gameObject.name.Contains("Trail"))
            {
                trail.transform.SetParent(null);
                Destroy(trail.gameObject, 2f);
            }
        }
    }
}
