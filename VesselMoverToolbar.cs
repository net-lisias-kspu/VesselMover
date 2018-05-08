using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KSP.UI.Screens;

namespace VesselMover
{
  [KSPAddon(KSPAddon.Startup.Flight, false)]
  public class VesselMoverToolbar : MonoBehaviour
  {
    public static bool hasAddedButton = false;
    public static bool toolbarGuiEnabled = false;
    public static bool addCrewMembers = true;
    public static bool selectCrewMembers = false;
    public static bool ShowUI = true;
    private static bool latch;
    internal static GUIStyle ButtonToggledStyle;
    internal static List<ProtoCrewMember> SelectedCrewMembers = new List<ProtoCrewMember>();

    Rect toolbarRect;
    float toolbarWidth = 280;
    float toolbarHeight = 0;
    float toolbarMargin = 6;
    float toolbarLineHeight = 20;
    float contentWidth;
    Vector2 toolbarPosition;
    Rect svRectScreenSpace;
    Rect svCrewScreenSpace;
    bool showMoveHelp = false;
    float helpHeight;

    private Rect _crewSelectRect;
    private float _crewSelectWidth = 300;
    private float _crewSelectHeight = 300;
    private static Vector2 _crewSelectPosition;
    private static Vector2 _displayViewerPosition = Vector2.zero;

    void Start()
    {
      GameEvents.onHideUI.Add(OnHideUI);
      GameEvents.onShowUI.Add(OnShowUI);
      toolbarPosition = new Vector2(Screen.width - toolbarWidth - 80, 39);
      toolbarRect = new Rect(toolbarPosition.x, toolbarPosition.y, toolbarWidth, toolbarHeight);
      _crewSelectPosition = new Vector2(Screen.width/2 - _crewSelectWidth/2, Screen.height/2 - _crewSelectHeight /2);
      _crewSelectRect = new Rect(_crewSelectPosition.x, _crewSelectPosition.y, _crewSelectWidth, _crewSelectHeight);
      contentWidth = toolbarWidth - (2 * toolbarMargin);

      SelectedCrewMembers = new List<ProtoCrewMember>();

      AddToolbarButton();
    }

    void OnDestroy()
    {
      GameEvents.onHideUI.Remove(OnHideUI);
      GameEvents.onShowUI.Remove(OnShowUI);
    }

    void OnGUI()
    {
      ButtonToggledStyle = new GUIStyle(GUI.skin.button);
      ButtonToggledStyle.normal.background = ButtonToggledStyle.onActive.background;

      if (ShowUI && addCrewMembers && VesselSpawn.IsSelectingCrew)
      {
        _crewSelectRect = GUILayout.Window(401239, _crewSelectRect, CrewSelectionWindow, "Select Crew", HighLogic.Skin.window);
        if (!latch) Debug.Log(_crewSelectRect.ToString());
        latch = true;
      }

      if (!ShowUI || !toolbarGuiEnabled || !VesselMove.instance || !VesselSpawn.instance ||
          VesselSpawn.instance.openingCraftBrowser || VesselSpawn.IsSelectingCrew) return;
      toolbarRect = GUI.Window(401240, toolbarRect, ToolbarWindow, "Vessel Mover", HighLogic.Skin.window);

      if (!VesselMove.instance.isMovingVessel)
      {
        if (!MouseIsInRect(svRectScreenSpace)) return;
        Vector2 mousePos = MouseGUIPos();
        Rect warningRect = new Rect(mousePos.x + 5, mousePos.y + 20, 200, 60);
        GUI.Label(warningRect, "WARNING: Experimental. Launch clamps may be broken.", HighLogic.Skin.box);
      }
      else if (showMoveHelp)
      {
        GUI.Window(401241, new Rect(toolbarRect.x, toolbarRect.y + toolbarRect.height, toolbarRect.width, helpHeight), MoveHelp, "Controls", HighLogic.Skin.window);
      }

    }

