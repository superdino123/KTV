Fluent.Ribbon
=============

[![Join the chat at https://gitter.im/fluentribbon/Fluent.Ribbon](https://img.shields.io/badge/GITTER-join%20chat-green.svg?style=flat-square)](https://gitter.im/fluentribbon/Fluent.Ribbon?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Twitter](https://img.shields.io/badge/twitter-%40batzendev-blue.svg?style=flat-square)](https://twitter.com/batzendev)

[![Build status](https://img.shields.io/appveyor/ci/batzen/fluent-ribbon.svg?style=flat-square)](https://ci.appveyor.com/project/batzen/fluent-ribbon)
[![Release](https://img.shields.io/github/release/fluentribbon/fluent.ribbon.svg?style=flat-square)](https://github.com/fluentribbon/Fluent.Ribbon/releases/latest)
[![Issues](https://img.shields.io/github/issues/fluentribbon/fluent.ribbon.svg?style=flat-square)](https://github.com/fluentribbon/Fluent.Ribbon/issues)
[![Downloads](https://img.shields.io/nuget/dt/Fluent.Ribbon.svg?style=flat-square)](http://www.nuget.org/packages/Fluent.Ribbon/)
[![Nuget](https://img.shields.io/nuget/vpre/Fluent.Ribbon.svg?style=flat-square)](http://nuget.org/packages/Fluent.Ribbon)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](https://github.com/fluentribbon/Fluent.Ribbon/blob/master/License.txt)

This project was previously hosted on [CodePlex](https://fluent.codeplex.com/).

![Banner](./Images/banner.png)

Fluent.Ribbon is a library that implements an Office-like user interface for the Windows Presentation Foundation (WPF). It provides controls such as RibbonTabControl, Backstage, Gallery, QuickAccessToolbar, ScreenTip and so on.

![Showcase](./Images/Showcase.gif)

*   [More Screenshots](../../wiki/Screenshots)

# Contact
If you wish to contact me (batzen) directly please use [twitter](https://twitter.com/batzendev) or [gitter](https://gitter.im/batzen).

# Access to preview versions
You can access preview versions through the [AppVeyor nuget feed](https://ci.appveyor.com/nuget/fluent-ribbon).


# Contributing
## What you can do to help us
*   We are accepting pull requests, so you are very welcome to create one
*   [Fix some bugs](../../issues)
*   Help us translating
*   Help us updating the documentation and walkthrough

## Development requirements
* Visual Studio 2015 or later
* Optional (but recommended)
  * Editorconfig extension for Visual Studio

## Settings that should be used
*   Visual Studio settings which should be used
  *  All languages
    * Indentation: 4 spaces (please don't use tabs)
  * XAML
    * Position each attribute on a separate line
      * Position first attribute on same line as start tag

# Documentation
Visit the [documentation](http://fluentribbon.github.io/documentation/).  
Almost all features are shown in the showcase application.  
The showcase application is included with every release, so you can just grab it from [releases](../../releases)  
If you think there is something missing in the showcase application feel free to create an issue for that.

To be more familiar with the Ribbon concept see [msdn article](http://msdn.microsoft.com/en-us/library/cc872782.aspx).

# History &amp; roadmap
A history of changes is maintained in the [Changelog](Changelog.md).
The roadmap is done by [milestones](../../milestones).

# Feature List
| Office UI Element |  Status |
| ----- | ----- |
| **Backstage** |   |
| Displaying the Backstage Button |  Full Support |
| Backstage Menu Controls |  Partial |
| **Application Menu** |   |
| Displaying the Application Button |  Full Support |
| Application Menu Styles |  Full Support |
| **Tabs** |   |
| Displaying Tabs |  Full Support |
| Minimizing the Ribbon |  Full Support |
| Tab Scrolling |  Full Support |
| **Groups** |   |
| Displaying Groups |  Full Support |
| Group Size Reducing / Increesing |  Full Support |
| Dialog Box Launchers |  Full Support |
| **Controls** |   |
| Button |  Full Support |
| ToggleButton |  Full Support |
| DropDownButton |  Full Support |
| SplitButton |  Full Support |
| TextBox |  Full Support |
| CheckBox |  Full Support |
| ComboBox |  Full Support |
| Spinner |  Full Support |
| Toolbar |  Full Support |
| ColorGallery (ColorPicker) |  Full Support |
| **Ribbon Resizing** |   |
| Defining Groups for Ribbon Resizing |  Full Support |
| Collapsed Group Behavior |  Full Support |
| Defining Group Combinations for Ribbon Resizing |  Full Support |
| Group Horizontal Scrolling |  Full Support |
| Tabs Compression |  Full Support |
| **Quick Access Toolbar (QAT)** |   |
| Displaying QAT |  Full Support |
| Ribbon right-click QAT support |  Full Support |
| Position QAT below the Ribbon |  Full Support |
| Displaying Many Controls in the QAT |  Full Support |
| **Keyboard Access** |   |
| Displaying KeyTips |  Full Support |
| Dismissing KeyTips |  Full Support |
| Keyboard Navigation |  Partial |
| KeyTip Size and Positioning |  Full Support |
| KeyTips for Collapsed Groups |  Full Support |
| KeyTips for All Kind of Menu and Submenu |  Full Support |
| KeyTips for Backstage |  Partial |
| KeyTips custom placement |  Full Support |
| KeyTips for Quick Access Toolbar |  Full Support |
| **Galleries** |   |
| Displaying Galleries |  Full Support |
| Gallery inline/popup support |  Full Support |
| In-Ribbon Galleries |  Full Support |
| Resizing Expanded In-Ribbon Galleries |  Full Support |
| Filtering Gallery Groups |  Full Support |
| **Mini-Toolbar** |   |
| Displaying the Mini Toolbar |  Not Implemented |
| Dismissing the Mini Toolbar |  Not Implemented |
| Controls Displayed on the Mini Toolbar |  Not Implemented |
| Displaying the Mini Toolbar with Context Menus |  Not Implemented |
| **ScreenTips** |   |
| Displaying ScreenTips |  Full Support |
| F1 help access |  Full Support |
| Disable Reason Text |  Full Support |
| Image in ScreenTip |  Full Support |

If you feel lack of some important features feel free to use [issues](https://github.com/fluentribbon/Fluent.Ribbon/issues) to create an issue/feature request.

# Localizations
*   Arabic
*   Azerbaijani
*   Catalan
*   Chinese
*   Chinese (Traditional)
*   Czech
*   Danish
*   Dutch
*   English
*   Estonian
*   Finnish
*   French
*   German
*   Greek
*   Hebrew
*   Hungarian
*   Italian
*   Japanese
*   Korean
*   Lithuanian
*   Norwegia
*   Russian
*   Persian
*   Polish
*   Portuguese
*   Portuguese (Brazilian)
*   Romanian
*   Sinhala
*   Slovak
*   Slovenian
*   Spanish
*   Swedish
*   Turkish
*   Ukrainian
*   Vietnamese

## Awesome tools which Fluent.Ribbon can use

[![Resharper](./Images/icon_ReSharper.png)](https://www.jetbrains.com/resharper/)

## Licence

[MIT License (MIT)](./License.txt)