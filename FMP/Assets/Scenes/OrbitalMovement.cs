using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour {

    Vector3 gravityDirection;
    Collider thisCollider;
    Rigidbody rb;

    private void Start() {
        thisCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        Vector3 closestPoint = new Vector3();
        Collider closestCollider = new Collider();
        float closestDistance = Mathf.Infinity;

        foreach(Collider collider in colliders) {
            if (collider != thisCollider) {
                Vector3 tempClosest = collider.ClosestPoint(transform.position);
                float tempDistance = Vector3.Distance(transform.position, tempClosest);
                if (tempDistance < closestDistance) {
                    closestPoint = tempClosest;
                    closestDistance = tempDistance;
                    closestCollider = collider;
                }
            }
        }
        gravityDirection = -(transform.position - closestPoint).normalized;
        Ray ray = new Ray(transform.position, gravityDirection);
        RaycastHit hit;
        closestCollider.Raycast(ray, out hit, 10f);
        Debug.DrawLine(transform.position, hit.point, Color.blue);
        transform.LookAt(transform.position * 2 - closestPoint);

        rb.AddForce(gravityDirection * (rb.mass * rb.mass));
    }
}
