using UnityEngine;
using System.Collections;

public class DisThis : MonoBehaviour
{
    void Awake()
    {
        Destroy(this.gameObject);
    }
}