    void ToolbarWindow(int windowID)
    {
      float line = 0;
      line += 1.25f;

      if (FlightGlobals.ActiveVessel && (FlightGlobals.ActiveVessel.LandedOrSplashed || VesselMove.instance.isMovingVessel))
      {
        if (!VesselMove.instance.isMovingVessel)
        {
          if (GUI.Button(LineRect(ref line, 1.5f), "Move Vessel", HighLogic.Skin.button))
          {
            VesselMove.instance.StartMove(FlightGlobals.ActiveVessel, true);
          }
          line += 0.2f;

          Rect spawnVesselRect = LineRect(ref line);
          svRectScreenSpace = new Rect(spawnVesselRect);
          svRectScreenSpace.x += toolbarRect.x;
          svRectScreenSpace.y += toolbarRect.y;

          if (GUI.Button(spawnVesselRect, "Spawn Vessel", HighLogic.Skin.button))
          {
            VesselSpawn.instance.StartVesselSpawn();
          }

          line += .75f;
          Rect crewRect1 = LineRect(ref line);
          Rect crewRect2 = new Rect(crewRect1.x + crewRect1.width/2 + 5f, crewRect1.y, crewRect1.width / 2, crewRect1.height);
          crewRect1.width = crewRect1.width / 2;
          svCrewScreenSpace = new Rect(crewRect1);
          svCrewScreenSpace.x += toolbarRect.x;
          svCrewScreenSpace.y += toolbarRect.y;
          addCrewMembers = GUI.Toggle(crewRect1, addCrewMembers, "Spawn Crew");
          if (!addCrewMembers) GUI.enabled = false;
          selectCrewMembers = GUI.Toggle(crewRect2, selectCrewMembers, "Choose Crew");
          GUI.enabled = true;
          showMoveHelp = false;
        }
        else
        {
          if (GUI.Button(LineRect(ref line, 2), "Place Vessel", HighLogic.Skin.button))
          {
            VesselMove.instance.EndMove();
          }

          line += 0.3f;

          if (GUI.Button(LineRect(ref line, 2), "Drop Vessel", HighLogic.Skin.button))
          {
            VesselMove.instance.DropMove();
          }

          line += 0.3f;

          if (GUI.Button(LineRect(ref line), "Help", HighLogic.Skin.button))
          {
            showMoveHelp = !showMoveHelp;
          }
        }
      }
      else
      {
        GUIStyle centerLabelStyle = new GUIStyle(HighLogic.Skin.label);
        centerLabelStyle.alignment = TextAnchor.UpperCenter;
        GUI.Label(LineRect(ref line), "You need to be landed to use this!", centerLabelStyle);
      }

      toolbarRect.height = (line * toolbarLineHeight) + (toolbarMargin * 2);
      GUI.DragWindow(new Rect(0, 0, Screen.width, 30));
      VMUtils.RepositionWindow(ref toolbarRect);
    }

    void CrewSelectionWindow(int windowID)
    {
      KerbalRoster kerbalRoster = HighLogic.CurrentGame.CrewRoster;
      GUILayout.BeginVertical();
      _displayViewerPosition = GUILayout.BeginScrollView(_displayViewerPosition, GUI.skin.box, GUILayout.Height(250), GUILayout.Width(280));
      IEnumerator<ProtoCrewMember> kerbals = kerbalRoster.Kerbals(ProtoCrewMember.RosterStatus.Available).GetEnumerator();
      while (kerbals.MoveNext())
      {
        ProtoCrewMember crewMember = kerbals.Current;
        if (crewMember == null) continue;
        bool selected = SelectedCrewMembers.Contains(crewMember);
        GUIStyle buttonStyle = selected ? ButtonToggledStyle : HighLogic.Skin.button;
        selected = GUILayout.Toggle(selected,  $"{crewMember.name}, {crewMember.gender}, {crewMember.trait}", buttonStyle);
        if (selected && !SelectedCrewMembers.Contains(crewMember))
        {
          SelectedCrewMembers.Clear();
          SelectedCrewMembers.Add(crewMember);
        }
        else if (!selected && SelectedCrewMembers.Contains(crewMember))
        {
          SelectedCrewMembers.Clear();
        }
      }
      kerbals.Dispose();
      GUILayout.EndScrollView();
      GUILayout.Space(20);
      if (GUILayout.Button("Select", HighLogic.Skin.button))
      {
        VesselSpawn.SelectedCrewData = SelectedCrewMembers;
        VesselSpawn.IsSelectingCrew = false;
        VesselSpawn.IsCrewSelected = true;
      }
      GUILayout.EndVertical();
    }

