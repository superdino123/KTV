   M S B A M L   `   `   `  ����D  @KtvStudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KtvStudio.ViewsKtvStudio.Views  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      K*clr-namespace:KtvStudio.Helpers.ConvertersKtvStudio.Helpers.Converters  5      I EClientControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]?clr-namespace:ClientControls.Behaviours;assembly=ClientControlsClientControls.Behaviours 5
      *    $KtvStudio.Views.DataAnalysisManageUc   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:KtvStudio.Views     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      A
converters*clr-namespace:KtvStudio.Helpers.Converters     5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5	      ^clientControlsBehaviours?clr-namespace:ClientControls.Behaviours;assembly=ClientControls  5
        � 	Resources  5      A   ;KtvStudio.Helpers.Converters.ConsumeYearVisibilityConverter   CyvcB   <KtvStudio.Helpers.Converters.ConsumeMonthVisibilityConverter  Cmvc@   :KtvStudio.Helpers.Converters.ConsumeDayVisibilityConverter  Cdvc��%W   &	        &	      &	 &       5   
   64     5   
   65     5   
   63   5      .��5      � .+�5   
   �� $y�	Collapsed=�6I    �� SelectedIndex 6   �� *DataAnalysisSelectedIndex+.@�5      �� -   5         ��Name$ Index��5      $��封面��6%   �� 5      -   5        $ MoneyManage��5      $��	营业额��6+   �� 5      -   5        $ RoomTimeManage��5      $��包间使用率��6.   �� 5      -   5        $ SongHotManage��5      $��歌曲热度��6-   �� 5      -   5        $ SingerHotManage��5      $��歌手热度��6/   5      �� 5   
     dataAnalysisIndex#��[ 6   $	��150q�6<   y�5      ��  �� ElementName$ Index�� �� Path$ 
IsSelected�  bool2VisibilityConverter �� 	Converter# [ � 5   
   y�6   �� $ MoneyManage��$ 
IsSelected�# [  � RowDefinitions 5       �� 5!      $
��Auto �6    �� 5"      $��* �6    .+�5$      �� $��0��6   $��20q�6&    �� Orientation$ 
Horizontal=�62   .+�5%      �� $��时间范围��6   $��Center=�6(   $��10 0 0 0q�6C   �� 5&      $��	10 0 50 0q�6I    6   �� *ConsumeTimeSpanType+.@�5'      �� $��	近十年��6#   �� 5(      $��	某一年��6#   �� 5)      $��	某一月��6#   �� 5*      $��	某一天��6#   5+      *  $MahApps.Metro.Controls.NumericUpDown  5,      $	��100��6)   $��Center=�5-   %      ButtonsAlignment$
 Left=�5.   %   	   Maximum$
	 2300Q�5/   %   
   Minimum$

 1900Q�50   %      Value 51   %   �� *ConsumeYearValue+y�52   %   �� *ConsumeTimeSpanType+# [  �� 53      $	��年��6   $��Center=�6%   $��	10 0 30 0q�6@   y�54      �� *ConsumeTimeSpanType+# [    55      $	��100��6)   $��Center=�56   %   $
 Left=�57   %   $	 12Q�58   %   $
 1Q�59   %    5:   %   �� *ConsumeMonthValue+y�5;   %   �� *ConsumeTimeSpanType+# [ �� 5<      $	��月��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5=      �� *ConsumeTimeSpanType+# [   5>      $	��100��6)   $��Center=�5?   %   $
 Left=�5@   %   $	 31Q�5A   %   $
 1Q�5B   %    5C   %   �� *ConsumeDayValue+y�5D   %   �� *ConsumeTimeSpanType+# [ �� 5E      $	��日��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5F      �� *ConsumeTimeSpanType+# [ �� 5G      $��刷新��6   $
��20 0q�6F   $��13(�6U   ��6&   �� *TurnOverCmd+6U   5H      � 5I      $��1��6    5J      �� 5K      $��* �6$   �� 5L      $��* �6$   .+�5N      #  LiveCharts.Wpf.CartesianChart  $��0��6%   &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6]      Series 62   �� *ConsumeSeriesCollection+   AxisY 5P        LiveCharts.Wpf.Axis     Title$ Sales��6#      LabelFormatter 61   �� *ConsumeYFormatter+6`      AxisX 5S        $ Month��6#      Labels 61   �� *ConsumeLabels+6T   5U        LiveCharts.Wpf.PieChart  5V      $��1��6   $ Right=�5W         InnerRadius$	 100Q�6.   $
��0 15q�6@    5V   ,   �� *!ConsumeRoomTypeSeriesCollection+   ChartLegend 5X      "	  LiveCharts.Wpf.DefaultLegend	  5Y       	  
BulletSize$ 20Q�6,   6>      DataTooltip 5[      #
  LiveCharts.Wpf.DefaultTooltip
  5\       
  
BulletSize$ 20Q�6-   6?   5^      5_      5`      � 5c   
   y�6   �� $ RoomTimeManage��$ 
IsSelected�# [  5d      �� 5e      $
��Auto �6    �� 5f      $��* �6    .+�5h      �� $��0��6   $��20q�6&   $ 
Horizontal=�62   .+�5i      �� $��时间范围��6   $��Center=�6(   $��10 0 0 0q�6C   �� 5j      $��	10 0 50 0q�6I    6   �� *ConsumeTimeSpanType+.@�5k      �� $��	近十年��6#   �� 5l      $��	某一年��6#   �� 5m      $��	某一月��6#   �� 5n      $��	某一天��6#   5o        5p      $	��100��6)   $��Center=�5q   %   $
 Left=�5r   %   $
	 2300Q�5s   %   $

 1900Q�5t   %    5u   %   �� *ConsumeYearValue+y�5v   %   �� *ConsumeTimeSpanType+# [  �� 5w      $	��年��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5x      �� *ConsumeTimeSpanType+# [    5y      $	��100��6)   $��Center=�5z   %   $
 Left=�5{   %   $	 12Q�5|   %   $
 1Q�5}   %    5~   %   �� *ConsumeMonthValue+y�5   %   �� *ConsumeTimeSpanType+# [ �� 5�      $	��月��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5�      �� *ConsumeTimeSpanType+# [   5�      $	��100��6)   $��Center=�5�   %   $
 Left=�5�   %   $	 31Q�5�   %   $
 1Q�5�   %    5�   %   �� *ConsumeDayValue+y�5�   %   �� *ConsumeTimeSpanType+# [ �� 5�      $	��日��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5�      �� *ConsumeTimeSpanType+# [ �� 5�      $��刷新��6   $
��20 0q�6I   $��13(�6X   ��6&   �� *RoomUseRateCmd+6X   5�      � 5�      $��1��6    5�      �� 5�      $��* �6$   �� 5�      $��* �6$   .+�5�        $��0��6%   $ Right=�6^    62   �� *RoomTimeSeriesCollection+ 5�        $ Sales��6#      MinValue$ 0Q�6_      MaxValue$ 1Q�6l    61   �� *RoomTimeYFormatter+6{    5�        $ Month��6#    61   �� *RoomTimeLabels+6U   5�        5�      $��1��6   $ Right=�5�      $	 100Q�6.   $
��0 15q�6@    5�   ,   �� *" UserRateRoomTypeSeriesCollection+ 5�      	  5�      $ 20Q�6,   6>    5�      
  5�      $ 20Q�6-   6?   5�      5�      5�      � 5�   
   y�6   �� $ SongHotManage��$ 
IsSelected�# [  5�      �� 5�      $
��Auto �6    �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $
��Auto �6    �� 5�      $��* �6    .+�5�      �� $��0��6   $��
20 20 20 5q�6&   $ 
Horizontal=�6:   .+�5�      �� $$��点唱次数统计时间限制��6   $��Center=�6.   $��0q�6I   (  "System.Windows.Controls.DatePicker  5�      $	��150��6   $��10 0 0 0q�6)      SelectedDate 5�      �� *MinClickDate+�� 5�      $	��内��6   $��Center=�6%   $��	10 0 50 0q�6@     5�      $	��150��6   $��	10 0 50 0q�6)   $y�	Collapsed=�6<    5�      �� *MaxClickDate+�� 5�      $��点唱次数限制��6   $��Center=�6*   $��10 0 0 0q�6E     5�      $	��100��6)   $��Center=�5�   )   $
 Left=�5�   )   $
 0Q�5�   )   $��10 0 0 0q�5�   )    5�   )   �� *MinRecordNumber+�� 5�      $��以上��6   $��Center=�6&   $��	10 0 50 0q�6A   �� 5�      $��	半衰期��6   $��Center=�6'   $��10 0 0 0q�6B     5�      $	��100��6)   $��Center=�5�   )   $
 Left=�5�   )   $
 1Q�5�   )   $��10 0 0 0q�5�   )    5�   )   �� *HalfLifeValue+�� 5�      $	��天��6   $��Center=�6%   $��	10 0 50 0q�6@   �� 5�      $��刷新��6   $
��20 0q�6E   $��13(�6T   $
��10 0q�6b   ��6&   �� *
SongHotCmd+6b   �� 5�      $��更新排行��6   $
��20 0q�6M   $��13(�6\   $��10 0 0 0q�6j   ��6(   �� *UpdateSongHotCmd+6j   5�      �� 5�      $��1��6   $ 
Horizontal=�6&   $��
20 0 20 10q�6?    J� Opacity$	 0.5Q�6S   .+�5�      �� $!��新歌热度计算公式：��6   $��Center=�6-   $��0q�6H   �� 5�      $z��t点击量 * (0.5)^([点唱次数统计时间限制与出版时间的最大值]与现在时间的时间差/半衰期)��6   $��Center=�6T   $��0q�6o   5�      &   System.Windows.Controls.DataGrid  5�      -   5        $��2��5�         
IsReadOnly .    AutoGenerateColumns .     RowHeaderWidth$ 20Q�5�         HeadersVisibility$	 All=�5�      $
��20 0q�5�      0  *ClientControls.Behaviours.DataGridBehavior    DisplayRowNumber  .   AzureDataGrid#���  5�      !   SelectionMode$! Extended=�5�      ��5�      �� *SongRecordTempResultSource+"   Columns" 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn#   Header$# 歌曲名称��6)   $   Width5  /System.Windows.Controls.DataGridLengthConverter$	$ 200 6U   1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 67   �� *	musicname+6U     5�      $# 歌手姓名��6)   $	$ 200 6V   % 67   �� *
singername+6V     5�      $# 热度得分��6)   $	$ 200 6n   % 67   �� *	hotnumber+& �� StringFormat$& {0:N2}��6n     5�      $# 	点击量��6)   $	$ 200 6S   % 66   �� *
clicknum+6S     5�      $# 发行日期��6)   $	$ 200 6x   % 67   �� *releasedate+$& {0:yyyy-MM-dd}��6x   5�      �� 5�      $��3��6   $��20q�6&   $ 
Horizontal=�62   .+�5�      �� $��时间范围��6   $��Center=�6(   $��10 0 0 0q�6C   �� 5�      $��	10 0 50 0q�6F    6   �� *SongTimeSpanType+.@�5�      �� $��	近十年��6#   �� 5�      $��	某一年��6#   �� 5�      $��	某一月��6#   5�        5�      $	��100��6)   $��Center=�5�   %   $
 Left=�5�   %   $
	 2300Q�5�   %   $

 1900Q�5�   %    5�   %   �� *SongYearValue+y�5�   %   �� *SongTimeSpanType+# [  �� 5�      $	��年��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5�      �� *SongTimeSpanType+# [    5�      $	��100��6)   $��Center=�5�   %   $
 Left=�5�   %   $	 12Q�5�   %   $
 1Q�5�   %    5�   %   �� *SongMonthValue+y�5�   %   �� *SongTimeSpanType+# [ �� 5�      $	��月��6   $��Center=�6%   $��	10 0 30 0q�6@   y�5�      �� *SongTimeSpanType+# [ �� 5�      $��刷新��6   $
��20 0q�6I   $��13(�6X   ��6&   �� *SongTimeHotCmd+6X   5�        5�      $��4��6!   $ Right=�6Z    6.   �� *SongTimeSeriesCollection+ 5�        $ Number��6    6.   �� *SongTimeYFormatter+6^    5       $
 Time��6    6,   �� *SongTimeLabels+6P   5     5     � 5  
   y�6   �� $ SingerHotManage��$ 
IsSelected�# [  5	     �� 5
     $
��Auto �6    �� 5     $��* �6    .+�5     �� $��0��6   $��
20 20 20 5q�6&   $ 
Horizontal=�6:   .+�5     �� $$��点唱次数统计时间限制��6   $��Center=�6.   $��0q�6I     5     $	��150��6   $��10 0 0 0q�6)    5     �� *SingerMinClickDate+�� 5     $	��内��6   $��Center=�6%   $��	10 0 50 0q�6@   �� 5     $��刷新��6   $
��20 0q�6G   $��13(�6V   $
��10 0q�6d   ��6&   �� *SingerHotCmd+6d   �� 5     $��更新排行��6   $
��20 0q�6O   $��13(�6^   $��10 0 0 0q�6l   ��6(   �� *UpdateSingerHotCmd+6l   5       5     $��1��6    .  .  $ 20Q�5     $	 All=�5     $
��20 0q�5       . #���  5     ��5     �� *SingerRecordTempResultSource+" 5       5      $# 歌手姓名��6)   $	$ 200 6V   % 67   �� *
singername+6V     5!     $# 	点击量��6)   $	$ 200 6S   % 66   �� *
clicknum+6S   5#     5$     5&     5'     