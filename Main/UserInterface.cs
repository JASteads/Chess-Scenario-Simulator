using System.Windows.Forms;

public abstract class UserInterface
{
    protected Panel panel;

    public UserInterface()
    {
        panel = new Panel();
        panel.SuspendLayout();
    }

    public Panel GetPanel()
    {
        return panel;
    }
}