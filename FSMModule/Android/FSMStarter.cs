using UnityEngine;
//The script is designed to manually launch fsm if the mob is placed manually on the scene
[RequireComponent(typeof(AndroidStateMachine))]
public class FSMStarter : MonoBehaviour
{
    private void Start() => gameObject.GetComponent<AndroidStateMachine>().StartMachine();
}
