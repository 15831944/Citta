﻿using System;
using System.Drawing;
using System.Windows.Forms;


namespace C2.Controls.Aero
{
    /// <summary>
    /// Windows Glass Form
    /// Inherit from this form to be able to enable glass on Windows Form
    /// </summary>
    public class GlassForm : Form
    {
        #region properties

        /// <summary>
        /// Get determines if AeroGlass is enabled on the desktop. Set enables/disables AreoGlass on the desktop.
        /// </summary>
        public static bool AeroGlassCompositionEnabled
        {
            get
            {
                if (System.Environment.OSVersion.Version <= new Version(6, 0, 0, 0))
                {
                    return false;
                }

                return DesktopWindowManagerNativeMethods.DwmIsCompositionEnabled();
            }

            set
            {
                if (System.Environment.OSVersion.Version <= new Version(6, 0, 0, 0))
                {
                    return;
                }
                DesktopWindowManagerNativeMethods.DwmEnableComposition(
                    value ? CompositionEnable.Enable : CompositionEnable.Disable);
            }
        }

        #endregion

        #region events

        /// <summary>
        /// Fires when the availability of Glass effect changes.
        /// </summary>
        public event EventHandler<AeroGlassCompositionChangedEventArgs> AeroGlassCompositionChanged;

        #endregion

        #region operations

        /// <summary>
        /// Makes the background of current window transparent
        /// </summary>
        public void SetAeroGlassTransparency()
        {
            this.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Excludes a Control from the AeroGlass frame.
        /// </summary>
        /// <param name="control">The control to exclude.</param>
        /// <remarks>Many non-WPF rendered controls (i.e., the ExplorerBrowser control) will not 
        /// render properly on top of an AeroGlass frame. </remarks>
        public void ExcludeControlFromAeroGlass(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            if (AeroGlassCompositionEnabled)
            {
                Rectangle clientScreen = this.RectangleToScreen(this.ClientRectangle);
                Rectangle controlRect = control.ClientRectangle;
                //controlRect.Inflate(-1, -1);
                controlRect.Y++;
                controlRect.Height--;
                Rectangle controlScreen = control.RectangleToScreen(controlRect);

                Margins margins = new Margins();
                margins.LeftWidth = controlScreen.Left - clientScreen.Left;
                margins.RightWidth = clientScreen.Right - controlScreen.Right;
                margins.TopHeight = controlScreen.Top - clientScreen.Top;
                margins.BottomHeight = clientScreen.Bottom - controlScreen.Bottom;

                // Extend the Frame into client area
                DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        public void ExcludeRectangleFromAeroGlass(Rectangle rectangle)
        {
            if (AeroGlassCompositionEnabled)
            {
                Margins margins = new Margins();
                margins.LeftWidth = rectangle.X;
                margins.TopHeight = rectangle.Y;
                margins.RightWidth = ClientSize.Width - rectangle.Right;
                margins.BottomHeight = Height - rectangle.Bottom;

                // Extend the Frame into client area
                DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(Handle, ref margins);
            }
        }

        /// <summary>
        /// Resets the AeroGlass exclusion area.
        /// </summary>
        public void ResetAeroGlass()
        {
            if (AeroGlassCompositionEnabled && this.Handle != IntPtr.Zero)
            {
                Margins margins = new Margins(true);
                DesktopWindowManagerNativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }

        #endregion

        #region implementation
        /// <summary>
        /// Catches the DWM messages to this window and fires the appropriate event.
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == DWMMessages.WM_DWMCOMPOSITIONCHANGED
                || m.Msg == DWMMessages.WM_DWMNCRENDERINGCHANGED)
            {
                OnAeroGlassCompositionChanged();
            }

            base.WndProc(ref m);
        }

        protected virtual void OnAeroGlassCompositionChanged()
        {
            if (AeroGlassCompositionChanged != null)
            {
                AeroGlassCompositionChanged.Invoke(this, new AeroGlassCompositionChangedEventArgs(AeroGlassCompositionEnabled));
            }
        }

        /// <summary>
        /// Initializes the Form for AeroGlass
        /// </summary>
        /// <param name="e">The arguments for this event</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //ResetAeroGlass();
        }

        /// <summary>
        /// Overide OnPaint to paint the background as black.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode == false)
            {
                //if (AeroGlassCompositionEnabled && e != null)
                //{
                //    // Paint the all the regions black to enable glass
                //    e.Graphics.FillRectangle(Brushes.Black, this.ClientRectangle);
                //}
            }
        }

        #endregion
    }
}
