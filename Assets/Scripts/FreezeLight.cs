using UnityEngine;

public class FreezeLight : MonoBehaviour
{
    private BoxCollider col;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }
    void OnEnable()
    {
        if(PlayerController.instance == null)
        {
            return;
        }
        if(PosInLight(PlayerController.instance.transform.position))
        {
            PlayerController.instance.FreezePlayer();
        }
        else{
        }
    }
    void OnDisable()
    {
        if(PosInLight(PlayerController.instance.transform.position))
            PlayerController.instance.UnfreezePlayer();
    }
    public bool PosInLight(Vector3 pos)
    {
        return col.bounds.Contains(pos);
    }
}
