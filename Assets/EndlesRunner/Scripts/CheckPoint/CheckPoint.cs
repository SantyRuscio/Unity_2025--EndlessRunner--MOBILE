using UnityEngine;

public class CheckPoint : Item
{
    public override void Execute()
    {
        Debug.Log("CheckPointGuarda");
        GameManager.instance.SaveMethod();
    }
}

