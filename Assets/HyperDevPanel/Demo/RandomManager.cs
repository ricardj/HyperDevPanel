using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [HideInInspector]
    public int hellp;

    [DevPanel]
    public void SayHello()
    {
        Debug.Log("Hello there");
    }

    [DevPanel]
    public void Hello()
    {
        Debug.Log("Hello ");
    }


}