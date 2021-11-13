using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction Jumped;
    public event UnityAction StartedGainPower;
    public event UnityAction StartedMakeLanding;
    
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartedGainPower?.Invoke();
        
        if (Input.GetKeyUp(KeyCode.Space))
            Jumped?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.S))
            StartedMakeLanding?.Invoke();
    }
}
