   M S B A M L   `   `   `  ����I  EClientControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]?clr-namespace:ClientControls.Behaviours;assembly=ClientControlsClientControls.Behaviours  5      D @KtvStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KtvStudio.ViewsKtvStudio.Views 5      K*clr-namespace:KtvStudio.Helpers.ConvertersKtvStudio.Helpers.Converters 5      $   KtvStudio.Views.SongInfoEditUc   5      A =Fluent, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullfluenturn:fluent-ribbon  H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      dclientControlsBehaviours?clr-namespace:ClientControls.Behaviours;assembly=ClientControls     5      ;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	      R NMahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5
      )localclr-namespace:KtvStudio.Views  5      ;
converters*clr-namespace:KtvStudio.Helpers.Converters  5      -   5          >�Name$  ThisUc��5       � 	Resources 5      P  JKtvStudio.Helpers.Converters.SingRailValueToComboBoxSelectedIndexConverter   Srvtcsic��%   &	          5   
   6C   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��40��6   
�������$��0q�6=   ._�5      ]� .+�5      �� $
��Left=�6   $��2 5 5 5q�62   $��10��6C   ���#A0D8F0��5      ��  �� ElementName$ toggleButtonBaseInfo�� �� Path$ Height�5       d� 5!      -   5        $��toggleButtonBaseInfo��5!      $��Right=�5"   +   ��.  $
��Left=�5$   +   
�������$��0q�5&   +   
������ $	��5 0q�5(   +    d� 	IsChecked$
 TrueY�5)   +   $��歌曲信息��5*   +   $��13(�5+   +   $��Normal"�5,   +   $
��Left=�5-   +   5.      5/      �� 50   
   $��1��6   ���	#FFE0E0E0$��0 2 0 0q�52      ���#A0D8F0 �� Padding$	 0 3q�54      y�55      �� $ toggleButtonBaseInfo��$ 	IsChecked�   BooleanToVisibilityConverter �� 	Converter# [ ._�56      �  57      �� 58      $
��Auto �6$   �� 59      $
��Auto �6$   �� 5:      $
��Auto �6$   �� 5;      $
��Auto �6$    � ColumnDefinitions 5=      �� 5>      $
��Auto �6'   �� 5?      $
��Auto �6'   �� 5@      $
��Auto �6'   �� 5A      $
��Auto �6'   �� 5B      $
��Auto �6'   �� 5C      $
��Auto �6'   �� 5D      $
��Auto �6'   �� 5E      $
��Auto �6'   .+�5G      �� $��0��6   $��0��6)   $��歌曲名称��69   $��Center=�5H      $��5 5 0 5q�67   �� 5I      -   5        $��0��5I      $��1��6'   $	��200��67   $��5q�6F   ��5J      �� *SongInfoEditItem.MusicName+5K      �� 5L      $��0��6   $��2��6)   $��歌手姓名��69   $��Center=�5M      $��5 5 0 5q�67   �� 5N      -   5        	 ��Name$	 SingerNameComboBox��5N      $��0��67   $��3��6D   $	��200��6T   $��5q�6c   
 �� 
IsEditable
 .  �� DisplayMemberPath$ 
singername��5R       �� SelectedValuePath$ id��5S       �� SelectedValue 5P      �� *SongInfoEditItem.SingerId+ �� Text 5Q      �� *SongInfoEditItem.SingerName+��5T      �� *SingerInfoSource+�� 5U      $��0��6   $��4��6)   $��语言种类��69   $��Center=�5V      $��5 5 0 5q�67   �� 5W      $��0��6   $��5��6(   $	��200��68   $��5q�6G   
 .  $ 
actionname��5Z      $ 
actioncode��5[       5Y      �� *SongInfoEditItem.LanguageType+��5\      �� *LanguageTypeSource+�� 5]      $��0��6   $��6��6)   $��发布日期��69   $��Center=�5^      $��5 5 0 5q�67   (  "System.Windows.Controls.DatePicker  5_      $��0��6   $��7��6*   $	��200��6:   $��5q�6I   $��Center=�5a      $��Stretch=�5b      *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Select a date��5c         WatermarkAlignment$ Right=�5d         SelectedDate 5`      �� *SongInfoEditItem.ReleaseDate+5d      5e      5f      �� 5g   
   $��2��6   $��40��6   
