using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class RefreshConfig : MonoBehaviour
{
    public void Click() => StartCoroutine(RefreshData());   
    private IEnumerator RefreshData() 
    {
        bool isMainJsonExctracted = false;
        yield return StartCoroutine(FirebaseParser.ExtractJsonFile(result => isMainJsonExctracted = result, forceUpdate:true));
        if (isMainJsonExctracted)
        {
            yield return StartCoroutine(FirebaseFileSplitter.Split(useStatRetake: false));
        }
        yield return new WaitForSeconds(2);
        MainMenu.Load();
    }
}
