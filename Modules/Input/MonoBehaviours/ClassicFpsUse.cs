using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ClassicFpsUse : MonoBehaviour
{
    public Collider ReferenceCollider;
    public ProximityChecker Detector;

    private bool active = false;

    private void Start()
    {
        Detector.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            active = true;
            StartCoroutine(Wait(0.1f));
        }
    }

    private void Interact()
    {
        List<GameObject> collidingWith = Detector.GetCurrentOverlapping();

        if (collidingWith.Count > 0)
        {
            IInteractible firstCollided = collidingWith.FirstOrDefault()
                .GetComponent<IInteractible>();

            if (firstCollided != null)
                firstCollided.Use(ReferenceCollider);
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        Detector.gameObject.SetActive(true);

        Interact();

        yield return new WaitForSeconds(waitTime);
        active = false;
        Detector.gameObject.SetActive(false);
    }
}