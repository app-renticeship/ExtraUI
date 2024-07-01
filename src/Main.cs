// Extra UI Mod by MF
using UnityEngine;
using UnityEngine.UI;

namespace ExtraUI
{

    public class Main : ModMeta
    {

        public static string _Name = "ExtraUI";
        public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
        {
            Text text = WindowManager.SpawnLabel();
            text.text = "\nAdditional quality of life UIs! (by MF)\n - Owned Subsidiaries\n - More to come!";
            WindowManager.AddElementToElement(
                text.gameObject,
                parent.gameObject,
                new Rect(0f, 0f, 400f, 128f),
                new Rect(0f, 0f, 0f, 0f));
        }


        public override string Name => _Name;
    }

}