    Rect LineRect(ref float currentLine, float heightFactor = 1)
    {
      Rect rect = new Rect(toolbarMargin, toolbarMargin + (currentLine * toolbarLineHeight), contentWidth, toolbarLineHeight * heightFactor);
      currentLine += heightFactor + 0.1f;
      return rect;
    }

    void MoveHelp(int windowID)
    {
      float line = 0;
      line += 1.25f;
      LineLabel("Movement: " + GameSettings.PITCH_DOWN.primary.ToString() + " " +
        GameSettings.PITCH_UP.primary.ToString() + " " +
        GameSettings.YAW_LEFT.primary.ToString() + " " +
        GameSettings.YAW_RIGHT.primary.ToString(), ref line);
      LineLabel("Roll: " + GameSettings.ROLL_LEFT.primary.ToString() + " " +
        GameSettings.ROLL_RIGHT.primary.ToString(), ref line);
      LineLabel("Pitch: " + GameSettings.TRANSLATE_DOWN.primary.ToString() + " " +
        GameSettings.TRANSLATE_UP.primary.ToString(), ref line);
      LineLabel("Yaw: " + GameSettings.TRANSLATE_LEFT.primary.ToString() + " " +
        GameSettings.TRANSLATE_RIGHT.primary.ToString(), ref line);
      LineLabel("Auto rotate rocket: " + GameSettings.TRANSLATE_BACK.primary.ToString(), ref line);
      LineLabel("Auto rotate plane: " + GameSettings.TRANSLATE_FWD.primary.ToString(), ref line);
      LineLabel("Change movement speed: Tab", ref line);
      LineLabel("Adjust Altitude: " + GameSettings.THROTTLE_UP.primary.ToString() + " " +
        GameSettings.THROTTLE_DOWN.primary.ToString(), ref line);

      helpHeight = (line * toolbarLineHeight) + (toolbarMargin * 2);
    }

    void LineLabel(string label, ref float line)
    {
      GUI.Label(LineRect(ref line), label, HighLogic.Skin.label);
    }

    void AddToolbarButton()
    {
      if (HighLogic.LoadedSceneIsFlight)
      {
        if (!hasAddedButton)
        {
          Texture buttonTexture = GameDatabase.Instance.GetTexture("VesselMover/Textures/icon", false);
          ApplicationLauncher.Instance.AddModApplication(ShowToolbarGUI, HideToolbarGUI, Dummy, Dummy, Dummy, Dummy, ApplicationLauncher.AppScenes.FLIGHT, buttonTexture);
          hasAddedButton = true;
        }
      }
    }

    public void ShowToolbarGUI()
    {
      VesselMoverToolbar.toolbarGuiEnabled = true;
    }

    public void HideToolbarGUI()
    {
      VesselMoverToolbar.toolbarGuiEnabled = false;
    }

    void Dummy()
    { }

    public static bool MouseIsInRect(Rect rect)
    {
      return rect.Contains(MouseGUIPos());
    }

    public static Vector2 MouseGUIPos()
    {
      return new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 0);
    }

    public void OnHideUI()
    {
      ShowUI = false;
    }
    public void OnShowUI()
    {
      ShowUI = true;
    }
  }
}

