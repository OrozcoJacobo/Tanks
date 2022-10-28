using UnityEngine;

public class DestroyIfDisabled : MonoBehaviour
{
    public bool selfDestructionEnabled { get; set; } = false;

    public void OnDisable()
    {
        if(selfDestructionEnabled)
        {
            Destroy(gameObject);
        }
    }
}
