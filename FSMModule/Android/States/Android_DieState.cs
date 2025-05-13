using UnityEngine;

public class Android_DieState : AndroidState
{
    private float timeToDeath = 3;
    private void OnEnable()
    {
        gameObject.GetComponent<AndroidSounds>().PlayClipOneShot(AndroidClipType.Death);
        gameObject.GetComponent<MobRigidbodies>().OffKinematics();
        transform.parent = null;
        SetLayerRecursively(gameObject, 0);//we set the base layer for all descendants so that it cannot be shot at

        if (!Android.PlayerOnPickup) return;
        
        CarAttachPoints carAttachPoints = Android.CarTarget.gameObject.GetComponent<CarAttachPoints>();

        if (carAttachPoints != null)
        {
            for (int i = 0; i < carAttachPoints.AttachPoints.Length; i++)
            {
                if(carAttachPoints.AttachPoints[i] == Android)
                    carAttachPoints.AttachPoints[i] = null;
            }
        }
    }
    private void Update()
    {
        timeToDeath -= Time.deltaTime;
        if(timeToDeath < 0)
            Destroy(gameObject);
        
    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null) continue;
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
