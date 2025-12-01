using UnityEngine;

public class FreezeLight : MonoBehaviour
{
    private Light light;

    void Awake()
    {
        light = GetComponent<Light>();
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
    }
    void OnDisable()
    {
        PlayerController.instance.UnfreezePlayer();
    }
    public bool PosInLight(Vector3 pos)
    {
        if(Vector3.Distance(pos, light.transform.position) > light.range)
        {
            return false; 
        }
        else{
            Vector3 dirToPlayer = (pos - light.transform.position).normalized;
            if (Vector3.Angle(light.transform.forward, dirToPlayer) < light.spotAngle / 2)
            {
                return true;
            }
        }
        return false;
    }
}
