using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

using UnityPlayer;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769
namespace Template
{
	public sealed partial class AppSettings : SettingsFlyout
	{
		bool muteToggle = false;
		
		public AppSettings()
		{
			this.InitializeComponent();
		}
		
		protected override void OnGotFocus(RoutedEventArgs e)
		{
			// The 'vol' PlayerPref prevents an AudioListener volume of 0.5 (for example)
			// from being set to 1 when the app is unmuted.
			muteToggle = UnityEngine.PlayerPrefs.GetInt("vol",1) == 0;
			mute.IsOn = muteToggle;
			mute.Toggled += mute_Toggled;
		}

		private void mute_Toggled(object sender, RoutedEventArgs e)
		{
			muteToggle = !muteToggle;
			AppCallbacks.Instance.InvokeOnAppThread(() =>
			{
				// Set the volume to 0, else set it to whatever the saved volume level is.
				UnityEngine.AudioListener.volume = (muteToggle) ? 0f : UnityEngine.PlayerPrefs.GetInt("vol",1);
			}, false);
		}

	}
}