�������$��0q�6=   ._�5h      ]� .+�5i      �� $
��Left=�6   $��2 5 5 5q�62   $��10��6C   ���#A0D8F0��5j   (   �� $ toggleButtonBaseInfo1��$ Height�5k   (   d� 5l      -   5        $��toggleButtonBaseInfo1��5l      $��Right=�5m      ��.  $
��Left=�5o      
�������$��0q�5q      
������ $	��5 0q�5s      $
 TrueY�5t      $��分类设置��5u      $��13(�5v      $��Normal"�5w      $
��Left=�5x      5y      5z      �� 5{   
   $��3��6   ���	#FFE0E0E0$��0 2 0 0q�5}      ���#A0D8F0$	 0 3q�5      y�5�      �� $ toggleButtonBaseInfo1��$ 	IsChecked�# [ ._�5�      �  5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   $��0��6)   $��一级分类��69   $��Center=�5�      $��5 5 0 5q�67   �� 5�      -   5        $��0��5�      $��1��6(   $	��200��68   $��5q�6G   
 .  $ categoryname��5�      $ id��5�      ��5�      �� *FirstCategorySourceView+5�      �� 5�      $��0��6   $��2��6)   $��二级分类��69   $��Center=�5�      $��5 5 0 5q�67   �� 5�      -   5        $��0��5�      $��3��6(   $	��200��68   $��5q�6G   
 .  $ categoryname��5�      $ id��5�      ��5�      �� *SecondCategorySourceView+5�      �� 5�      $��0��6D   $��4��6Q   $	��0 5q�6a   $
��20 0q�6n   $��添加��6}    � DataContext 6   �� $ ThisUc����6�   �� *&$CategoryEditViewModel.AddCategoryCmd+�� 5�      $��1��6   $��0��6)   $��已选分类��69   $��Center=�5�      $��5 5 0 5q�67   ,  &MahApps.Metro.Controls.MetroTabControl  5�      -   5          Name$! SelectedCategorysTabControl��5�   +   $��1��6P   $��1��6]   $��5��6m   $��5q�6�   $��26��6�   6�   5�      5�      �� 5�   
   $��4��6   $��40��6   
�������$��0q�6=   ._�5�      ]� .+�5�      �� $
��Left=�6   $��2 5 5 5q�62   $��10��6C   ���#A0D8F0��5�   (   �� $ toggleButtonBaseInfo2��$ Height�5�   (   d� 5�      -	   5        $��toggleButtonBaseInfo2��5�      $��Right=�5�   +   ��.  $
��Left=�5�   +   
�������$��0q�5�   +   
������ $	��5 0q�5�   +   $
 TrueY�5�   +   $��视频上传��5�   +   $��13(�5�   +   $��Normal"�5�   +   $
��Left=�5�   +   5�      5�      �� 5�   
   $��5��6   ���	#FFE0E0E0$��0 2 0 0q�5�      ���#A0D8F0$	 0 3q�5�      y�5�      �� $ toggleButtonBaseInfo2��$ 	IsChecked�# [ ._�5�      �  5�      �� 5�      $
��Auto �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   
�������$��2q�6=   $
��Left=�6Q   $��5q�6l   ._�5�      �  5�      �� 5�      $
��Auto �6,   �� 5�      $
��Auto �6,    5�      �� 5�      $��* �6/   �� 5�      $
��Auto �6/   .+�5�      r� -
   5         r�Name$ vedio��5�   D   $��0��6'   $��0��64    r� LoadedBehavior$ Manual=�6S    r� Stretch$ Uniform=�6k   $	��425��5�   '   $	��500��64    r� Source 5�   '   �� *SongInfoEditItem.MVUrl+5�   '   �� 5�      $��0��6%   $��1��62    �� Orientation$ Vertical=�6B   $��0 5 0 0q�6Y   .+�5�        Fluent.Button  -   5           Header$ 重播��5�   A   $��20 0 0 0q�6M      	LargeIcon 5�   "   1  +MahApps.Metro.IconPacks.PackIconFontAwesome  5�   &   $��5 0 0 0q�6D   �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconFontAwesomeKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]   Kind$ 	UndoSolid=�6U   $��26��6f   $��26��6q   5�        5�      -   5        $ 播放/暂停��5�   F   $
��10 0q�6U    5�   "     5�   &   $��5 0 0 0q�6D   $ 	PlaySolid=�6U   $��26��6f   $��26��6q   5�        5�      -   5        $ 停止��5�   @   $��20 0 0 0q�6L    5�   "   .  (MahApps.Metro.IconPacks.PackIconMaterial  5�   &   $��5 0 0 0q�6A   �	  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]] 	  Kind$
 Stop=�6R   $��26��6^   $��26��6i   5�      5�      �� 5�      $��1��6%   $��0��62   $��2��6B   $ 
