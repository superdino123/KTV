   M S B A M L   `   `   `  ����C  ?KtvMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:KtvMusicKtvMusic  5   	   /clr-namespace:KtvMusic.ViewsKtvMusic.Views  5	   	   I)clr-namespace:KtvMusic.Helpers.ConvertersKtvMusic.Helpers.Converters  5
   	       KtvMusic.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   R NMahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5   	   "localclr-namespace:KtvMusic  5   	   (viewsclr-namespace:KtvMusic.Views  5	   	   :
converters)clr-namespace:KtvMusic.Helpers.Converters  5
   	   -   5          �Name$  
ThisWindow��5   	    � WindowStyle$
 None=�5   	    � WindowState$ 	Maximized=�5   	   ��5      !� 5   
    j� TileMode$
 Tile=�6    j� Viewport$ 0,0,1,1�6'    !� Visual 5      �� 5         musicBackground#���   6   6E   5       � 	Resources 5        Btvc@   :KtvMusic.Helpers.Converters.MusicStrNullToDefaultConverter 
 Msntdc  CloseWindow �� Text��%j   &	       &	      &	 &     �� 5   
   6'     5   
   64   �� 5   
   $ 关闭窗口��6.   5       J� InputBindings 5      �� 5   
   
	 �� Key$		 Esc��6   
 �� Command#
 [ 6    J� CommandBindings 5       )  #System.Windows.Input.CommandBinding  5!   
   -   5           Command# [ 5!      5#      .��5%      �  � DataContext 6   ��  �� ElementName$ 
ThisWindow�� �� Path$ MusicViewModel� � RowDefinitions 5&   
   �� 5'      $
��Auto �6   �� 5(      $��* �6   �� 5)      $
��Auto �6   .+�5+   
   �� $��0��6   
������� J� Opacity$	 0.1Q�61   6A   � 5,   
   $��0��6   $��5q�6    � ColumnDefinitions 5-      �� 5.      $
��Auto �6#   �� 5/      $
��Auto �6#   �� 50      $
��Auto �6#   �� 51      $
��Auto �6#   �� 52      $
��Auto �6#   �� 53      $
��Auto �6#   �� 54      $��* �6#   �� 55      $
��Auto �6#   .+�57      �� $��2��6   $��5��6%   $��1q�69   
������� �� CornerRadius$ 20��6a   $��15q�6s   �� 58      $��0��6   $��70��6$     icon#���  6/   �� 59      $��1��6   $	��0 1q�6%   
�������$ 20��6O   $��0 10 100 10q�6a   �� 5:      $��1��6   $��
DiplantKTV��6(   $��等线Light)�6:   
�������$��28(�6b   $��Center=�6p   $��
16 0 120 0q�6�   �� 5;      $��2��6   $��正在播放：��6(   
�������$��18(�6H   $��Center=�6V   $��	35 0 0 0 q�6q   �� 5<      $��3��6   
�������$��18(�6   $��Center=�6�   $
��15 0q�6�   $	��300��6�   ��6(   �� *PlayingSongName+ �� 	Converter# [ 6�   �� 5=      $��4��6   $��即将播放：��6(   
�������$��18(�6H   $��Center=�6V   �� 5>      $��5��6   
�������$��18(�6�   $��Center=�6�   $
��15 0q�6�   $	��300��6�   ��6(   �� *NextPlaySongName+# [ 6�   �� 5?      $��7��6   $��已选��6%   $��18(�62   $��	50 0 30 0q�6@   $��60��6S   $��60��6^     AccentCircleButtonStyle#��[ 5@      ��5A      �� *TurnToSelectedSongCmd+5B      4  .MahApps.Metro.Controls.MetroAnimatedTabControl  5D   
   -  'MahApps.Metro.Controls.TabControlHelper   
Underlined$ TabPanel=�6+   $y�	Collapsed=�5E   +    �� TabStripPlacement$
 Left=�5F   +    �� SelectedIndex 5G   +   �� *PageSelectedIndex+.@�5H      �� -   5         ��Name$
 Home��5H      $��首页��6$   )  #MahApps.Metro.Controls.MetroTabItem  5I      -   5          Name$ SingerSearch��5I   $   $��歌手��6:     5J      -   5        $ SongFilterList��5J   $   $��歌曲列表��6<   $��10 20q�6J   +  %MahApps.Metro.Controls.ControlsHelper   HeaderFontSize$ 18Q�6Z     5K      -   5        $ SelectedSongList��5K   $   $��已选��6>   $��10 20q�6J   $ 18Q�6Z   5L      � 5N   
   $��1��6   $��5q�6   .+�5P      � $��Center=�6r   $��Center=�6�    � Orientation$ Vertical=�5Q      y�5P      �� $
 Home��$ 
IsSelected�# [ .+�5R      !  MahApps.Metro.Controls.Tile     Title$ 歌名点歌��6      
BlackBrush   MouseOverBorderBrush# �  5S          HorizontalTitleAlignment$ Center=�5T          VerticalTitleAlignment$	 Top=�5U         LargeTileStyle#��[ 5V       ��5W       �� *SongNameFilterCmd+��5X      �� 5Y        �� 
StartPoint$	  0,0&�6.   ! �� EndPoint$	! 0,1&�6?   .��5Z      � " � Color$" #FFCB12��6+   # � Offset$# 0Q�6;   � 5[      $" #FF710C��6+   $
# 0.93Q�6;   5\      ��5^      �� 5_      .R�5`      �� .+�5a   "   ��   	musicLogo#���  6(   $	��400��6M   5b      5c      5e        5f      $ 歌星点歌��6    $��3q�5g       # �  5h       $ Center=�5i       #��[ 5j       ��5k       �� *SingerSearchOpenCmd+��5l      �� 5m      $	  0,0&�6.   $	! 0,1&�6?   .��5n      � $" #EA47A9��6+   $# 0Q�6;   � 5o      $" #FD0063��6+   $
# 0.93Q�6;   5p      ��5r      �� 5s      .R�5t      �� .+�5u   "   ��  	 singerPhoto#��� 	 6(   $	��400��6O   5v      5w      5y        5z      $  分类歌单              ��6    $ Center=�5{       $ Center=�5|       # �  5}        
 SmallTileStyle#��[
 5~       ��5       �� *SongTypeCmd+��5�      �� 5�      $	  0,0&�6.   $	! 0,1&�6?   .��5�      � $" #95F2FE��6+   $# 0Q�6;   � 5�      $" #10E1FC��6+   $
