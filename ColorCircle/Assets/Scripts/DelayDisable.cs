using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDisable : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private void OnEnable()
    {
        Invoke("DisableGO", lifeTime);
    }

    void DisableGO ()
    {
        gameObject.SetActive(false);
    }
}
