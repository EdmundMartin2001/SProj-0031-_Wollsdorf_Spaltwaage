using System;
using System.Drawing;

namespace Wollsdorf_Spaltwaage.Allgemein.Button
{
    public sealed class GradientFill
    {
        public static bool Fill(Graphics gr, Rectangle rc, Color startColor, Color endColor, FillDirection fillDir)
        {
            Win32.TRIVERTEX[] pVertex = new Win32.TRIVERTEX[] { new Win32.TRIVERTEX(rc.X, rc.Y, startColor), new Win32.TRIVERTEX(rc.Right, rc.Bottom, endColor) };
            Win32.GRADIENT_RECT[] pMesh = new Win32.GRADIENT_RECT[] { new Win32.GRADIENT_RECT(0, 1) };
            IntPtr hdc = gr.GetHdc();
            bool flag = Win32.GradientFill(hdc, pVertex, (uint) pVertex.Length, pMesh, (uint) pMesh.Length, (uint) fillDir);
            gr.ReleaseHdc(hdc);
            return flag;
        }

        public enum FillDirection
        {
            LeftToRight,
            TopToBottom
        }
    }
}

