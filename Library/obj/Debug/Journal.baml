   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5   	   7 clr-namespace:Library.ConvertersLibrary.Converters  5   	       Library.Journal   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Library     5   	   1conv clr-namespace:Library.Converters     5   	     � Title$  Журнал��5	   	   $	��450��6   $	��700��6%    � WindowStartupLocation$ CenterScreen=�61    � Icon$ Icons\Journal.png��6V    � 	Resources 5
      %   Library.Converters.IdToBookName   BookConverter%   Library.Converters.IdToLastName  ReaderConverter'   !Library.Converters.DateTimeToDate  DateConverter%   Library.Converters.BoolToString  BoolConverter"   Library.Converters.BoolToVis  VisibilityConverter%   Library.Converters.BoolToEnable  EnableConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _       5   
   6     5   
   6     5   
   6     5   
   6     5   
   6     5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   �� 5      $��90 �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��Table��5         AutoGenerateColumns .     
IsReadOnly . $��1��6b   $��1��6r      EnableRowVirtualization . 	   AlternatingRowBackground
	 �����
 �� AlternationCount$
 2��5!      ��5      �� *	Records+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Читатель��6%    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ 2*
 6}   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *UserID+ �� 	Converter# [ 6}     5$      $ 
Книга��6%   $ 2*
 6x    64   �� *BookID+# [  6x     5%      $ Дата выдачи��6%   $ 90
 6}    6:   �� *Date+# [ 6}     5&      $ Дата возврата��6%   $ 90
 6�    6<   �� *
ReturnDate+# [ 6�     5'      $ Взята?��6%   $ 50
 6~    65   �� *
IsAccepted+# [ 6~     5(      $ Возвращена?��6%   $ 90
 6�    6:   �� *
IsReturned+# [ 6�   5*      �� 5+   
   -   5        $	��btn��5+      $��20��6   $��1��6(   $��3��68   $	��100��6E   $��Читатель взял��6Q   $
��Left=�6A  {�6i   ��  �� Path$ SelectedItem.RecordID� �� ElementName$ Table��# [ ��6�   �� *Taken+��6�   �� $ SelectedItem.RecordID�$ Table��6A  �� 5,   
   $��1��6   $��3��6%    �� Orientation$ Vertical=�62   .+�5-      �� $��Оценка: ��6   $��Right=�6(   $	��100��6D    �� TextAlignment$ Center=�6P   �� 5.      $��Right=�6�   $	��100��6�   $��0,0,0,15q�6�   ��6   �� *Marks+{�65   �� $ SelectedItem.RecordID�$ Table��# [  �� SelectedIndex 6�   �� *SelectedMark+�� 5/      $	��100��6   $#��Читатель вернул��6"   $��Right=�6  {�6<   �� $ SelectedItem.RecordID�$ Table��# [ ��6�   �� *
Returned+��6�   �� $ SelectedItem.RecordID�$ Table��6  50      52      53      