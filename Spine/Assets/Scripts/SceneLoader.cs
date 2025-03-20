// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.SceneManagement;

    /// <summary>
    /// Provides utilities for switching between scenes.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Request that the MRTK SceneSystem load a scene of a given name.
        /// </summary>
 //       public void LoadScene(string sceneName) => CoreServices.SceneSystem.LoadContent(sceneName, LoadSceneMode.Single);
    public void Selectscene()
    {
        SceneManager.LoadScene("AzureAnchors", LoadSceneMode.Single);
    }
}

