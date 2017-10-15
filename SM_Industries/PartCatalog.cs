using KSP.UI.Screens;
using RUI.Icons.Selectable;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace SMIndustries
{
    
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class SMArmoryFilter : BaseFilter
    {
        protected override string Manufacturer
        {
            get { return "SM Armory"; }
            set { }
        }
        protected override string categoryTitle
        {
            get { return "SMArmory"; }
            set { }
        }
    }

    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class SMMarineFilter : BaseFilter
    {
        protected override string Manufacturer
        {
            get { return "SM Marine"; }
            set { }
        }
        protected override string categoryTitle
        {
            get { return "SMMarine"; }
            set { }
        }
    }
   [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class LBPFilter : BaseFilter
    {
        protected override string Manufacturer
        {
            get { return "Festive Naval Engineering Co."; }
            set { }
        }
        protected override string categoryTitle
        {
            get { return "FNE"; }
            set { }
        }
    }
    public abstract class BaseFilter : MonoBehaviour
    {
        private readonly List<AvailablePart> parts = new List<AvailablePart>();
        internal string category = "Filter by function";
        internal bool filter = true;
        protected abstract string Manufacturer { get; set; }
        protected abstract string categoryTitle { get; set; }

        void Awake()
        {
            parts.Clear();
            var count = PartLoader.LoadedPartsList.Count;
            for(int i = 0; i < count; ++i)
            {
                var avPart = PartLoader.LoadedPartsList[i];
                if (!avPart.partPrefab) continue;
                if (avPart.manufacturer == Manufacturer)
                {
                    parts.Add(avPart);
                }
            }

            print(categoryTitle + "  Filter Count: " + parts.Count);
            if (parts.Count > 0)
                GameEvents.onGUIEditorToolbarReady.Add(SubCategories);
        }

        private bool EditorItemsFilter(AvailablePart avPart)
        {
            return parts.Contains(avPart);
        }

        private void SubCategories()
        {
            var icon = GenIcon(categoryTitle);
            //var filter = PartCategorizer.Instance.filters.Find(f => f.button.categoryName == category); //orignal1.3
            var filter = PartCategorizer.Instance.filters.Find(f => f.button.categorydisplayName == "#autoLOC_453547");//change for 1.3.1
            PartCategorizer.AddCustomSubcategoryFilter(filter, categoryTitle, categoryTitle, icon, EditorItemsFilter);
        }

        private Icon GenIcon(string iconName)
        {
            var normIcon = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            var normIconFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), iconName + "_normal.png");
            normIcon.LoadImage(File.ReadAllBytes(normIconFile));

            var selIcon = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            var selIconFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), iconName + "_selected.png");
            selIcon.LoadImage(File.ReadAllBytes(selIconFile));

            print("*****Adding icon for " + categoryTitle);
            var icon = new Icon(iconName + "Icon", normIcon, selIcon);
            return icon;
        }
    }

}