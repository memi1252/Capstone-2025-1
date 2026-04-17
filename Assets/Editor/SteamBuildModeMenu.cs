using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

public static class SteamBuildModeMenu
{
    private const string MenuPath = "Tools/Build/Steam 없는 빌드 모드";
    private const string DisableSteamDefine = "FORCE_DISABLE_STEAMWORKS";

    [MenuItem(MenuPath)]
    private static void ToggleSteamBuildMode()
    {
        bool shouldDisableSteam = !IsSteamDisabledForStandalone();
        SetSteamDisabledForStandalone(shouldDisableSteam);

        string stateText = shouldDisableSteam ? "OFF (스팀 미사용 빌드)" : "ON (스팀 사용 빌드)";
        EditorUtility.DisplayDialog("Steam 빌드 모드", $"Steam 모드가 {stateText}로 설정되었습니다.", "확인");
    }

    [MenuItem(MenuPath, true)]
    private static bool ToggleSteamBuildModeValidate()
    {
        Menu.SetChecked(MenuPath, IsSteamDisabledForStandalone());
        return true;
    }

    private static bool IsSteamDisabledForStandalone()
    {
        return GetDefinesForStandalone().Contains(DisableSteamDefine);
    }

    private static void SetSteamDisabledForStandalone(bool disabled)
    {
        HashSet<string> defines = GetDefinesForStandalone();

        if (disabled)
        {
            defines.Add(DisableSteamDefine);
        }
        else
        {
            defines.Remove(DisableSteamDefine);
        }

        SetDefinesForStandalone(defines);
    }

    private static HashSet<string> GetDefinesForStandalone()
    {
        string rawDefines;

#if UNITY_2021_1_OR_NEWER
        rawDefines = PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(BuildTargetGroup.Standalone));
#else
        rawDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
#endif

        return new HashSet<string>(
            rawDefines.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
            StringComparer.Ordinal
        );
    }

    private static void SetDefinesForStandalone(HashSet<string> defines)
    {
        string joined = string.Join(";", defines.Where(x => !string.IsNullOrWhiteSpace(x)).OrderBy(x => x, StringComparer.Ordinal));

#if UNITY_2021_1_OR_NEWER
        PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(BuildTargetGroup.Standalone), joined);
#else
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, joined);
#endif
    }
}


