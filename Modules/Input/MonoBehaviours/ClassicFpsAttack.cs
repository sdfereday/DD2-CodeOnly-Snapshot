using UnityEngine;
using System.Collections;

public class ClassicFpsAttack : MonoBehaviour
{
    public GameObject AttackObject;

    private bool active = false;

    private void Start()
    {
        AttackObject.SetActive(false);
    }

    private void Update()
    {
        if(!active && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown(InputConsts.Fire)))
        {
            active = true;
            StartCoroutine(Wait(0.2f));
        }
    }

    private IEnumerator Wait(float waitTime)
    {
        AttackObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        active = false;
        AttackObject.SetActive(false);
    }
}