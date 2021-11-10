using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _shellPrefab;
    [SerializeField]
    private GameObject _shellSpawnPosition;
    [SerializeField]
    private GameObject _parent;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _projectileSpeed = 15;
    [SerializeField]
    private float _turnSpeed = 2;
    [SerializeField]
    private float _fireDelay = 0.5f;


    private bool canShoot = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = (_target.transform.position - _parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _parent.transform.rotation = Quaternion.Slerp(_parent.transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
        float? angle = RotateTurret();
        if (angle != null && Vector3.Angle(direction, _parent.transform.forward) < 10 && canShoot)
        {
            Fire();
            canShoot = false;
            StartCoroutine(ResetCanShoot());
        }
    }
    private IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(_fireDelay);
        canShoot = true;
    }
    private void Fire()
    {
        GameObject shell = Instantiate(_shellPrefab, _shellSpawnPosition.transform.position, _shellSpawnPosition.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = _projectileSpeed * this.transform.forward;
    }
    private float? RotateTurret()
    {
        float? angle = CalculateAngle(false);
        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0, 0);
        }
        return angle;
    }
    private float? CalculateAngle(bool low)
    {
        Vector3 direction = (_target.transform.position - transform.position);
        float y = direction.y;
        direction.y = 0f;
        float x = direction.magnitude;
        float gravity = 9.8f;
        float speedSqr = _projectileSpeed * _projectileSpeed;
        float underTheSqrt = (speedSqr * speedSqr) - gravity * (gravity * x * x + 2 *y *speedSqr);
        if (underTheSqrt < 0f)
            return null;
        float root = Mathf.Sqrt(underTheSqrt);
        float highAngle = speedSqr + root;
        float lowAngle = speedSqr - root;
        if (low)
        {
            return Mathf.Atan2(lowAngle, gravity*x) * Mathf.Rad2Deg;
        }
        return Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg;
    }
}
