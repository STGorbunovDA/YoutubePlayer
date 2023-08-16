namespace YoutubePlayer.Views.Base;

public partial class PageBase : ContentPage
{
    /// <summary>
    ///  ѕредоставл€ют доступ к спискам дочерних элементов PageContentGrid и PageIconsGrid соответственно. 
    ///  Ёто позвол€ет управл€ть содержимым и иконками страницы с помощью этих списков.
    /// </summary>
    public IList<Microsoft.Maui.IView> PageContent => PageContentGrid.Children;
    public IList<Microsoft.Maui.IView> PageIcons => PageIconsGrid.Children;

    /// <summary>
    /// ”станавливает значение доступности кнопки навигации назад.
    /// </summary>
    protected bool IsBackButtonEnabled
    {
        set => NavigateBackButton.IsEnabled = value;
    }

    public PageBase()
    {
        InitializeComponent();

        //—крывает встроенную навигационную панель Xamarin Forms.
        NavigationPage.SetHasNavigationBar(this, false);

        //”станавливает режим страницы.
        SetPageMode(PageMode.None);

        //”станавливает режим отображени€ контента без навигационной панели.
        SetContentDisplayMode(ContentDisplayMode.NoNavigationBar);
    }

    

    #region Bindable properties

    public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);
        set => SetValue(PageTitleProperty, value);
    }

    public PageMode PageMode
    {
        get => (PageMode)GetValue(PageModeProperty);
        set => SetValue(PageModeProperty, value);
    }

    public ContentDisplayMode ContentDisplayMode
    {
        get => (ContentDisplayMode)GetValue(ContentDisplayModeProperty);
        set => SetValue(ContentDisplayModeProperty, value);
    }


    public static readonly BindableProperty PageTitleProperty = BindableProperty.Create(
        nameof(PageTitle),
        typeof(string),
        typeof(PageBase),
        string.Empty,
        defaultBindingMode:
        BindingMode.OneWay,
        propertyChanged: OnPageTitleChanged);

    public static readonly BindableProperty PageModeProperty = BindableProperty.Create(
           nameof(PageMode),
           typeof(PageMode),
           typeof(PageBase),
           PageMode.None,
           propertyChanged: OnPageModePropertyChanged);

    public static readonly BindableProperty ContentDisplayModeProperty = BindableProperty.Create(
        nameof(ContentDisplayMode),
        typeof(ContentDisplayMode),
        typeof(PageBase),
        ContentDisplayMode.NoNavigationBar,
        propertyChanged: OnContentDisplayModePropertyChanged);

    private static void OnPageTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
        {
            basePage.TitleLabel.Text = (string)newValue;
            basePage.TitleLabel.IsVisible = true;
        }
    }


    private static void OnPageModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
            basePage.SetPageMode((PageMode)newValue);
    }

    private static void OnContentDisplayModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable != null && bindable is PageBase basePage)
            basePage.SetContentDisplayMode((ContentDisplayMode)newValue);
    }

    /// <summary>
    /// —крывает встроенную навигационную панель Xamarin Forms.
    /// </summary>
    /// <param name="pageMode"></param>
    private void SetPageMode(PageMode pageMode)
    {
        switch (pageMode)
        {
            case PageMode.Menu:
                HamburgerButton.IsVisible = true;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Navigate:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = true;
                CloseButton.IsVisible = false;
                break;
            case PageMode.Modal:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = true;
                break;
            default:
                HamburgerButton.IsVisible = false;
                NavigateBackButton.IsVisible = false;
                CloseButton.IsVisible = false;
                break;
        }
    }


    /// <summary>
    /// ”станавливает режим отображени€ контента без навигационной панели.
    /// </summary>
    /// <param name="contentDisplayMode"></param>
    private void SetContentDisplayMode(ContentDisplayMode contentDisplayMode)
    {
        switch (contentDisplayMode)
        {
            case ContentDisplayMode.NavigationBar:
                Grid.SetRow(PageContentGrid, 2);
                Grid.SetRowSpan(PageContentGrid, 1);
                break;
            case ContentDisplayMode.NoNavigationBar:
                Grid.SetRow(PageContentGrid, 0);
                Grid.SetRowSpan(PageContentGrid, 3);
                break;
            default:
                //Do Nothing
                break;
        }
    }
    #endregion


    
}