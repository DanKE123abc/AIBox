   M S B A M L   `   `   `  ���� @  <AIBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:AIBoxAIBox      AIBox.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     %localclr-namespace:AIBox     -     � Title$  AIBox��$	��800��$	��320�� � WindowStartupLocation$ Manual=� � 
ResizeMode$ NoResize=�
������� � 	Resources    MyButton �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers	 P� Property
 P� Value��%�   &	        �� " ��.���  � �� �� " ��.R��� $��border��$��0q�#��z��._��� $��Center=�$��Center=� P� 	 �@J�IsMouseOver$

 True��. ���  �@J�Opacity$
 0.55Q�.��� -    J� 	AllowDrop$
 True�� � ColumnDefinitions �� $
��101* ��� $
��249* � � RowDefinitions �� $
��Auto ��� $��* �.+��� -   #���   
�������$��0��$�� —— ��$
��Left=�$��	92,0,0,10q�$	��132��$��Black"�$��2���� -   $��ConversationListBox��$��10,15,10,29q�$
��Auto=�$��Disabled=�$��2��$��2��
����������� .R��� .+��� ���#0022ff
�������$��5q�$��5q� �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�$��Right=�$��微软雅黑)�$��16(����� *UserMessage+�� 
�������
���   �$��5q�$��5q�$
 True��$
 Wrap=�$
��Left=�$��微软雅黑)�$��16(����� *AgentMessage+� $��Bottom=�$��1��$��2�� �� $
��Auto ��� $��* �.+��� -   $��0��$��发送�����#0022ff
�������$��60��$��10,5,0,5q�$��微软雅黑)�$��15(�$��Black"��� -    ��Name$ InputTextBox��$��1��$��1��$	��Top=�$��5,6,5,0q�$
��Auto��$
 Wrap=� �� AcceptsReturn$
 True��" �� VerticalScrollBarVisibility$
 Auto=�
�������$��微软雅黑)�$��15(