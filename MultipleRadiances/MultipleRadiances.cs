using Modding;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace MultipleRadiances
{
    public class MultipleRadiances : Mod,IMenuMod,IGlobalSettings<Settings>
    {
        internal static MultipleRadiances Instance;

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

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            List<IMenuMod.MenuEntry> menus = new();
            menus.Add(
                new()
                {
                    Name = "",
                    Description="",
                    Values= new string[]
                    {
                    },
                    Loader=()=>,
                    Saver=()=>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });
            menus.Add(
                new()
                {
                    Name = "",
                    Description = "",
                    Values = new string[]
                    {
                    },
                    Loader = () =>,
                    Saver = () =>
                });


            return menus;
        }

        public override string GetVersion()
        {
            return "m.x.0.1";
        }


        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;

            Log("Initialized");
        }

        public void OnLoadGlobal(Settings s)
        {
            throw new NotImplementedException();
        }

        public Settings OnSaveGlobal()
        {
            throw new NotImplementedException();
        }
    }
}