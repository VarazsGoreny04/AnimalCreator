using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AnimalCreator.WPF.View;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		string titleBarColorString = "232323";
		int titleBarColor = titleBarColorString.Length > 3 ? Convert.ToInt32(titleBarColorString, 16) : 0;

		DwmSetWindowAttribute(new WindowInteropHelper(this).EnsureHandle(), 35, ref titleBarColor, Marshal.SizeOf(titleBarColor));
	}

	[LibraryImport("dwmapi.dll")]
	private static partial int DwmSetWindowAttribute(IntPtr windowHandle, int attributeID, ref int attributeValue, int attributeSize);
}