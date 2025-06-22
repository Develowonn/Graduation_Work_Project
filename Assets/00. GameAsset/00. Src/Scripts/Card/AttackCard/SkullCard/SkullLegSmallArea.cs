using UnityEngine;

public class SkullLegSmallArea : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = InGameManager.Instance.GetPlayerObject().transform.position;
    }
}
