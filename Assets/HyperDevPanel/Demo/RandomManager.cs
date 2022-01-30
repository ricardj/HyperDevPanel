using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [HideInInspector]
    public int hellp;

    [DevPanel("SayHello")]
    public void SayHello()
    {
        Debug.Log("Hello there");
    }

    [DevPanel("Hello")]
    public void Hello()
    {
        Debug.Log("Hello ");
    }


}