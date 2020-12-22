using UnityEngine;

public class RotateMisc : MonoBehaviour
{
    public float speedRotation = 2f;
    Rigidbody misc;

    private void Start()
    {
        misc = GetComponent<Rigidbody>();  //нахожу компонент на объекте

        misc.angularVelocity = Random.insideUnitSphere * speedRotation;  //задаю случайное вращение этому объекту
    }
}