// Extra UI Mod by MF
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ExtraUI
{
    public static class Utils
    {
        static String Name = "Owned\nSubsidiaries";

        public static Button subsButton(UnityAction toggle)
        {
            var button = WindowManager.SpawnButton();
            button.GetComponentInChildren<Text>().text = Name;
            button.onClick.AddListener(toggle);
            button.name = "MainButton";
            WindowManager.AddElementToElement(
                button.gameObject,
                WindowManager.FindElementPath("MainPanel/Holder/MainBottomPanel").gameObject,
                new Rect(0f, 0f, 100f, 50f),
                new Rect(0f, 0f, 0f, 0f));
            return button;
        }
        public static GameObject SubsPanel()
        {
            GameObject go = GameObject.Instantiate(
                WindowManager.FindElementPath("CompanyWindow").gameObject, 
                WindowManager.Instance.Canvas.transform, false);
            go.name = $"{Main._Name}SubsidiariesPanel";
            return go;
        }

        public static T InjectComponent<T, TRemove>(this GameObject gameObject, bool removeComponent = false)
            where T : Component
            where TRemove : Component
        {
            if (gameObject == null)
            {
                Debug.LogError("GameObject is null.");
                return null;
            }

            T component = gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
            if (removeComponent && typeof(T) != typeof(TRemove))
            {
                TRemove toRemove = gameObject.GetComponent<TRemove>();
                if (toRemove != null)
                {
                    GameObject.Destroy(toRemove);
                }
            }

            return component;
        }
    }

}

