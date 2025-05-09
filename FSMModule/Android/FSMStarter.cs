using UnityEngine;
//Скрипт предназначен для ручного запуска fsm, если моба располагают вручную на сцене 
[RequireComponent(typeof(AndroidStateMachine))]
public class FSMStarter : MonoBehaviour
{
    private void Start() => gameObject.GetComponent<AndroidStateMachine>().StartMachine();
}
