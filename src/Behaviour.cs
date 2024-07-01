// Extra UI Mod by MF
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace ExtraUI
{
    public class Behaviour : ModBehaviour
    {
        public static Button SubsButton;
        public static GameObject SubsPanel;
        public static bool Loaded = false;

        SubsidiariesPanel SubsClass;

        public override void OnActivate() { }
        public override void OnDeactivate() { }
        void Start()
        {
            if (!isActiveAndEnabled) return;
            SceneManager.sceneLoaded += OnLoad;
        }

        private void OnLoad(Scene scene, LoadSceneMode mode)
        { 
            if (isActiveAndEnabled)
            {
                Loaded = false;
                switch (scene.name)
                {
                    case "MainMenu":
                        CleanUpScene();
                        break;
                    case "MainScene":

                        InitMainScene();
                        break;
                    default:
                        goto case "MainMenu";
                }
            }
        }

        private void InitMainScene()
        {
            GameSettings.IsDoneLoadingGame += LoadData;
            InitUI();
            Loaded = true;
        }

        private void CleanUpScene()
        {
            DataSync.Purge();
            if (SubsButton != null)
            {
                Destroy(SubsPanel);
                Destroy(SubsButton);
                SubsButton = null;
                SubsPanel = null;
            }
            GameSettings.IsDoneLoadingGame -= LoadData;
        }

        private void InitUI()
        {
            if (SubsPanel == null)
            {
                SubsPanel = Utils.SubsPanel();
                SubsClass = SubsPanel.InjectComponent<SubsidiariesPanel,CompanyWindow>(true);
            }

            if (SubsButton == null)
            {
                SubsButton = Utils.subsButton(ToggleSubsPanel);
            }
        }

        private void LoadData(object sender, EventArgs e)
        {
            DataSync.Init();
        }
        void Update()
        {
            if (!Loaded) return; 
            DataSync.UpdateList();
        }
        private void ToggleSubsPanel()
        {
            SubsClass.Window.Toggle();
        }
    }
}

