using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleFollower : MonoBehaviour
{
    [field: SerializeField]
    public Transform MarbleTransform { get; private set; }

    [field: SerializeField]
    public float Distance { get; private set; } = 15;
    [field: SerializeField]
    public float Height { get; private set; } = 10;
    
    protected void Update() {
        Vector3 position = transform.position;
        position.z = MarbleTransform.position.z - Distance;
        position.x = MarbleTransform.position.x;
        position.y = MarbleTransform.position.y + Height;
        transform.position = position;
    }

}
