using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
