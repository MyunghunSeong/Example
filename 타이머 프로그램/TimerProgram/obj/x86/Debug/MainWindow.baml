   M S B A M L   `   `   `  ����G  CTimerProgram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TimerProgramTimerProgram  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       TimerProgram.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:TimerProgram     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5       �� 
TargetType �� Property �� Value  TimerProgram.GetTimeClass   now  rest��%l  '��       '��     &	      &	 1    �� 5   
   " ��6   .�5	      ��  � ��$ 40(�6)   �� 5
       � ��$ Center=�64   �� 5       � ��$ Center=�62   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 �� ��6+   �� 5       � ��$ Timer��6%   5      �� 5   
   " ��6   .�5      ��  �@��TextAlignment$
 Left=�6.   �� 5       � ��$ 30(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��
 ��p��6+   �� 5       � ��$ Center=�62   �� 5       � ��$ 0q�60   5        5   
   6.     5   
   6/   5      .��5      � -   5        $��gridView��5   *    � RowDefinitions 5   
   �� 5       $��2* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6    � ColumnDefinitions 5&   
   �� 5'      $��* �6   �� 5(      $��* �6   .+�5+   
   �� $��0��6   $��0��6!   $��2��61   �� 5-   
   $��1��6   $��0��6"   $��2��62   $��2��6G    �� Orientation$ 
Horizontal=�6X   .+�5.      �� -   5        $��label1��5.      $��현재 시간 : ��6"   $��Center=�65   $��20(�6P   
����� �6^   �� 5/      -   5        $��text1��5/   b   ��6   ��  �� Path$	 Now�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=���50      ��  �� ElementName$ gridView��$ ActualWidth���6O   �� $ gridView��$ ActualHeigth�51      �� 53   
   $��3��6   $��0��6   $��2��6.   .+�54      �� -   5        $��
pictureBox��54      ��6*   �� $ gridView��$ ActualWidth���6c   �� $ gridView��$ ActualHeight�55      56      57      