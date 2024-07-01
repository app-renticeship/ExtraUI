// Extra UI Mod by MF
//      Hierarchy:
//      /<SubsidiariesPanel>
//          (0)/TopPanel/etc
//          (1)/ContentPanel/CompanyList/etc
//          (2)/ResizeButton
//

using System;
using System.Linq;
using UnityEngine;

namespace ExtraUI
{
    public class SubsidiariesPanel : MonoBehaviour
    {
        public GUIWindow Window;
        public GUIListView CompanyList;
        public GameObject CompanyDetailWindow;

        [NonSerialized]
        private bool _isBound;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            Bind();
            ConfigureGUIList();
            SetupEventHandlers();
        }

        private void Init()
        {
            Window = GetComponentInChildren<GUIWindow>();
            CompanyList = transform.GetChild(1).GetComponentInChildren<GUIListView>();

            string panelName = Behaviour.SubsPanel.name;
            Window.WindowSizeID += panelName;
            Window.InitialTitle = Window.NonLocTitle = Window.TitleText.text = "Owned Subsidiaries | " + Main._Name;
            Window.name = panelName;

            CompanyList.gameObject.transform.SetParent(transform.GetChild(1));

            DevConsole.Console.Log($"{Main._Name} <{Window.name}>: Components Initialized and Data Bound");
        }
        private void Bind()
        {
            if (_isBound) return;
            _isBound = true;
            CompanyList.Items = GameSettings.Instance.MyCompany.GetSubsidiaries().Cast<object>().ToList();
            DataSync.BindSubsidiariesUpdate(CompanyList.Items);
        }

        private void ConfigureGUIList()
        {
            CompanyList["CompanyPlayer"].ToggleActive(player: false, active: GameSettings.Instance.IsNetworkMode);
        }

        private void SetupEventHandlers()
        {
            CompanyList.OnDoubleClick = delegate
            {
                Company firstSelected = CompanyList.GetFirstSelected<Company>();
                if (firstSelected != null && !firstSelected.Bankrupt)
                {
                    HUD.Instance.companyWindow.ShowCompanyDetails(firstSelected);
                }
                else
                {
                    DevConsole.Console.Log("Selected company is either null or bankrupt.");
                }
            };
        }
        private void OnDestroy()
        {
            if (_isBound)
            {
                DataSync.UnbindSubsidiariesUpdate();
            }

        }
    }
}