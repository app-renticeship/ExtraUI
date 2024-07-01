// Extra UI Mod by MF
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraUI
{
    public class DataSync
    {
        public static EventList<Company> Subsidiaries = new EventList<Company>();
        private static bool initialized = false;
        private static Action BindAction;
        public static void UpdateList()
        {
            if (!initialized) return;
            HashSet<Company> cacheSet;
            try { cacheSet = new HashSet<Company>(GameSettings.Instance.MyCompany.GetSubsidiaries().Cast<Company>().ToList()); } catch { return; }
            var subsidiariesSet = new HashSet<Company>(Subsidiaries);
            
            foreach (var company in cacheSet) { if (!subsidiariesSet.Contains(company)) { Subsidiaries.Add(company); } }
            Subsidiaries.RemoveAll(company => !cacheSet.Contains(company));

        }
        public static void Init()
        {
            if (initialized) return;

            Purge();
            var cacheList = GameSettings.Instance.MyCompany.GetSubsidiaries().Cast<Company>().ToList();
            foreach (var company in cacheList) { Subsidiaries.Add(company); }
            initialized = true;

        }
        public static void Purge()
        {
            initialized = false;
            UnbindSubsidiariesUpdate();
            Subsidiaries.Clear();
        }
        public static void BindSubsidiariesUpdate(EventList<object> target)
        {
            UnbindSubsidiariesUpdate();
            BindAction = () => { Subsidiaries.Update(target); };
            Subsidiaries.OnChange += BindAction;
        }
        public static void UnbindSubsidiariesUpdate()
        {
            if (BindAction != null)
            {
                Subsidiaries.OnChange -= BindAction;
                BindAction = null;
            }
        }
    }
}
