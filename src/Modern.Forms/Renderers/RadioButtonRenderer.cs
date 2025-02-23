﻿using Modern.Forms.Layout;
using SkiaSharp;

namespace Modern.Forms.Renderers
{
    /// <summary>
    /// Represents a class that can render a RadioButton.
    /// </summary>
    public class RadioButtonRenderer : Renderer<RadioButton>, IRenderGlyph, IRenderTextAndImage
    {
        /// <inheritdoc/>
        public int GlyphSize { get; } = 24;

        /// <inheritdoc/>
        public int GlyphTextPadding { get; } = 0;

        /// <inheritdoc/>
        public int ImageTextMargin { get; } = 4;

        /// <inheritdoc/>
        protected override void Render (RadioButton control, PaintEventArgs e)
        {
            var layout = TextImageLayoutEngine.Layout (control);

            ControlPaint.DrawRadioButton (e, layout.GlyphBounds.GetCenter (), control.Checked ? CheckState.Checked : CheckState.Unchecked, !control.Enabled);

            // Draw the image
            if ((control as IHaveTextAndImageAlign).GetImage () is SKBitmap image)
                e.Canvas.DrawBitmap (image, layout.ImageBounds, !control.Enabled);

            // Draw the focus rectangle
            if (control.Selected && control.ShowFocusCues)
                e.Canvas.DrawFocusRectangle (layout.Focus, 0);

            // Draw the text
            if (control.Text.HasValue ())
                e.Canvas.DrawText (control.Text, layout.TextBounds, control, control.TextAlign, maxLines: 1, ellipsis: control.AutoEllipsis);
        }
    }
}
