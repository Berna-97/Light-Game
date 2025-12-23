using UnityEngine;

public class Repetition : MonoBehaviour
{
    public GameObject target;
    public float speed = 50f;
    public float explodeDistance = 2.5f;
    public float damage = 1f;
    public float arcHeight = 1f; // Height of the arc

    private EnemyMoveScript enemyScript;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float journeyLength;
    private float distanceTraveled;
    private float randomArcModifier; // Random variation for arc
    private Vector3 randomArcDirection; // Random direction for the arc

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            enemyScript = target.GetComponent<EnemyMoveScript>();

            // Initialize arc movement
            startPosition = transform.position;
            targetPosition = target.transform.position;
            journeyLength = Vector3.Distance(startPosition, targetPosition);
            distanceTraveled = 0f;

            // Random arc modifier between 0.5 and 1.5 (50% to 150% of arcHeight)
            randomArcModifier = Random.Range(0.5f, 1.5f);

            // Generate a random perpendicular direction for the arc
            Vector3 directionToTarget = (targetPosition - startPosition).normalized;
            Vector3 randomPerpendicular = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            // Make sure it's perpendicular to the travel direction
            randomArcDirection = Vector3.Cross(directionToTarget, randomPerpendicular).normalized;
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Update target position (in case enemy moves)
        targetPosition = target.transform.position;

        // Move along the arc
        distanceTraveled += speed * Time.deltaTime;
        float fractionOfJourney = distanceTraveled / journeyLength;

        // Linear interpolation between start and target
        Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

        // Add arc in random direction (parabolic curve) - peaks at 0.5 (middle of journey)
        float arcProgress = Mathf.Sin(fractionOfJourney * Mathf.PI);
        currentPos += randomArcDirection * (arcProgress * arcHeight * randomArcModifier);

        transform.position = currentPos;

        // Make rocket look in the direction it's moving
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Destroy when close to target
        if (Vector3.Distance(transform.position, target.transform.position) < explodeDistance)
        {
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage, transform.position);
            }
            Destroy(gameObject);
        }
    }
}