# 0.93Q�6;   5�      .��5�      .  (MahApps.Metro.IconPacks.PackIconOcticons  $��50��61   $��50��5�   1   �	  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconOcticonsKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]$ 	  Kind$$ FileDirectory=�5�   1   $��90 0 0 0q�5�   1   5�        5�      $  新歌上线              ��6    $ Center=�5�       $ Center=�5�       # �  5�       #��[
 5�       ��5�       �� *NewSongFilterCmd+��5�      �� 5�      $	  0,0&�6.   $	! 0,1&�6?   .��5�      � $" #D5F3A5��6+   $# 0Q�6;   � 5�      $" #8EDF0A��6+   $
# 0.93Q�6;   5�      .��5�      1
  +MahApps.Metro.IconPacks.PackIconFontAwesome
  $��70��64   $��70��5�   4   �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconFontAwesomeKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]%   Kind$% 	StarSolid=�5�   4   $��90 0 0 0q�5�   4   5�        5�      $              语 种��6    # �  5�       $ Center=�5�       $ Center=�5�       #��[
 5�       ��5�       �� *LanguageTypeCmd+��5�      �� 5�      $	  0,0&�6.   $	! 0,1&�6?   .��5�      � $" #F3B3BC��6+   $# 0Q�6;   � 5�      $" #E4566A��6+   $
# 0.93Q�6;   5�      .��5�      
  $��80��64   $��80��5�   4   $% CommentsRegular=�5�   4   $��0 0 70 0q�6K   5�        5�      $              榜 单��6    $��3q�5�       # �  5�       $ Center=�5�       $ Center=�5�       #��[
 5�       ��5�       �� *SongRankCmd+��5�      �� 5�      $	  0,0&�6.   $	! 0,1&�6?   .��5�      � $" #DB9FEE��6+   $# 0Q�6;   � 5�      $" #AA1ED6��6+   $
# 0.93Q�6;   5�      .��5�      
  $��60��64   $��60��5�   4   $% TrophySolid=�5�   4   $��0 0 70 0q�5�   4   5�      5�      � 5�      y�6   �� $ SingerSearch��$ 
IsSelected�# [ .+�5�      #   KtvMusic.Views.SingerSearchUc  -   5        &  Name$& singerSearchUc��5�   '    6?   �� $ 
ThisWindow��$ MusicViewModel�5�      � 5�      y�6   �� $ SongFilterList��$ 
IsSelected�# [ .+�5�      '   !KtvMusic.Views.SingerFilterListUc  -   5        '  Name$' singerFilterListUc��5�   +    6G   �� $ 
ThisWindow��$ MusicViewModel�5�      � 5�      y�6   �� $ SelectedSongList��$ 
IsSelected�# [ .+�5�      '   !KtvMusic.Views.SelectedSongListUc  -	   5        (  Name$( selectedSongListUc��5�   +    6G   �� $ 
ThisWindow��$ MusicViewModel�5�      5�      � 5�   
   $��2��6   $��5q�6   $��Center=�6'    5�        MenuButtonStyle) �� 
