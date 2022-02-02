using UnityEngine;

public class RandomManager : MonoBehaviour
{
    [DevPanel]
    public float someFloat { get; set; }

    [DevPanel]
    public bool someBool { get; set; }

    [DevPanel]
    public void SayHello()
    {
        Debug.Log("Hello there");
    }

}