Horizontal=�6V   .+�5�      �� -   5         ��Name$ sliderPosition��5�   6   $��5 0 0 0q�6%   $	��500��6N   $��18��5�   P   �� 5�      -   5         ��Name$ vedioAllTime��5�   (   $��0��6>   �� 5�      $��音量��6(   $��10 0 2 0q�62   �� 5�      $��0 0 5 0q�6%    � Minimum$ 0Q�66     � Maximum$  1Q�6B   $��50��6�   $��18��6�   ! � Value! 6N   �� $ vedio��$ Volume�" �� Mode$" TwoWay=�6�   �� 5�      $��音道��6(   $��10 0 2 0q�62   �� 5�      $��0 0 5 0q�6%   $ -1Q�66   $  1Q�6C   $��50��6�   $��18��6�   ! 6O   �� $ vedio��$ Balance�$" TwoWay=�6�   5�      5�      5�      � 5�      $��1��6    5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6!   $ 
Horizontal=�6.   .+�5�      �� $��预览方式��6$   $��Center=�60   $��5 5 0 5q�6K   )
  #MahApps.Metro.Controls.ToggleSwitch
  5�      -   5        $��5q�5�   0   # 
  	IsChecked$# FalseY�5�   0   $��13(�6C   $ 
  OffLabel$$ 云端��5�   0   % 
  OnLabel$% 本地��6>    + 'MahApps.Metro.Styles.ToggleSwitch.Win10#��[ 5   0   �� 5     $<��6云端播放可能会出现卡顿，请耐心等待！��6$   
���   �& J� Opacity$	& 0.5Q�5  7   $��Center=�6E   $��5 5 0 5q�6`   5     �� 5     $��1��6!   $ 
Horizontal=�6.   .+�5     �� $��本地路径��6$   $��Center=�60   $��5 5 0 5q�6K   �� 5     $	��200��6"   $��5q�61   {�.  ��5  "   �� *SongLocationMVUrl+6E   �� 5     $��选择��6!   $
��20 0q�6.   $��Center=�6=   $��5q�6Z   ��5	  !   �� *SongMVUrlSelectCmd+�� 5
     $��上传��6!   $
��20 0q�6.   $��Center=�6=   $��5q�6Z   ��5  !   �� *SongMVUpLoadCmd+5     �� 5     $��2��6!   $ 
Horizontal=�6.   .+�5     �� $��云端路径��6%   $��Center=�61   $��5 5 0 5q�6L   �� 5     $	��200��6"   $��5q�61   ��6<   �� *SongInfoEditItem.MVUrl+5     �� 5     $��3��6!   $ 
Horizontal=�6.   .+�5     �� $��原唱音道��6$   $��Center=�60   $��5 5 0 5q�6K   �� 5     $	��200��6#   $��5q�62   ' �� SelectedIndex' 5  #   �� *SongInfoEditItem.SingRail+# [  .@�5     �� $	��左��6+   �� 5     $	��右��6+   5     5     5     5     5     5     5     