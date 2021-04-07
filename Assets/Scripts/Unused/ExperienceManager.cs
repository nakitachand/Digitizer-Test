using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnInitialize;

    [SerializeField]
    private UnityEvent OnDisabled;

    private void OnEnable()
    {
        OnInitialize?.Invoke();
    }

    private void OnDisable()
    {
        OnDisabled?.Invoke();
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
