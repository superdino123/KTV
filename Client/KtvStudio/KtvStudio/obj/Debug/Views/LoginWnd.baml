   M S B A M L   `   `   `  ����D  @KtvStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KtvStudio.ViewsKtvStudio.Views  5      K*clr-namespace:KtvStudio.Helpers.ConvertersKtvStudio.Helpers.Converters  5          KtvStudio.Views.LoginWnd   5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /localclr-namespace:KtvStudio.Views     5      ;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      ;
converters*clr-namespace:KtvStudio.Helpers.Converters  5      -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  
ThisWindow��5         ShowIconOnTitleBar .     ShowCloseButton .    ShowTitleBar .   � 
ResizeMode$ NoResize=�5         icon � Icon# �   5         IsWindowDraggable .   � WindowStartupLocation$ CenterScreen=�5       � WindowState$ 	Maximized=�5      	 � Title$	 登录��5      
 � DataContext
 5      ��  �� ElementName$ 
ThisWindow�� �� Path$ ClientViewModel���5      �� 5   
     	loginBack �� ImageSource# �  6    � 	Resources 5      7   1KtvStudio.Helpers.Converters.UserAddressConverter  Uac��%   &	         5   
   6*   5      .��5      � $��Center=�6   $��Stretch=�6&    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5!   
   �� $��0��6   $��3��6   
���   � J� Opacity$	 0.3Q�6B   �� 5"   
   $��0��6   $	��200��6   $	��200��6*   $��0 50 0 0q�66   #���   5#      �� 5$   
   -   5         ��Name$ LoginTabControl��5%      $��1��5$       �� TabStripPlacement$	 Top=�6"   
������ $��0 50 0 0q�6S   $��Center=�6e   .@�5&      �� .��5'      �  5(      �� 5)      $
��Auto �6(   �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   .+�5-      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   .+�5.      �� 
������� �� CornerRadius$ 10��65   $��1q�6G    �� Padding$
 10 3q�6[   $
��20 0q�6j   ._�5/      �� 
������ 
�������$��18(�6S   $��Center=�6a   $	��250��6|   $��0q�6�    �� SelectedIndex 50   '   �� *CurrentStaffInfo.UserAddress+ �� 	Converter# [ .@�51   "   �� $��田林东路店��6/   52      53      �� 54      
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $
��20 0q�6j   ._�55      �� *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ 请输入工号��6&   
������ 
�������$��18(�56   R   $	��250��6`   $��0q�6l   ��57   &   �� *CurrentStaffInfo.UserRecord+58      �� 59      
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $
��20 0q�6j   ._�5:      G� -   5        
������ 
�������$��18(�5:   V   $	��250��6d   $��0q�6p      IsWaitingForData . $ 请输入密码��5=   *      ClearTextButton 5;   *   ��  �� RelativeSource �� *Self+$&  (Controls:TextBoxHelper.HasText)� �� Mode$ OneWay=�5>   *   5?      5@      �� 5A      -   5        $��1��5A      $��Right=�6*   
�������$��15(�6Y   $
��20 0q�6g   
������ $��0q�6�   $��0q�6�   $��Normal"�6�   $��修改密码��5B      6,   �� 5C      $��2��6   $��登 录��6*   $��Normal"�68   +  %MahApps.Metro.Controls.ControlsHelper   CornerRadius$ 10��6L   $��20 50 20 120q�6v   
������ $��0q�6�   $��20(�6�   
���������5D      �� *
LoginCmd+5E      5F      �� 5G      .��5H      � $��	0 0 0 180q�6    � ColumnDefinitions 5I      �� 5J      $
��Auto �6+   �� 5K      $
��Auto �6+   .+�5M      �� $��0��6!   $ Vertical=�61   $��Center=�6H   $��0 0 50 0q�6c   .+�5N      �� 
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $��20 10q�6j   ._�5O      �� $��18(�6&   $	��250��64   $��0q�6@   
������ 
�������$
��10 0q�6�   $ 请输入工号��5P   &   ��5Q   &   �� *CurrentStaffInfo.UserRecord+5R      �� 5S      
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $��20 10q�6j   ._�5T      �� -   5         ��Name$ OldPasswordTextBox��5V   &   $��18(�5T   &   $	��250��64   $��0q�6@   
������ 
�������$
��10 0q�6�   $ 请输入原密码��5U   &   5V   &   5W      �� 5X      
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $��20 10q�6j   ._�5Y      G� -   5          G�Name$  NewPasswordTextBox��5[   *   $��18(�5Y   *   $	��250��68   $��0q�6D   
������ 
�������$
��10 0q�6�   $ 请输入新密码��5Z   *   5[   *   5\      �� 5]      
�������$ 10��65   $��1q�6G   $
 10 3q�6[   $��20 10q�6j   ._�5^      G� -   5        $  NewPasswordAgainTextBox��5`   *   $��18(�5^   *   $	��250��68   $��0q�6D   
������ 
�������$
��10 0q�6�   $ 请再次输入新密码��5_   *   5`   *   5a      5b      �� 5c      $��1��6!   $��Center=�61   .+�5d      �� -   5        $��20(�5d   !   $��0q�6/   
������ 
�������$��Normal"�6o   $
��0 20q�6�   $��返回登录��5e   !   60   �� 5f      -	   5        $��20(�5f   !   $��0q�6/   
������ 
�������$��Normal"�6o   $
��0 20q�6�   $��修改密码��5g   !   5h   !   5i      5j      5k      5l      � 5m   
   $��2��6   .+�5n      �� $��Center=�6   
�������$	 0.5Q�6H   $��0 0 0 30q�6V   �� 5o      -
   5        $)��#https://github.com/superdino123/KTV?�5o      .�6f   Github6n   5p      5q      5r      5s      