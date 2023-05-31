using DroneController.Drawing;
using System;
using System.Drawing;

namespace DroneController.Indicators
{
    /// <summary>
    /// Heading Indicator.</summary>
    public class HeadingIndicator :
        Indicator,
        IGraphicCanvas,
        IDisposable
    {
        
        protected float heading;
        
        public float Heading
        {
            get { return heading; }
            set { heading = value; }
        }
        /// <summary>
        /// New Drawing Envelope received.</summary>
        protected override void NewDrawingEnvelopeReceived()
        {
        }

        /// <summary>
        /// Draw Function.</summary>
        /// <param name="g">Graphics for Drawing</param>
        public override void Draw(Graphics g)
        {

        }
    }
}
