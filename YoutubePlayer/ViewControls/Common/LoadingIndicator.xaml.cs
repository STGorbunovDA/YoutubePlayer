namespace YoutubePlayer.ViewControls.Common;

/// <summary>
/// Определяет пользовательский элемент управления, который позволяет отображать индикатор загрузки видео.
/// </summary>
public partial class LoadingIndicator : VerticalStackLayout
{
    public LoadingIndicator()
    {
        InitializeComponent();
    }

    public bool IsBusy
    {
        get => (bool)this.GetValue(IsBusyProperty);
        set => this.SetValue(IsBusyProperty, value);
    }

    public string LoadingText
    {
        get => (string)this.GetValue(LoadingTextProperty);
        set => this.SetValue(LoadingTextProperty, value);
    }

    //Bindable Properties

    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        "IsBusy",
        typeof(bool),
        typeof(LoadingIndicator),
        false,
        BindingMode.OneWay,
        null,
        SetIsBusy);

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
       "LoadingText",
       typeof(string),
       typeof(LoadingIndicator),
       string.Empty,
       BindingMode.OneWay,
       null,
       SetLoadingText);


    private static void SetIsBusy(BindableObject bindable, object oldValue, object newValue)
    {
        LoadingIndicator control = bindable as LoadingIndicator;

        control.IsVisible = (bool)newValue;
        control.actIndicator.IsRunning = (bool)newValue;
    }
 

    private static void SetLoadingText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as LoadingIndicator).lblLoadingText.Text = (string)newValue;
 
}