TargetType* �� Property+ �� Value, �� Triggers- P� Property. P� Value��%�  &	       �� 5�      ") ��5�      .�5�      �� * � ��
+ ���� 63   �� 5�      * � ��
+ �����63   �� 5�      * � ��$
+ 20 0q�6/   �� 5�      * � ��$+ 20 10q�60   �� 5�      * � ��$+ 0q�68   , 5�      P� 5�      - �@J�IsMouseOver. . . �5�      �� * � ��
+ �����6;   �� 5�      * � ��
+ �   �6;   �� 5�      * � ��$+ 0q�6@   5�      5�      5�       5�      �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   �� 5�      $
��Auto �6#   .+�5�      d� $��0��6   $��20px 0q�6+   
�������$��0q�6P    5 1MahApps.Metro.Styles.MetroCircleToggleButtonStyle#���  5�      / d� 	IsChecked/ 5�      �� *IsUsingSpeech+��5�      �� 5�      .R�5�      �� .+�5�      .  (MahApps.Metro.IconPacks.PackIconMaterial  �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconMaterialKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]0   Kind$0 TextToSpeech=�69   $��40��6M   $��40��6X   $��Center=�6d   �� 5�      $��语 音��6(   $��18(�63   1 �� TextAlignment$1 Center=�6A   5�      5�      5�      �� 5�      $��1��6   #��[ 6%   ��5�      �� *ReturnHomeCmd+��5�      �� 5�      .R�5�      �� .+�5�        $
0 Home=�69   $��40��6E   $��40��6P   $��Center=�6\   �� 5�      $��首页��6(   $��18(�62   $1 Center=�6@   5�      5�      5     �� 5     $��2��6   #��[ 6%   ��6N   �� *SoundDownCmd+��5     �� 5     .R�5     �� .+�5       $0 MinusCircleOutline=�69   $��40��6S   $��40��6^   $��Center=�6j   �� 5     $��调小��6(   $��18(�62   $1 Center=�6@   5	     5
     5     �� 5     $��3��6   #��[ 6%   ��6N   �� *SoundCancelCmd+��5     �� 5     .R�5     �� .+�5       $
$ Mute=�69   $��40��6E   $��40��6P   $��Center=�6\   �� 5     $��静音��6(   $��18(�62   $1 Center=�6@   5     5     5     �� 5     $��4��6   #��[ 6%   ��6N   �� *
SoundUpCmd+��5     �� 5     .R�5     �� .+�5       $0 PlusCircleOutline=�69   $��40��6R   $��40��6]   $��Center=�6i   �� 5     $��调大��6(   $��18(�62   $1 Center=�6@   5     5     5      �� 5!     $��5��6   #��[ 6%   ��6N   �� *SongToNextCmd+��5"     �� 5#     .R�5$     �� .+�5%     ,  &MahApps.Metro.IconPacks.PackIconEntypo  �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconEntypoKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]2   Kind$2 ControllerNext=�67   $��40��6M   $��40��6X   $��Center=�6d   �� 5&     $��切歌��6(   $��18(�62   $1 Center=�6@   5'     5(     5*     �� 5+     $��6��6   #��[ 6%   ��6N   �� *SongPauseAndPlayCmd+��5,     �� 5-     .R�5.     �� .+�5/     ,  &MahApps.Metro.IconPacks.PackIconModern  �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconModernKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]3   Kind$3 ControlPause=�67   $��40��6K   $��40��6V   $��Center=�6b   �� 50     $��暂停/播放��6(   $��18(�65   $1 Center=�6C   51     52     54     �� 55     $��7��6   #��[ 6%   ��6N   �� *SongAgainCmd+��56     �� 57     .R�58     �� .+�59     
  $% 	UndoSolid=�6<   $��40��6M   $��40��6X   $��Center=�6d   �� 5:     $��重唱��6(   $��18(�62   $1 Center=�6@   5;     5<     5>     �� 5?     $��8��6   #��[ 6%   ��6N   �� *SongAccompanyOrSingCmd+��5@     �� 5A     .R�5B     �� .+�5C       $$ Person=�69   $��40��6G   $��40��6R   $��Center=�6^   �� 5D     $��
原/伴唱��6(   $��18(�64   $1 Center=�6B   5E     5F     5H     �� 5I     $��9��6   #��[ 6%   ��6N   �� *	ShowMVCmd+��5J     �� 5K     .R�5L     �� .+�5M       $0 Airplay=�69   $��40��6H   $��40��6S   $��Center=�6_   �� 5N     $��MV��6(   $��18(�62   $1 Center=�6@   5O     5P     5R     5S     5T     5U     