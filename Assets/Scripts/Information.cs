using UnityEngine;

public class Information : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;  //реализация вращения объекта за камерой
    }
}