using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;
using VRCSDK2;

public class WordTools : EditorWindow
{
    [MenuItem("VRChat Word/Button Positionrest")]
    private static void AllResetSetting()
    {
        Button resetButton = GetResetButton();
        if (resetButton != null)
        {
            VRC_SceneResetPosition[] sceneResetPositions = resetButton.transform.parent.gameObject.GetComponentsInChildren<VRC_SceneResetPosition>();
            AddResetOnClickListener(resetButton, sceneResetPositions);
        }
    }


    [MenuItem("VRChat Word/（ALL）Button Positionrest")]
    private static void ResetSetting()
    {
        Button resetButton = GetResetButton();
        if (resetButton != null)
        {


            GameObject[] findResultGameObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
            List<GameObject> gameObjectList = new List<GameObject>();
            gameObjectList.AddRange(findResultGameObjects);
            gameObjectList.Sort((a, b) => string.Compare(a.name, a.name));
            foreach (GameObject gameObj in gameObjectList)
            {
                VRC_SceneResetPosition[] sceneResetPositions = gameObj.GetComponents<VRC_SceneResetPosition>();
                AddResetOnClickListener(resetButton, sceneResetPositions);
            }
        }
    }

    private static void AddResetOnClickListener(Button resetButton, VRC_SceneResetPosition[] sceneResetPositions)
    {
        if (sceneResetPositions != null)
        {
            GameObject resetObj = resetButton.gameObject;
            foreach (VRC_SceneResetPosition sceneResetPosition in sceneResetPositions)
            {
                UnityEventTools.RemovePersistentListener(resetButton.onClick, sceneResetPosition.ResetPosition);
                UnityEventTools.AddPersistentListener(resetButton.onClick, sceneResetPosition.ResetPosition);
            }
        }
    }


    private static Button GetResetButton()
    {
        GameObject selectGameObj = Selection.activeGameObject;
        if (selectGameObj != null)
        {
            /**
             * なぜか必ず失敗する？？？？
            Button resetButton = selectGameObj.GetComponent<Button>();
            */
            MonoBehaviour[] components = selectGameObj.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour component in components)
            {
                if (component is Button)
                {
                    return (Button)component;
                }
            }
        }
        return null;
    }
}
