using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _patientPrefab;
    [SerializeField]
    private int _num;
    [SerializeField]
    private float _intervalStep;

    WaitForSeconds _delay;
    // Start is called before the first frame update
    private void Start()
    {
        _delay = new WaitForSeconds(_intervalStep);
        StartCoroutine(SpawnPatients());
    }
    private IEnumerator SpawnPatients()
    {
        for (int i = 0; i < _num; i++)
        {
            yield return _delay;
            SpawnPatient();
        }
    }
    private void SpawnPatient()
    {
        Instantiate(_patientPrefab, this.transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
}
