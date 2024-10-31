using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelManager : MonoBehaviour
{
    public void LoadEndScene(string key)
    {
       Addressables.LoadSceneAsync(key);
    }
}
