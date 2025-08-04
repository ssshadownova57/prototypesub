using HarmonyLib;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(PrecursorMoonPoolTrigger))]
public class PrecursorMoonPoolTrigger_Patches
{
    [HarmonyPatch(nameof(PrecursorMoonPoolTrigger.Update)), HarmonyPostfix]
    private static void Update_Postfix(PrecursorMoonPoolTrigger __instance)
    {
        __instance.checkVehicles.RemoveWhere(v => !v.gameObject.activeSelf);
    }
}