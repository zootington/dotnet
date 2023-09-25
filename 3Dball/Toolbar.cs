using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Linq;

public class Toolbar
{
    private List<Button> _buttons = new List<Button>();
    private Button _hoveredButton = null;

    // Add a button to the toolbar
    public void AddButton(Button button)
    {
        _buttons.Add(button);
    }

    // Check if any button is under the given mouse position
    public bool IsMouseOver(Vector2 position)
    {
        return _buttons.Any(button => button.IsMouseOver(position));
    }

    // Get the button under the given mouse position
    public Button GetButtonAt(Vector2 position)
    {
        return _buttons.FirstOrDefault(button => button.IsMouseOver(position));
    }

    // Render the toolbar and its buttons
    public void Render()
    {
        foreach (var button in _buttons)
        {
            button.Render();
        }
    }

    // Update the toolbar (hover effects, etc.)
    public void Update(Vector2 mousePosition)
    {
        Button currentlyHovered = GetButtonAt(mousePosition);

        if (currentlyHovered != _hoveredButton)
        {
            if (_hoveredButton != null)
            {
                _hoveredButton.OnMouseLeave();
            }

            if (currentlyHovered != null)
            {
                currentlyHovered.OnMouseHover();
            }

            _hoveredButton = currentlyHovered;
        }
    }
}
