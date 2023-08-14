namespace YoutubePlayer.ViewControls.Common;

/// <summary>
/// Определяет пользовательский элемент управления, который позволяет отображать информацию об ошибке, включая текст и изображение.
/// </summary>
public partial class ErrorIndicator : VerticalStackLayout
{
    public ErrorIndicator()
    {
        InitializeComponent();
    }

    public bool IsError
    {
        get => (bool)this.GetValue(IsErrorProperty);
        set => this.SetValue(IsErrorProperty, value);
    }

    public string ErrorText
    {
        get => (string)this.GetValue(ErrorTextProperty);
        set => this.SetValue(ErrorTextProperty, value);
    }

    public ImageSource ErrorImage
    {
        get => (ImageSource)this.GetValue(ErrorImageProperty);
        set => this.SetValue(ErrorImageProperty, value);
    }

    //Bindable Properties
    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
        "IsError",
        typeof(bool),
        typeof(ErrorIndicator),
        false,
        BindingMode.OneWay,
        null,
        SetIsError);

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        "ErrorText",
        typeof(string),
        typeof(ErrorIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetErrorText);

    public static readonly BindableProperty ErrorImageProperty = BindableProperty.Create(
       "ErrorImage",
       typeof(ImageSource),
       typeof(ErrorIndicator),
       null,
       BindingMode.OneWay,
       null,
       SetErrorImage);


    private static void SetIsError(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).IsVisible = (bool)newValue;

    private static void SetErrorText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).lblErrorText.Text = (string)newValue;

    private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).imgError.Source = (ImageSource)newValue;


    
}