   M S B A M L   `   `   `  ����D  @KtvStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KtvStudio.ViewsKtvStudio.Views  5      K*clr-namespace:KtvStudio.Helpers.ConvertersKtvStudio.Helpers.Converters  5      &     KtvStudio.Views.SingerInfoEditUc   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:KtvStudio.Views     5      A
converters*clr-namespace:KtvStudio.Helpers.Converters     5        � 	Resources  5
      A   ;KtvStudio.Helpers.Converters.ImageUrlNullToDefaultConverter 
  Iuntdc�� 5   
   %   &	          5      68   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��40��6   
�������$��0q�6=   ._�5      ]� .+�5      �� $
��Left=�6   $��2 5 5 5q�62   $��10��6C   ���#A0D8F0��5   (   ��  �� ElementName$ toggleButtonBaseInfo�� �� Path$ Height�5   (   d� 5      -   5        $��toggleButtonBaseInfo��5      $��Right=�5   +   ��.  $
��Left=�5   +   
�������$��0q�5    +   
������ $	��5 0q�5"   +    d� 	IsChecked$
 TrueY�5#   +   $��歌手信息��5$   +   $��13(�5%   +   $��Normal"�5&   +   $
��Left=�5'   +   5(      5)      �� 5*   
   $��1��6   ���	#FFE0E0E0$��0 2 0 0q�5,      ���#A0D8F0 �� Padding$	 0 3q�5.      y�5/      �� $ toggleButtonBaseInfo��$ 	IsChecked�   BooleanToVisibilityConverter �� 	Converter# [ ._�50      �  51      �� 52      $
��Auto �6$   �� 53      $
��Auto �6$   �� 54      $
��Auto �6$   �� 55      $
��Auto �6$    � ColumnDefinitions 57      �� 58      $
��Auto �6'   �� 59      $
��Auto �6'   �� 5:      $
��Auto �6'   �� 5;      $
��Auto �6'   �� 5<      $
��Auto �6'   �� 5=      $
��Auto �6'   �� 5>      $
��Auto �6'   �� 5?      $
��Auto �6'   .+�5A      �� $��0��6   $��0��6)   $��歌手姓名��69   $��Center=�5B      $��5 5 0 5q�67   �� 5C      $��0��6   $��1��6'   $	��200��67   $��5q�6F   ��5D      �� *SingerInfoEditItem.SingerName+�� 5E      $��0��6   $��2��6)   $��歌手外文名��69   $��Center=�5F      $��5 5 0 5q�67   �� 5G      $��0��6   $��3��6'   $	��200��67   $��5q�6F   ��5H      �� *&$SingerInfoEditItem.SingerEnglishName+�� 5I      $��0��6   $��4��6)   $��歌手别名��69   $��Center=�5J      $��5 5 0 5q�67   �� 5K      $��0��6   $��5��6'   $	��200��67   $��5q�6F   ��5L      �� *$"SingerInfoEditItem.SingerOtherName+�� 5M      $��0��6   $��6��6)   $��歌手性别��69   $��Center=�5N      $��5 5 0 5q�67   �� 5O      $��0��6   $��7��6(   $	��200��68   $��5q�6G    �� SelectedIndex 5P      �� *SingerInfoEditItem.SingerSex+.@�5Q      �� $	��男��6#   �� 5R      $	��女��6#   �� 5S      $��组合��6#   5T      �� 5U      $��1��6   $��0��6)   $��歌手热度��69   $��Center=�5V      $��5 5 0 5q�67   �� 5W      $��1��6   $��1��6'   $	��200��67   $��5q�6F   ��5X      �� *#!SingerInfoEditItem.SingerClickNum+�� 5Y      $��1��6   $��2��6)   $��歌手国籍��69   $��Center=�5Z      $��5 5 0 5q�67   �� 5[      $��1��6   $��3��6(   $	��200��68   $��5q�6G   	 �� 
IsEditable	 . 
 �� DisplayMemberPath$
 
actionname��5]       �� SelectedValuePath$ 
actionname��6:   ��5\      �� *CountryInfoSource+ �� SelectedValue 5^      �� *&$SingerInfoEditItem.SingerNationality+�� 5_      $��2��6   $��0��6)   $��歌手介绍��69   $��Center=�5`      $��5 5 0 5q�67   �� 5a      $��2��6   $��1��6'   $��7��67   $
��1000��6K   $	��200��6[   $��5q�6j   $��80��6u    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  �� 
AcceptsTab . ��5b      �� *$"SingerInfoEditItem.SingerIntroduce+5c      5d      �� 5e   
   $��2��6   $��40��6   
�������$��0q�6=   ._�5f      ]� .+�5g      �� $
��Left=�6   $��2 5 5 5q�62   $��10��6C   ���#A0D8F0��5h   (   �� $ toggleButtonBaseInfo1��$ Height�5i   (   d� 5j      -   5        $��toggleButtonBaseInfo1��5j      $��Right=�5k   +   ��.  $
��Left=�5m   +   
�������$��0q�5o   +   
������ $	��5 0q�5q   +   $
 TrueY�5r   +   $��歌手信息��5s   +   $��13(�5t   +   $��Normal"�5u   +   $
��Left=�5v   +   5w      5x      �� 5y   
   $��3��6   ���	#FFE0E0E0$��0 2 0 0q�5{      ���#A0D8F0$	 0 3q�5}      y�5~      �� $ toggleButtonBaseInfo1��$ 	IsChecked�# [ ._�5      �  5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $��* �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $��0��6%   $��10q�65   $��80��6A   $��80��6L   ��5�      �� *#!SingerInfoEditItem.SingerPhotoUrl+# [  �� 5�      $��1��6   $��0��6&   $��上传图片��66   $��Center=�6E   $
��20 0q�6b   $��40��6q   ��5�      �� *SingerImageUpLoadCmd+�� 5�      $��0��6   $��3��6%   $��1��66   $��10q�6F   $	��420��6R   ��5�      �� *#!SingerInfoEditItem.SingerPhotoUrl+5�      5�      5�      5�      