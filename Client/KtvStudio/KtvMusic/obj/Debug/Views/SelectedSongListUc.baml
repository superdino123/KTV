   M S B A M L   `   `   `  ����C  ?KtvMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:KtvMusic.ViewsKtvMusic.Views  5      I)clr-namespace:KtvMusic.Helpers.ConvertersKtvMusic.Helpers.Converters  5      '    !KtvMusic.Views.SelectedSongListUc   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:KtvMusic.Views     5      R NMahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5      :
converters)clr-namespace:KtvMusic.Helpers.Converters  5        � 	Resources  5      C   =KtvMusic.Helpers.Converters.MVUploadUrlToLocationUrlConverter   Murtluc@   :KtvMusic.Helpers.Converters.MusicStrNullToDefaultConverter 
 Msntdc��%:   &	        &	        5   
   67     5   
   64   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $	��800��6   y�5      �� *
NoShowMv+  bool2VisibilityConverter �� 	Converter# [  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   $��2��6"   
�������$��10q�6F    J� Opacity$	 0.1Q�6R   �� 5      $��0��6   $��60��6"   $��60��6-   $��
20 20 20 0q�69   $
��Left=�6M     AccentCircleButtonStyle#��[ 5      ��5      �� *ReturnPreCmd+.��5      1  +MahApps.Metro.IconPacks.PackIconFontAwesome  $��50��60   $��50��5   0   �  �ControlzEx.PackIconBase`1[[MahApps.Metro.IconPacks.PackIconFontAwesomeKind, MahApps.Metro.IconPacks, Version=1.9.1.0, Culture=neutral, PublicKeyToken=null]]   Kind$ ArrowCircleLeftSolid=�5    0   5!      �� 5"      $��1��6   $��30 10 20 45q�6(   .��5#      �� -   5         ��Name$ SongSelectedStackPanel��5#       �� CanVerticallyScroll . $��Visible=�6X   5$      5%      � 5&   
   $��1��6    5'      �� 5(      $
��Auto �6    �� 5)      $
��Auto �6    �� 5*      $��* �6    �� 5+      $
��Auto �6    .+�5-      �� $��0��6   $��4��6"   
�������$	 0.1Q�6F   $
��0 10q�6T   �� 5.      $��1��6   $��5 0 0 0q�6"   
�������$��
10 20 10 0q�6P   y�5/      �� *
NoShowMv+# [ ._�50      �� $��18(�6   
�������$
��10 0q�6=   ��51      �� *PlayingSongName+# [ 52      r� 53      -   5         r�Name$ vedio��53      $��2��6*   $��10q�67   	 r� LoadedBehavior$	 Manual=�6C   
 r� Stretch$
 Uniform=�6\   $	��500��6n   $	��300��6}    r� Source 55      �� *PlayingSongUrl+# [  �� 56      -   5         ��Name$ sliderPosition��56   "   $��3��6   $��
10 0 10 20q�6:   
���������6N   ��  �� ElementName$ vedio�� �� Path$ Width�y�58      �� *
NoShowMv+# [ 59      5:      5;      