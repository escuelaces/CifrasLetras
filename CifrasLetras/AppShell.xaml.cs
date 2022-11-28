namespace CifrasLetras;

public partial class AppShell : Shell
{
	public AppShell()
	{
		BindingContext = new CifrasyLetras();
        InitializeComponent();
	}
}

