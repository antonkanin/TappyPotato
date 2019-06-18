using System.Collections;
using Constants;
using UnityEngine;

public class DeathController : BaseTappyController
{
    [SerializeField] private GameObject eyePrefab = default;

    private bool eyeLost = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.AnyDeath())
        {
            GameManager.Instance.PlayerDied();
        }

        if (collider.DieAndLooseEye() && !eyeLost)
        {
            StartCoroutine(Co_SpawnEye());
            eyeLost = true;
        }
    }

    protected override void OnGameOverConfirmed()
    {
        eyeLost = false;
    }

    private IEnumerator Co_SpawnEye()
    {
        yield return new WaitForSeconds(DeathTiming.TopForksBeforeFreeze);

        var eyeObject = Instantiate(eyePrefab,
            transform.position + new Vector3(0.2f, 0.0f, 0.0f),
            Quaternion.identity);

        eyeObject.transform.parent = transform.parent;
    }
}