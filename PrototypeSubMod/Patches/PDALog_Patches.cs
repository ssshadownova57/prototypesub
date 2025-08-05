using HarmonyLib;
using Nautilus.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(PDALog))]
internal class PDALog_Patches
{
    public static List<(string assetName, string key)> entries = new();
    public static List<(string assetName, string key)> orionEntries = new();
    
    [HarmonyPatch(nameof(PDALog.Initialize)), HarmonyPostfix]
    private static void Initialize_Postfix(PDAData pdaData)
    {
        AddEntries(entries, pdaData.log[0].icon);
        AddEntries(orionEntries, Plugin.AssetBundle.LoadAsset<Sprite>("ProtoPDALogo"));
    }

    private static void AddEntries(List<(string assetName, string key)> entriesToRegister, Sprite sprite)
    {
        foreach (var item in entriesToRegister)
        {
            var fmodAsset = AudioUtils.GetFmodAsset(item.assetName);
            fmodAsset.id = fmodAsset.path;

            PDALog.EntryData ency = new()
            {
                key = item.key,
                type = PDALog.EntryType.Default,
                icon = sprite,
                sound = fmodAsset,
                doNotAutoPlay = false
            };

            if (!PDALog.mapping.ContainsKey(item.key))
            {
                PDALog.mapping.Add(item.key, ency);
            }
        }
    }
}
