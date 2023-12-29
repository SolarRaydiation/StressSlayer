using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonRainScript : MonoBehaviour
{
    /*
     * This script is rather special because it is written specifically to provide the code
     * to inflict damage on an entity (player or an enemy) on collision.
     */
    [SerializeField] EntityStatsScript ess;
    [SerializeField] private const float POISON_RAIN_DAMAGE = 1f;
    void Start()
    {
        ess = gameObject.GetComponent<EntityStatsScript>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Particle collision with {other.gameObject.name}");
        ess.DecreaseHealth(POISON_RAIN_DAMAGE);
    }
}
