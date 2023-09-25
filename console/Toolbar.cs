using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace ToolbarNS
{
    public class Toolbar
    {
        private List<ToolbarButton> buttons = new List<ToolbarButton>();

        public Toolbar()
        {
            buttons.Add(new ToolbarButton(10, 10, 100, 30, "Button1"));
            buttons.Add(new ToolbarButton(120, 10, 100, 30, "Button2"));
        }

        public void Render()
        {
            foreach (var button in buttons)
            {
                button.Render();
            }
        }

        public ToolbarButton? GetButtonAt(int x, int y)
        {
            foreach (var button in buttons)
            {
                if (button.IsInside(x, y))
                {
                    return button;
                }
            }

            return null;
        }
    }
}