using Modding;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;
using ManyRadiances;
using Galaxy.Api;
using System.Runtime.InteropServices.WindowsRuntime;
using Satchel.BetterMenus;

namespace MultipleRadiances
{
    public class MultipleRadiances : Mod,IMenuMod,IGlobalSettings<Settings>,ITogglableMod
    {
        internal static MultipleRadiances Instance;

        public Settings set=new();

        public bool origin=false;

        public List<GameObject> rads = new List<GameObject>();


        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        public MultipleRadiances() : base("MultipleRadiances")
        {
            Instance = this;
        }

        public bool ToggleButtonInsideMenu => true;


        public override string GetVersion()
        {
            return "m.x.0.1";
        }


        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");
            Instance = this;
            origin = false;
            ModHooks.LanguageGetHook += LangChange;
            On.PlayMakerFSM.OnEnable += MultiRad;
            ManyRadiances.ManyRadiances.Instance.Multirad = true;
            Log("Initialized");
        }

        private string LangChange(string key, string sheetTitle, string orig)
        {
            switch (key)
            {
                case "ABSOLUTE_RADIANCE_SUPER": return "多重";
                case "ABSOLUTE_RADIANCE_MAIN": return"无上辐光";
                case "GG_S_RADIANCE": return"拥有着多重分身之神";
                case "GODSEEKER_RADIANCE_STATUE":return "ok。"; 
                default: return orig;
            }
        }

        private void MultiRad(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
                if (self.FsmName == "Control" && self.gameObject.name == "Absolute Radiance")
                {
                    Log("beginmulti");
                    if (CountSet(set) != 0)
                    {
                        rads.Clear();
                        rads.Add(self.gameObject);
                        int count = CountSet(set);
                        while (count > 1)
                        {
                            Log(count);
                            rads.Add(GameObject.Instantiate(rads[0]));
                            count--;
                        }
                        self.gameObject.AddComponent<RadiancesControler>();
                        Log("start control");
                    }
                }


            orig(self);
        }

        public void OnLoadGlobal(Settings s)
        {
            set = s;
        }

        public Settings OnSaveGlobal()
        {
            return set;
        }
        public int CountSet(Settings s)
        {
            return(s.any1+s.any2+s.anyPrime+s.atomic+s.dumb+s.forgottenLight+s.ironHead+s.ori+s.superNova+s.ultimatum);
        }


        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            IMenuMod.MenuEntry toggle =(IMenuMod.MenuEntry)toggleButtonEntry;
            List<IMenuMod.MenuEntry> menu = new();
            string[] num = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            menu.Add(toggle);
            menu.Add(
                new()
                {
                    Name = "AnyRadiance1.0",
                    Description = "any辐光1.0",
                    Values = num,
                    Loader = () => set.any1,
                    Saver = i => set.any1 = i
                }); ;
            menu.Add(
                new()
                {
                    Name = "AnyRadiance2.0",
                    Description = "any辐光2.0",
                    Values = num,
                    Loader = () => set.any2,
                    Saver = i => set.any2 = i
                });
            menu.Add(
                new()
                {
                    Name = "Absolute Radiance",
                    Description = "原版辐光",
                    Values = num,
                    Loader = () =>set.ori ,
                    Saver = i =>set.ori = i
                });
            menu.Add(
                new()
                {
                    Name = "UltimatumRadiance",
                    Description = "终焉辐光",
                    Values =num,
                    Loader = () =>set.ultimatum,
                    Saver = i => set.ultimatum = i
                });
            menu.Add(
                new()
                {
                    Name = "DumbRadiance",
                    Description = "愚妄辐光",
                    Values = num,
                    Loader = () => set.dumb,
                    Saver = i => set.dumb = i
                });
            menu.Add(
                new()
                {
                    Name = "SupernovaRadiance",
                    Description = "超新星辐光",
                    Values = num,
                    Loader = () => set.superNova,
                    Saver = i =>set.superNova = i
                });
            menu.Add(
                new()
                {
                    Name = "AnyRadiancePrime",
                    Description = "全盛any辐光",
                    Values = num,
                    Loader = () => set.anyPrime,
                    Saver = i => set.anyPrime = i
                });
            menu.Add(
                new()
                {
                    Name = "AtomicRadiance",
                    Description = "原子辐光",
                    Values = num,
                    Loader = () => set.atomic,
                    Saver = i => set.atomic = i
                });
            menu.Add(
                new()
                {
                    Name = "IronHeadRadiance",
                    Description = "铁头辐光",
                    Values = num,
                    Loader = () => set.ironHead,
                    Saver = i => set.ironHead = i
                });
            menu.Add(
                new()
                {
                    Name = "ForgottenLight",
                    Description = "遗忘之光",
                    Values = num,
                    Loader = () => set.forgottenLight,
                    Saver = i =>set.forgottenLight = i
                });



            return menu;
        }

        public void Unload()
        {
            On.PlayMakerFSM.OnEnable -= MultiRad;
            ModHooks.LanguageGetHook -= LangChange;
            ManyRadiances.ManyRadiances.Instance.Multirad = false;
        }

    }
}