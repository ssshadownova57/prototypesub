using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace PrototypeSubMod.Patches;

[HarmonyPatch(typeof(CrushDamage))]
public class CrushDamage_Patches
{
    [HarmonyPatch(nameof(CrushDamage.UpdateDepthClassification)), HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> UpdateDepthClassification_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var match = new CodeMatch(i => i.opcode == OpCodes.Callvirt);

        var matcher = new CodeMatcher(instructions)
            .MatchForward(true, match)
            .Advance(1)
            .InsertAndAdvance(new CodeInstruction(OpCodes.Ldarg_0))
            .InsertAndAdvance(Transpilers.EmitDelegate(AllowDepthUpdate));
        
        return matcher.InstructionEnumeration();
    }

    public static bool AllowDepthUpdate(bool prevValue, CrushDamage crushDamage)
    {
        if (crushDamage.transform.parent == null)
        {
            return prevValue;
        }

        if (crushDamage.transform.parent.name == "ProtoVehicleHolder")
        {
            return true;
        }
        
        return prevValue;
    }
}