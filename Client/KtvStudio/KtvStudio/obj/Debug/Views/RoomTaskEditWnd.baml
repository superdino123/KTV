   M S B A M L   `   `   `  ����D  @KtvStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KtvStudio.ViewsKtvStudio.Views  5      B >Helpers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH1clr-namespace:Helpers.Converters;assembly=HelpersHelpers.Converters 5      K*clr-namespace:KtvStudio.Helpers.ConvertersKtvStudio.Helpers.Converters  5	      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M4clr-namespace:System.Globalization;assembly=mscorlibSystem.Globalization 5      %    KtvStudio.Views.RoomTaskEditWnd   5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /localclr-namespace:KtvStudio.Views     5      H
converters1clr-namespace:Helpers.Converters;assembly=Helpers     5      Bconverters1*clr-namespace:KtvStudio.Helpers.Converters     5	      ;Controls,http://metro.mahapps.com/winfx/xaml/controls  5
      Hglobalization4clr-namespace:System.Globalization;assembly=mscorlib  5      -   5        $��
ThisWindow��5      $��1q�5      
�������  � Title$  包房管理��5      $	��310��6$   $	��575��61    � WindowStartupLocation$ CenterScreen=�5       � WindowState$ Normal=�6&    � DataContext 5      ��  �� ElementName$ 
ThisWindow�� �� Path$ ClientViewModel� � 	Resources 5      9  3Helpers.Converters.BooleanInvertVisibilityConverter   BivcA   ;KtvStudio.Helpers.Converters.DateTimeNullToDefaultConverter 	 Dntdc9   3KtvStudio.Helpers.Converters.SexNameToBoolConverter 	 Sntbc/  )Helpers.Converters.BooleanInvertConverter  Bic��%t   &	        &	      &	 &     &	 9       5   
   66     5   
   65     5   
   6-     5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    5       �� 5!      $
��Auto �6    �� 5"      $
��Auto �6    .+�5$      �� $��0��6   $��40��6"   
�������$��0q�6A   ._�5%      ]� .+�5&      �� $
��Left=�6    $��2 5 5 5q�66   $��10��6G   ���#A0D8F0��5'   (   �� $ toggleButtonBaseInfo��$ Height�5(   (   d� 5)      -   5        $��toggleButtonBaseInfo��5)   #   $��Right=�5*   +   ��.  $
��Left=�5,   +   
�������$��0q�5.   +   
������ $	��5 0q�50   +    d� 	IsChecked$
 TrueY�51   +   $��包间信息��52   +   $��13(�53   +   $��Normal"�54   +   $
��Left=�55   +   56      57      �� 58      $��1��6   ���	#FFE0E0E0$��0 2 0 0q�5:      ���#A0D8F0	 �� Padding$		 0 3q�5<      y�5=      �� $ toggleButtonBaseInfo��$ 	IsChecked�   BooleanToVisibilityConverter
 �� 	Converter#
 [ ._�5>      �  5?      �� 5@      $
��Auto �6(   �� 5A      $
��Auto �6(   �� 5B      $
��Auto �6(    � ColumnDefinitions 5D      �� 5E      $
��Auto �6+   �� 5F      $
��Auto �6+   �� 5G      $
��Auto �6+   �� 5H      $
��Auto �6+   .+�5J      �� $��0��6    $��0��6-   $��	房间号��6=   $��Center=�5K       $��5 5 0 5q�6;   �� 5L      $��0��6   $��1��6+   $	��200��6;   $��5q�6J   {�.  ��5M      �� *RoomTaskEditItem.RoomId+6G   �� 5N      $��0��6    $��2��6-   $��预计消费[元]��6=   $��Center=�5O       $��5 5 0 5q�6;   �� 5P      $��0��6   $��3��6+   $	��200��6;   $��5q�6J   {�.  ��5Q      �� *RoomTaskEditItem.RoomConsume+6L   �� 5R      $��1��6    $��0��6-   $��起始时间��6=   $��Center=�5S       $��5 5 0 5q�6;   +  %MahApps.Metro.Controls.DateTimePicker  5T      -   5        $��1��5T   .   $��1��6;   $	��200��6K   $��5q�6Z    �� CurrentCulture+  %MahApps.Metro.Controls.TimePickerBase   Culture# Z 5V   .      SelectedDate 5U   .   �� *RoomTaskEditItem.StartTime+#
 [ {�5Y   .   �� *UserInfoWhenRenewNoVisibility+#
 [ �� 5Z      $��1��6    $��2��6-   $��消费时长��6=   $��Center=�5[       $��5 5 0 5q�6;   *  $MahApps.Metro.Controls.NumericUpDown  5\      -   5          Name$ ConsumeTimeValue��5^   -   $��1��5\   -   $��3��6:   $	��200��6J   $��5q�6Y      HasDecimals .     Minimum$ 0Q�6x   5^   -     5_      -   5        $��1��5_   .   $��3��6;   $	��200��6K   $��5q�6Z   $y�	Collapsed=�6e   # Z 5a   .    5`   .   �� *RoomTaskEditItem.EndTime+#
 [ 5c   .   5d      5e      5f      � 5g   
   $��1��6c   {�6   �� *UserInfoWhenRenewNoVisibility+#
 [  5h      �� 5i      $
��Auto �6    �� 5j      $
��Auto �6    .+�5l      �� $��0��6   $��40��6"   
�������$��0q�6A   ._�5m      ]� .+�5n      �� $
��Left=�6    $��2 5 5 5q�66   $��10��6G   ���#A0D8F0��5o   (   �� $ toggleButtonUserInfo1��$ Height�5p   (   d� 5q      -   5        $��toggleButtonUserInfo��5q   #   $��Right=�5r   +   ��.  $
��Left=�5t   +   
�������$��0q�5v   +   
������ $	��5 0q�5x   +   $
 TrueY�5y   +   $��用户信息��5z   +   $��13(�5{   +   $��Normal"�5|   +   $
��Left=�5}   +   5~      5      �� 5�      $��1��6   ���	#FFE0E0E0$��0 2 0 0q�5�      ���#A0D8F0$		 0 3q�5�      y�5�      �� $ toggleButtonUserInfo1��$ 	IsChecked�#
 [ ._�5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(    5�      �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6    $��0��6-   $��姓氏/姓名��6=   $��Center=�5�      $��5 5 0 5q�67   �� 5�      $��0��6   $��1��6+   $	��200��6;   $��5q�6J   ��5�      �� *UserInfo.CustomerName+�� 5�      $��0��6    $��2��6-   $��性别��6=   $��Center=�5�      $��5 5 0 5q�67   )  #MahApps.Metro.Controls.ToggleSwitch  5�      $��0��6,   $��3��69   $	��200��6I   $��5q�6X   $��Center=�6c      OffLabel$	 男��5�   ,      OnLabel$	 女��69    + 'MahApps.Metro.Styles.ToggleSwitch.Win10#��[ 5�   ,      	IsChecked 5�   ,   �� *UserInfo.CustomerSex+#
 [ 5�   ,   �� 5�      $��1��6    $��0��6-   $��联系方式��6=   $��Center=�5�      $��5 5 0 5q�67   �� 5�      $��1��6   $��1��6+   $	��200��6;   $��5q�6J   ��5�      �� *UserInfo.CustomerTel+5�      5�      5�      �� 5�   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5�      �� $��取消��6   $��5q�6"   $	��100��6-   ��69   �� *RoomTaskEditWndCloseCmd+�� 5�      $��确认��6   $��5q�6"   $	��100��6-   ��69   �� *RoomTaskSaveCmd+5�      5�      5�      