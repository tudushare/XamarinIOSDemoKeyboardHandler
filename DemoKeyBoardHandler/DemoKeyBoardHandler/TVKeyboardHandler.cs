using System;
using UIKit;
using Foundation;
using System.Drawing;
using CoreGraphics;

namespace DemoKeyBoardHandler
{
	public class TVKeyboardHandler
	{
		private UIView _activeView; // Controller that activated the keyboard
		public UIView View { get; set; } // The UIView for the keyboard handler

		private nfloat _scrollAmount = 0; // amount to scroll 
		private nfloat _scrolledAmount = 0; // how much we've
		// scrolled already
		private nfloat _bottom = 0.0f; // bottom point
		private const float OFFSET = 10.0f; // extra offset 

		public void KeyboardUpNotification(NSNotification notification)
		{
			// get the keyboard size
			var val = (NSValue)notification.UserInfo.ValueForKey (UIKeyboard.FrameBeginUserInfoKey);
			RectangleF keyboardFrame = val.RectangleFValue;

			// Find what opened the keyboard
			foreach (UIView view in this.View.Subviews)
			{
				if (view.IsFirstResponder)
					_activeView = view;
			}
			if (_activeView != null) {
				// Determine if we need to scroll up or down.
				// Bottom of the controller = initial position + height + offset 
				_bottom = (_activeView.Frame.Y + _activeView.Frame.Height + OFFSET);
				// Calculate how far we need to scroll
				_scrollAmount = (keyboardFrame.Height - (View.Frame.Size.Height - _bottom));

				//Move view up
				if (_scrollAmount > 0)
				{
					//Subtract the scrolledamount. We can’t do this subtraction above because the calculations won’t work correctly.
					_bottom -= _scrolledAmount;
					_scrollAmount = (keyboardFrame.Height - (View.Frame.Size.Height - _bottom));
					_scrolledAmount += _scrollAmount;
					ScrollTheView(false);
				}
				//Reset the view.
				else
				{
					ScrollTheView(true);
				}
			}
		}
		public void KeyboardDownNotification(NSNotification
			notification)
		{
			ScrollTheView(true);
		}

		private void ScrollTheView(bool reset)
		{
			// scroll the view up or down
			UIView.BeginAnimations(string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration(0.3);
			CGRect frame = View.Frame;
			if (reset)
			{
				frame.Y = frame.Y + _scrolledAmount;
				_scrollAmount = 0;
				_scrolledAmount = 0;
			}
			else
			{
				frame.Y -= _scrollAmount;
			}
			View.Frame = frame;
			UIView.CommitAnimations();
		}
	}
}

