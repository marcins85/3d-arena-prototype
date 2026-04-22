using UnityEngine;

public class CouritineRunner : MonoBehaviour
{
    public static CouritineRunner Instance;
    void Awake()
    {
        Instance = this;
    }
}
