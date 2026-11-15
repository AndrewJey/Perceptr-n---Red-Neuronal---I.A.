using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LegacyDiagrams
{
    public abstract class DiagramShape
    {
        public string Name { get; set; }
        internal abstract void Draw(Graphics graphics);
    }

    public sealed class DiagramLine : DiagramShape
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        internal override void Draw(Graphics graphics) { graphics.DrawLine(Pens.Black, X1, Y1, X2, Y2); }
    }

    public sealed class DiagramOval : DiagramShape
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        internal override void Draw(Graphics graphics) { graphics.DrawEllipse(Pens.Black, new Rectangle(Location, Size)); }
    }

    public sealed class DiagramCollection : List<DiagramShape> { }

    // Se dibuja en el fondo del formulario: no coloca una ventana opaca ni
    // intercepta los clics sobre etiquetas, botones o la tabla del ejercicio.
    public sealed class DiagramCanvas : Control
    {
        private Control paintedParent;
        private readonly DiagramCollection shapes = new DiagramCollection();
        public DiagramCollection Shapes { get { return shapes; } }

        public DiagramCanvas() { Visible = false; TabStop = false; }

        protected override void SetVisibleCore(bool value) { base.SetVisibleCore(false); }

        protected override void OnParentChanged(EventArgs e)
        {
            if (paintedParent != null) paintedParent.Paint -= DrawDiagram;
            base.OnParentChanged(e);
            paintedParent = Parent;
            if (paintedParent != null) { paintedParent.Paint += DrawDiagram; paintedParent.Invalidate(); }
        }

        private void DrawDiagram(object sender, PaintEventArgs e)
        {
            GraphicsState state = e.Graphics.Save();
            e.Graphics.TranslateTransform(Left, Top);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (DiagramShape shape in shapes) shape.Draw(e.Graphics);
            e.Graphics.Restore(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && paintedParent != null) paintedParent.Paint -= DrawDiagram;
            base.Dispose(disposing);
        }
    }
}
