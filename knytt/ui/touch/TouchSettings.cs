using Godot;
using System;

public class TouchSettings : Node
{
	public enum VerticalPosition
	{
		Top = 0,
		BelowTop = 1,
		Center = 2,
		AboveBottom = 3,
		Bottom = 4
	}
	
	public static bool EnablePanel
	{
		get { return GDKnyttSettings.ini["TouchPanel"]["Enable"].Equals("1"); }
		set
		{
			GDKnyttSettings.ini["TouchPanel"]["Enable"] = value ? "1" : "0";
			GDKnyttSettings.SmoothScaling = GDKnyttSettings.SmoothScaling;
		}
	}
	
	public static bool SwapHands
	{
		get { return GDKnyttSettings.ini["TouchPanel"]["Swap"].Equals("1"); }
		set { GDKnyttSettings.ini["TouchPanel"]["Swap"] = value ? "1" : "0"; }
	}
	
	public static VerticalPosition Position
	{
		get { return (VerticalPosition)Enum.Parse(typeof(VerticalPosition), GDKnyttSettings.ini["TouchPanel"]["VerticalPosition"]); }
		set { GDKnyttSettings.ini["TouchPanel"]["VerticalPosition"] = value.ToString(); }
	}
	
	public static float Scale
	{
		get { return Math.Min(float.Parse(GDKnyttSettings.ini["TouchPanel"]["Scale"]), 1.5f); }
		set { GDKnyttSettings.ini["TouchPanel"]["Scale"] = value.ToString(); }
	}
	
	public static float PanelAnchor
	{
		get { return new[]{1f, 0.8f, 0.5f, 0.2f, 0f}[(int)Position]; }
	}
	
	public static float AreaAnchor
	{
		get { return new[]{0f, 0f, 0.5f, 1f, 1f}[(int)Position]; }
	}
	
	public static bool ensureSettings()
	{
		bool modified = false;
		modified |= GDKnyttSettings.ensureSetting("TouchPanel", "Enable", "0");
		modified |= GDKnyttSettings.ensureSetting("TouchPanel", "Swap", "0");
		modified |= GDKnyttSettings.ensureSetting("TouchPanel", "VerticalPosition", "Top");
		modified |= GDKnyttSettings.ensureSetting("TouchPanel", "Scale", "1");
		return modified;
	}

	public static void applyAllSettings(SceneTree tree)
	{
		var panel = tree.Root.FindNode("TouchPanel", owned: false) as TouchPanel;
		if (panel != null)
		{
			panel.Configure();
		}
	}
}
