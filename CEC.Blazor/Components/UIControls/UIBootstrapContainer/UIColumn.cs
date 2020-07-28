﻿using Microsoft.AspNetCore.Components;

namespace CEC.Blazor.Components.UIControls
{
    /// <summary>
    /// UI Rendering Wrapper to build a Column
    ///  Provides a structured  mechanism for managing Bootstrap class elements used in Editors and Viewers in one place. 
    /// The properties are pretty self explanatory and therefore not decorated with summaries
    /// </summary>

    public class UIColumn : UIColumnBase
    {
        protected override string _Css => $"col-{Columns} {this.FormGroup}{AddOnCss.Trim()}".Trim();
    }
}