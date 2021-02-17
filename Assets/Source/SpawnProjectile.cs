using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueEyebrow;
using RogueEyebrow.Variables;
using SciFiArsenal;

public class SpawnProjectile : MonoBehaviour
{
    public FloatReference speed;
    public Vector3Variable firingSpeed;
    public FloatReference gravity;
    public Transform spawnPosition;
    public TriggerVariable triggerVariable;
    public Projectile projectile;
    [Layer]
    public int projectileLayer;
    public TriggerDelegate onTrigger;
    public BooleanVariable canFire;

    public void OnEnable()
    {
        onTrigger = new TriggerDelegate(Spawn);
        if (triggerVariable)
        {
            triggerVariable.RegisterForTrigger(onTrigger);
        }
    }

    public void OnDisable()
    {
        if (triggerVariable)
        {
            triggerVariable.UnregisterForTrigger(onTrigger);
        }
    }

    public void Spawn()
    {
        if (!canFire.Value)
        {
            return;
        }
        GameObject go = Instantiate(projectile.prefab, spawnPosition.position, Quaternion.identity) as GameObject;
        BallisticMotion motion = go.AddComponent<BallisticMotion>();
        go.transform.rotation = spawnPosition.rotation;
        go.layer = projectileLayer;
        go.GetComponent<ProjectileLifetimeComponent>().impactNormal = go.transform.forward * -1;
        
        motion.Initialize(spawnPosition.position, gravity);
        motion.AddImpulse(firingSpeed.Value);// * speed);

        if (projectile.muzzleFlash)
        {
            GameObject muzzleFlash = Instantiate(projectile.muzzleFlash, go.transform.position, go.transform.rotation) as GameObject;
            muzzleFlash.transform.rotation = go.transform.rotation * Quaternion.Euler(180, 0, 0);
            Destroy(muzzleFlash, 1.5f); // Lifetime of muzzle effect.
        }
    }
}
