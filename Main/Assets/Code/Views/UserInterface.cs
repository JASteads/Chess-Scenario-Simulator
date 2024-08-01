using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public abstract class UserInterface
{
    protected List<Control> controls;

    public UserInterface()
    {
        controls = new List<Control>();
    }

    public List<Control> GetControls()
    {
        return controls;
    }
}