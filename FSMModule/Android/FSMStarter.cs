using UnityEngine;
//������ ������������ ��� ������� ������� fsm, ���� ���� ����������� ������� �� ����� 
[RequireComponent(typeof(AndroidStateMachine))]
public class FSMStarter : MonoBehaviour
{
    private void Start() => gameObject.GetComponent<AndroidStateMachine>().StartMachine();
}
