using Microsoft.AspNetCore.Components;
using Pure.Blazor.Components.Buttons;

namespace Pure.Blazor.Components.Common;

public class PureComponent : ComponentBase
{
    protected override void OnParametersSet()
    {
        // todo: figure out how to build css less
        BuildCss();
    }

    [CascadingParameter] public ThemeProvider? ThemeProvider { get; set; } = new();

    /// <summary>
    ///     Add additional css classes to this component
    /// </summary>
    [Parameter]
    public string? Styles { get; set; }

    /// <summary>
    ///     Disables or enables the theme. Default is Auto, which means the theme is inherited from the parent component.
    /// </summary>
    [CascadingParameter]
    public Theme Theme { get; set; } = Theme.Auto;

    /// <summary>
    ///     The current theme styles
    /// </summary>
    [CascadingParameter]
    public PureStyles PureStyles { get; set; } = new();

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected virtual void BuildCss()
    {
    }

    protected virtual string ApplyStyle(string? style)
    {
        if (Theme == Theme.Off)
        {
            return Styles ?? "";
        }

        if (style == null)
        {
            return Styles ?? "";
        }

        if (ThemeProvider?.StylePrioritizer != null && Styles != null)
        {
            return ThemeProvider.StylePrioritizer.PrioritizeStyles(style, Styles);
        }

        return $"{style} {Styles}";
    }
}