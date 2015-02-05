using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace DemoKeyBoardHandler
{
	public partial class DemoKeyBoardHandlerViewController : UIViewController
	{
		private TVKeyboardHandler _tvKeyboardHandler;
		private NSObject _keyboardUp;
		private NSObject _keyboardDown;

		public DemoKeyBoardHandlerViewController (IntPtr handle) : base (handle)
		{
			_tvKeyboardHandler = new TVKeyboardHandler ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			_tvKeyboardHandler.View = this.View;
			_keyboardUp = NSNotificationCenter
				.DefaultCenter
				.AddObserver(UIKeyboard.DidShowNotification,
					_tvKeyboardHandler.KeyboardUpNotification);

			_keyboardDown = NSNotificationCenter
				.DefaultCenter
				.AddObserver(UIKeyboard.WillHideNotification,
					_tvKeyboardHandler.KeyboardDownNotification);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if (_keyboardUp != null && _keyboardDown != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardUp);
				NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardDown);
			}
		}

		#endregion


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			View.EndEditing (true);
		}
	}
}

